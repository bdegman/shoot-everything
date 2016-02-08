using UnityEngine;
using System.Collections;

public class SwordPlayPlayerController : MonoBehaviour {

	// controller switch
	public bool controllerEnabled;


	// attack objects
	public GameObject rightAttack;
	public GameObject leftAttack;
	public GameObject upAttack;
	public GameObject downAttack;

	public float attackLength;


	// joy stick monitoring
	private float currentAngle;
	private float previousAngle;

	private bool isRotating;

	private bool isMoving;
	private float previousPosX;
	private float previousPosY;

	private float joyHorizontal;
	private float joyVertical;


	// movement vars
	private Rigidbody2D rb2d;
	public float speed;
	public float maxSpeed;


	// ground check vars
	private bool grounded;
	public GameObject groundCheck;


	void Start () {
		//turn off all attacks
		rightAttack.SetActive (false);
		leftAttack.SetActive (false);
		upAttack.SetActive (false);
		downAttack.SetActive (false);

		rb2d = gameObject.GetComponent <Rigidbody2D> ();
	}

	void FixedUpdate () {
		Vector2 moveHorizontal = Vector2.zero;

		if (controllerEnabled == true) {
			// get right joystick input
			moveHorizontal = new Vector2 (Input.GetAxis ("LeftJoystickX"), 0);

			// move player horizontally
			rb2d.AddForce (moveHorizontal * speed);


		} else {
			bool right = Input.GetKey ("d");
			bool left = Input.GetKey ("a");

			// move player horizontally
			if (right){
				rb2d.AddForce (transform.right * speed);
			}

			if (left){
				rb2d.AddForce (transform.right * speed * -1);
			}
				
		}

		//limit horizontal speed
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}

		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}

	}

	void Update () {
		// groundcheck
		Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
		Vector2 groundPos = new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y);
		grounded = Physics2D.Linecast(playerPos , groundPos , 1 << LayerMask.NameToLayer("Ground"));


		// attacking
		joyHorizontal = Input.GetAxis("RightJoystickX");
		joyVertical = Input.GetAxis("RightJoystickY");
		currentAngle = Mathf.Atan2(joyVertical, joyHorizontal)  * Mathf.Rad2Deg;

		// check to see if stick is moving from center
		if (Input.GetAxis ("RightJoystickX") != previousPosX || Input.GetAxis ("RightJoystickY") != previousPosY) {
			isMoving = true;
		} else {
			isMoving = false;
		}

		// check to see if stick is rotating
		if (Mathf.Abs (Mathf.Abs (currentAngle) - Mathf.Abs (previousAngle)) > 10) {
			isRotating = true;
		} else {
			isRotating = false;
		}

		if (isRotating || isMoving) {
			float quadrant = CheckQuadrant (currentAngle);
			StartCoroutine (Attack (quadrant));
		}

		previousAngle = Mathf.Atan2(joyVertical, joyHorizontal)  * Mathf.Rad2Deg;
		previousPosX = Input.GetAxis ("RightJoystickX");
		previousPosY = Input.GetAxis ("RightJoystickY");

	}

	IEnumerator Attack (float quadrant) {
		if (quadrant == 1) {
			rightAttack.SetActive (true);
		} else if (quadrant == 2) {
			upAttack.SetActive (true);
		} else if (quadrant == 3) {
			leftAttack.SetActive (true);
		} else {
			downAttack.SetActive (true);
		}
		yield return new WaitForSeconds (attackLength);

		//turn off all attacks
		rightAttack.SetActive (false);
		leftAttack.SetActive (false);
		upAttack.SetActive (false);
		downAttack.SetActive (false);
	}

	public float CheckQuadrant (float angle) {
		if (angle >= -135 && angle < -45) {
			return(4);
		} else if (angle >= -45 && angle < 45) {
			return(3);
		} else if (angle >= 45 && angle < 135) {
			return(2);
		} else {
			return(1);
		}
	}
	
}