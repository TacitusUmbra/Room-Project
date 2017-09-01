using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPad : MonoBehaviour {
	//Creating a public Transform for the door gameobject to sit inside.
	public Transform door;
	//Creating a public bool unlocked and setting it to false.
	public bool unlocked = false;

	//When something with the tag of Redkeycard enters the pad's collider, it will turn the unlocked to true. This unlocks the pad.
	void OnTriggerEnter (Collider Redkeycard)
	{
		if (Redkeycard.tag == "Redkeycard") {
			Debug.Log ("Unlocked");
			unlocked = true;
		}
	}
}
