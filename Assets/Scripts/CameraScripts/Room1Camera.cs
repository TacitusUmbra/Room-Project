using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Camera : MonoBehaviour {
	
	//Setting up the target for the camera to look at
	public Transform target;

	//Look at the target, in this case the player, once the camera is turned on
	void Update () {
		transform.LookAt(target);
	}
}
