using UnityEngine;
using System.Collections;

public class SwordPlayPlayerController : MonoBehaviour {

    // controller switch
    public bool controllerEnabled;

    private Animator animator;
    private SpriteRenderer sprite;

    // attack objects
	public GameObject Attack_1;
	public GameObject Attack_2;
	public GameObject Attack_3;
	public GameObject Attack_4;
	public GameObject Attack_5;
	public GameObject Attack_6;
	public GameObject Attack_7;
	public GameObject Attack_8;

    public float attackLength;
	public float attackStamina = 10;
	private float currentStamina;

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
	public float speedInAir;
    public float maxSpeed;
    public float jumpSpeed;

    private bool right;
    private bool left;

    // ground check vars
    private bool grounded;
    public GameObject groundCheck;


    void Start () {
        //turn off all attacks
		TurnOffAttacks();

		currentStamina = attackStamina;

        rb2d = gameObject.GetComponent <Rigidbody2D> ();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer> ();
    }

	void TurnOffAttacks() {
		Attack_1.SetActive (false);
		Attack_2.SetActive (false);
		Attack_3.SetActive (false);
		Attack_4.SetActive (false);
		Attack_5.SetActive (false);
		Attack_6.SetActive (false);
		Attack_7.SetActive (false);
		Attack_8.SetActive (false);
	}

    void FixedUpdate () {
        Vector2 moveHorizontal = Vector2.zero;

        if (controllerEnabled == true) {
            // get right joystick input
            moveHorizontal = new Vector2 (Input.GetAxis ("LeftJoystickX"), 0);

            // move player horizontally
			if (grounded) {
				rb2d.AddForce (moveHorizontal * speed);
			} else {
				rb2d.AddForce (moveHorizontal * speedInAir);
			}
         

            if (Input.GetButtonDown ("A") && grounded) {
                rb2d.AddForce (jumpSpeed * transform.up);
            }


        } else {
            right = Input.GetKey ("d");
            left = Input.GetKey ("a");

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

        // set animation
        if (Mathf.Abs(rb2d.velocity.x) > 0) {
            animator.SetBool ("isWalking", true);
        } else {
            animator.SetBool ("isWalking", false);
        }

        if (Mathf.Sign (rb2d.velocity.x) > 0 && Input.GetAxis ("LeftJoystickX") > 0) {
            sprite.flipX = false;
        } else if (Mathf.Sign (rb2d.velocity.x) < 0 && Input.GetAxis ("LeftJoystickX") < 0){
            sprite.flipX = true;
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


		//attack
        if (isRotating || isMoving) {
			if (currentStamina > 0) {
				float quadrant = CheckQuadrant (currentAngle);
				SetQuadrantActive (quadrant);
				currentStamina--;
			}
        } else {
			TurnOffAttacks();
			if (currentStamina < attackStamina) {
				currentStamina++;
			}
        }

		Debug.Log (currentStamina);

        previousAngle = Mathf.Atan2(joyVertical, joyHorizontal)  * Mathf.Rad2Deg;
        previousPosX = Input.GetAxis ("RightJoystickX");
        previousPosY = Input.GetAxis ("RightJoystickY");

    }

    void SetQuadrantActive (float quadrant) {
        if (quadrant == 1) {
            Attack_1.SetActive (true);
        } else if (quadrant == 2) {
			Attack_2.SetActive (true);
        } else if (quadrant == 3) {
			Attack_3.SetActive (true);
		} else if (quadrant == 4) {
			Attack_4.SetActive (true);
		} else if (quadrant == 5) {
			Attack_5.SetActive (true);
		} else if (quadrant == 6) {
			Attack_6.SetActive (true);
		} else if (quadrant == 7) {
			Attack_7.SetActive (true);
        } else {
			Attack_8.SetActive (true);
        }
    }

    public float CheckQuadrant (float angle) {
        if (angle >= -158 && angle < -113) {
            return(8);
        } else if (angle >= -113 && angle < -67) {
            return(7);
        } else if (angle >= -67 && angle < -22) {
            return(6);
		} else if (angle >= -22 && angle < 22) {
			return(5);
		} else if (angle >= 22 && angle < 67) {
			return(4);
		} else if (angle >= 67 && angle < 113) {
			return(3);
		} else if (angle >= 113 && angle < 158) {
			return(2);
		} else {
            return(1);
        }
    }
}