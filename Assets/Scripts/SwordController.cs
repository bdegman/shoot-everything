using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {

	private Animator animator;

	private bool joystickMove;
	private float joystickDirection;

	private float joystickRotationSpeed;
	private float previousRotation;

	void Start () {
		animator = GetComponent<Animator>();
	}

	void Update() {
		joystickRotationSpeed = (Mathf.Atan2 (Input.GetAxis ("RightJoystickX"), Input.GetAxis ("RightJoystickY")) - previousRotation) / Time.deltaTime;
		previousRotation = Mathf.Atan2 (Input.GetAxis ("RightJoystickX"), Input.GetAxis ("RightJoystickY"));

		if (joystickRotationSpeed > 10) {
			joystickMove = true;
		} else {
			joystickMove = false;
		}

		if (joystickMove) {
			animator.SetTrigger ("Swing");
		} else {
			animator.ResetTrigger ("Swing");
		}

	}

}
