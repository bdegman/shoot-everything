using UnityEngine;
using System.Collections;

public class ItemPickUp : MonoBehaviour {

	private GameObject player;
	public GameObject weapon;

	void Start () {
		player = GameObject.Find("player");
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.collider.tag == "Player") {

			// destroy object
			Destroy (gameObject);

			// randomly pick weapon type and assign
			if (Random.value > -0.1) {
				player.GetComponent<PlayerController>().SwitchWeapon(weapon);

			}

		}
		return;

	}
}
