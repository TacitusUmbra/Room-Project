using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R4CheckPoint : MonoBehaviour {
	
	// If the player falls off the bridge of Room 4 and enters the trigger on the floor, they will be moved to a new position which is the checkpoint for Room 4
	void OnTriggerEnter (Collider Player)
	{
		if(Player.tag =="Player")
			Player.transform.position = new Vector3(205f,2f,1.5f);


	}
}