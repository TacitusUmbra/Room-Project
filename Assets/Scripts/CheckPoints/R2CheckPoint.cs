using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R2CheckPoint : MonoBehaviour {
	
	// If the player falls off the path of Room 2 and enters the trigger on the floor, they will be moved to a new position which is the checkpoint for Room 2
	void OnTriggerEnter (Collider Player)
	{
		if (Player.tag == "Player")
			Player.transform.position = new Vector3 (59f, 2f, 0f);

	}
}