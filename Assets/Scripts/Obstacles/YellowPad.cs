using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPad : MonoBehaviour {
	//Creating a public Transform for the door gameobject to sit inside.
	public Transform door;
	//Creating a public bool unlocked and setting it to false.
	public bool unlocked = false;

	//When something with the tag of Yellowkeycard enters the pad's collider, it will turn the unlocked to true. This unlocks the pad.
	void OnTriggerEnter (Collider Yellowkeycard)
	{
		if (Yellowkeycard.tag == "Yellowkeycard") {
			Debug.Log ("Unlocked");
			unlocked = true;
		}
	}
}
