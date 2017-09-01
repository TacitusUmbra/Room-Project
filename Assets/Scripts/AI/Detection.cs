using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
	//This is the target that to detect.
	public Transform target;
	//Public bool touched is set to false.
	public bool touched = false;
	//This is a public transform for the wall.
	public Transform wall;

	//If the player stays inside of the collider, touched will become true. 
	void OnTriggerStay (Collider Player)
	{
		if (Player.tag == "Player") {
			touched = true;
		}
	}
	//If the player exits the collider, touched will become false.
	void OnTriggerExit(Collider Player){
		touched = false;
		
		
	}
}
