using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour {

	private GameObject followObject;
	public float moveSpeed;


	void Update () {
		followObject = GameObject.Find ("player");
		transform.position = Vector2.MoveTowards(transform.position, followObject.transform.position, moveSpeed);
	}
}
