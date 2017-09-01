using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoom2 : MonoBehaviour {
	//Setting up a place to put the cameras 
	public Camera cam1;
	public Camera cam2;



	//When the player collides with the trigger, turn the player camera off and the room camera on
	void OnTriggerEnter(Collider Player)
	{
		if (Player.tag == "Player") {
			cam1.enabled = false;
			cam2.enabled = true;
		}
		
	}
	//When the player leaves the trigger, turn the room camera off and player camera on
	void OnTriggerExit(Collider Player){
		if (Player.tag == "Player"){
			cam2.enabled = false;
		cam1.enabled = true;
	}
	}}