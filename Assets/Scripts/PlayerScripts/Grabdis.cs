using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabdis : MonoBehaviour {
	//Sets the object(keycards in my case) to SetNearestGrab if it collides with something that has the tag of Player.
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<Player> ().SetNearestGrab (this);
		}
	}
	//If something that has the tag of Player exits the collider, the object(keycards in my case) will no longer be the SetNearestGrab.
	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<Player> ().SetNearestGrab (null);
		}
	}
}
