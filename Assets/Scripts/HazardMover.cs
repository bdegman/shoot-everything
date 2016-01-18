using UnityEngine;
using System.Collections;

public class HazardMover : MonoBehaviour {

	private GameObject player;
	private Rigidbody2D rb2d;

	public float direction;
	public float speed;

	void Start () {
		player = GameObject.Find("player");

		// check which side the player is on
		direction = Mathf.Sign (transform.position.x - player.transform.position.x);

		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = -transform.right * speed * direction;
	}
}
