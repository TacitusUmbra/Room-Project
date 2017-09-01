using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R3CheckPoint : MonoBehaviour {
	
	// If the player falls off the platforms of Room 3 and enters the trigger on the floor, they will be moved to a new position which is the checkpoint for Room 3
	void OnTriggerEnter (Collider Player)
	{
		if(Player.tag =="Player")
			Player.transform.position = new Vector3(136f,2f,1.5f);


	}
}