using UnityEngine;
using System.Collections;

public class HazardMover : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speed;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = -transform.right * speed;
	}
}
