using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject item;

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" || tag == "Player" || other.tag == "Shot" || tag == "Shot") {
			
			// destroy objects
			Destroy (other.gameObject);
			Destroy (gameObject);

			// item drop
			if (tag == "Enemy") {
				// drop item 50% of time
				if (Random.value > 0.5) {
					Instantiate (item, transform.position, transform.rotation);
				}

			}

		}
		return;

	}
}
