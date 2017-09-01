using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4Trigger : MonoBehaviour {
	// Setting up a GameObject drawbridge to draw upon
	public GameObject drawbridge;
	//When the player collides and stays on the collider (switch of room 4), the drawbridge will go up.
	void OnTriggerStay (Collider Player)
	{
		if (Player.tag == "Player")
			drawbridge.transform.position += Vector3.up * (Time.deltaTime/2);
	}
}