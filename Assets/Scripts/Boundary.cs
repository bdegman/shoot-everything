using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		// Destroy everything that leaves the trigger
		Destroy(other.gameObject);
	}
}	
