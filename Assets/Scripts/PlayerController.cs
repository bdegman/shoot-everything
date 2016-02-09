	using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// controller switch
	public bool controllerEnabled;

	private Animator animator;

	// weapon
	public GameObject defaultWeapon;
	public Transform shotSpawn;
	private GameObject shot;
	private float fireRate;
	private float ammo;
	private float nextFire;

	// movement vars
	private Rigidbody2D rb2d;
	public float speed;
	public float maxSpeed;

	// ground check vars
	private bool grounded;
	public GameObject groundCheck;

	// jetpack vars
	public GUIText jetPackText;
	public float jetPackMaxFuel;
	public float verticalSpeed;
	private float jetPackFuel;
	public float jetPackBurnRate;
	public float jetPackRechargeRate;


	public void SwitchWeapon (GameObject weapon) {
		fireRate = weapon.GetComponent<WeaponProperties>().fireRate;
		ammo = weapon.GetComponent<WeaponProperties>().ammo;
		shot = weapon.GetComponent<WeaponProperties>().shot;
	}


	void Start ()
	{
		jetPackFuel = jetPackMaxFuel;
		jetPackText.text = jetPackFuel.ToString ();
		rb2d = gameObject.GetComponent <Rigidbody2D> ();

		SwitchWeapon (defaultWeapon);
		StartCoroutine (Recharge ());

		animator = GetComponentInChildren<Animator> ();
	}

	void Update () {
		//shooting
		if (controllerEnabled == true) {
			if (Input.GetAxis ("Right Trigger") > 0 && Time.time > nextFire) {
				Fire ();
			}
		} else {
			if (Input.GetButton ("Fire1") && Time.time > nextFire) {
				Fire();
			}
		}
			
		// groundcheck
		Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
		Vector2 groundPos = new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y);
		grounded = Physics2D.Linecast(playerPos , groundPos , 1 << LayerMask.NameToLayer("Ground"));

	}

	void Fire () {
		nextFire = Time.time + fireRate;
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		ammo = ammo - 1;
		
		if (ammo == 0) {
			SwitchWeapon (defaultWeapon);
		}
				
	}

	void FixedUpdate () {
		Vector2 moveHorizontal = Vector2.zero;
		Vector2 moveVertical = Vector2.zero;

		if (controllerEnabled == true) {
			// get right joystick input
			moveHorizontal = new Vector2 (Input.GetAxis ("LeftJoystickX"), 0);
			moveVertical = new Vector2 (0, Input.GetAxis ("Left Trigger"));

			// move player horizontally
			rb2d.AddForce (moveHorizontal * speed);

			// jetpack lift
			if (jetPackFuel > 0 && moveVertical.y > 0) {
				rb2d.AddForce (verticalSpeed * moveVertical);
				jetPackFuel = jetPackFuel - jetPackBurnRate;
				jetPackText.text = jetPackFuel.ToString ();
			}

		} else {
			bool right = Input.GetKey ("d");
			bool left = Input.GetKey ("a");
			bool up = Input.GetKey ("w");

			// move player horizontally
			if (right){
				rb2d.AddForce (transform.right * speed);
			}

			if (left){
				rb2d.AddForce (transform.right * speed * -1);
			}

			if (jetPackFuel > 0 && up == true) {
				rb2d.AddForce (verticalSpeed * transform.up);
				jetPackFuel = jetPackFuel - jetPackBurnRate;
				jetPackText.text = jetPackFuel.ToString ();
			}


		}

		//limit horizontal speed
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}

		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
			

		// set animatino
		if (Mathf.Abs(rb2d.velocity.x) > 0) {
			animator.SetBool ("isWalking", true);
		} else {
			animator.SetBool ("isWalking", false);
		}
			
	}
		

	IEnumerator Recharge () {
		while (true) {
			if (jetPackFuel < jetPackMaxFuel && grounded == true) {
				jetPackFuel = jetPackFuel + jetPackBurnRate;
				jetPackText.text = jetPackFuel.ToString ();
			}
			yield return new WaitForSeconds (jetPackRechargeRate);
		}

	}

}
