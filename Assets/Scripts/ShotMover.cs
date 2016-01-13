using UnityEngine;
using System.Collections;

public class ShotMover : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speed;
	private GameObject aimer;

	void Start () {
		aimer = GameObject.Find("Aimer Point");
		rb2d = GetComponent<Rigidbody2D> ();

		//point towards aimer
		transform.rotation = aimer.transform.rotation;

		//move forward
		rb2d.velocity = transform.up * speed;

	}

}
