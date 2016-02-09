using UnityEngine;
using System.Collections;

public class AimerController : MonoBehaviour {

	public GameObject player;

	public GameObject playerRender;
	private SpriteRenderer playerSprite;
	private SpriteRenderer sprite;

	private float aimDirection;

	void Start () {
		sprite = GetComponentInChildren<SpriteRenderer> ();
		playerSprite = playerRender.GetComponent<SpriteRenderer> ();
	}

	void Update () {
		// mouse aim
		if (player.GetComponent<PlayerController> ().controllerEnabled == false) {
			Vector2 mouse_pos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 object_pos = Camera.main.WorldToScreenPoint (transform.position);
			mouse_pos.x = mouse_pos.x - object_pos.x;
			mouse_pos.y = mouse_pos.y - object_pos.y;

			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2 (mouse_pos.x, mouse_pos.y) * Mathf.Rad2Deg * -1);

			if (Input.mousePosition.x > player.transform.position.x) {
				aimDirection = 1;
			} else {
				aimDirection = -1;
			}

		} else {
			// controller aim
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2(Input.GetAxis("RightJoystickX"), Input.GetAxis("RightJoystickY")) * Mathf.Rad2Deg);

			if (transform.eulerAngles.z > 180 && transform.eulerAngles.z < 360) {
				aimDirection = 1;
			} else {
				aimDirection = -1;
			}
		}

		if (aimDirection > 0) {
			sprite.flipY = false;
			playerSprite.flipX = false;
		} else {
			sprite.flipY = true;
			playerSprite.flipX = true;
		}

	}
}