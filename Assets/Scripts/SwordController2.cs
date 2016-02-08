using UnityEngine;
using System.Collections;

public class SwordController2 : MonoBehaviour {

	public GameObject item;
	public GameObject item2;

	public Transform parentTransform;

	private float previousRotation;
	private float swordRotationSpeed; 

	void Start() {
		parentTransform = GetComponentInParent<Transform>();
	}

	void Update() {
		swordRotationSpeed = (parentTransform.eulerAngles.magnitude -previousRotation) / Time.deltaTime;
		previousRotation = parentTransform.eulerAngles.magnitude;

		Debug.Log (swordRotationSpeed);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Enemy" || other.tag == "Hazard") {

			// destroy objects
			if ( Mathf.Abs(swordRotationSpeed) > 25) {
				Destroy (other.gameObject);
				// item drop
				if (tag == "Enemy") {
					// drop item 50% of time
					float rnd = Random.value;
					if (rnd > 0.5) {
						if (rnd > 0.75) {	
							Instantiate (item, other.transform.position, other.transform.rotation);
						} else {
							Instantiate (item2, other.transform.position, other.transform.rotation);
						}
					}

				}
			}

		}
		return;

	}
}
