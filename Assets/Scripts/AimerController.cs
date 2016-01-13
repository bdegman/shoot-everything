using UnityEngine;
using System.Collections;

public class AimerController : MonoBehaviour {

	public GameObject player;

	void Update () {
		// mouse aim
		if (player.GetComponent<PlayerController> ().controllerEnabled == false) {
			Vector2 mouse_pos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 object_pos = Camera.main.WorldToScreenPoint (transform.position);
			mouse_pos.x = mouse_pos.x - object_pos.x;
			mouse_pos.y = mouse_pos.y - object_pos.y;

			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2 (mouse_pos.x, mouse_pos.y) * Mathf.Rad2Deg * -1);
		} else {
			// controller aim
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2(Input.GetAxis("RightJoystickX"), Input.GetAxis("RightJoystickY")) * Mathf.Rad2Deg);
		}
			
	}
}