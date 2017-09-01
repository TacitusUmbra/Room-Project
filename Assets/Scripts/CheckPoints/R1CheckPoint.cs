using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R1CheckPoint : MonoBehaviour {

	// If the player falls off the bridge of Room 1 and enters the trigger on the floor, they will be moved to a new position which is the checkpoint for Room 1
	void OnTriggerEnter (Collider Player)
	{
		if(Player.tag =="Player")
		Player.transform.position = new Vector3(0f,2f,0f);


	}
}