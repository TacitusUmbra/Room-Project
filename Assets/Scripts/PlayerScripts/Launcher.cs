using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
	//letting this script connect to the PlayerConfig
	public PlayerConfig pc;
	//Setting up a place to put our Rocket Prefab
	public GameObject rocketPrefab;
	void Update () {
		//If the player presses E on the keyboard, instantiate a rocket from out prefab that moves forward
			if (Input.GetKeyDown (this.pc.shoot)) {
				Instantiate (rocketPrefab, this.transform.position, this.transform.rotation); 

	}
}
}

