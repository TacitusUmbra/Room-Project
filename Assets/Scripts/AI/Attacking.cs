using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour {

	// Setting up a bool touched to false. This will tell if the player remains inside or exits the collider.
	public bool touched = false;
	//If the player with tag of player remains within the collider, touched will become true.
	void OnTriggerStay (Collider Player)
	{
		if (Player.tag == "Player") {
			touched = true;
		}
	}
	//If the collider is no longer touching the tag of player, touched will become false.
	void OnTriggerExit(Collider Player){
		touched = false;

	}
}
