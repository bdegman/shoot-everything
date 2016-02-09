using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject item;
	public GameObject item2;

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Shot" || other.tag == "SniperShot") {
			
			// destroy objects
			if (other.tag != "SniperShot") {
				Destroy (other.gameObject);
			}
			Destroy (gameObject);

			// item drop
			if (tag == "Enemy") {
				// drop item 50% of time
				float rnd = Random.value;
				if (rnd > 0.5) {
					Instantiate (item, transform.position, transform.rotation);
				}

			}

		}
		return;

	}
}
