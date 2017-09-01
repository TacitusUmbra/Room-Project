using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePad : MonoBehaviour {
	//Creating a public Transform for the door gameobject to sit inside.
	public Transform door;
	//Creating a public bool unlocked and setting it to false.
	public bool unlocked = false;
	//When something with the tag of Bluekeycard enters the pad's collider, it will turn the unlocked to true. This unlocks the pad.
	void OnTriggerEnter (Collider Bluekeycard)
	{
		if (Bluekeycard.tag == "Bluekeycard") {
			Debug.Log ("Unlocked");
			unlocked = true;
		}
	}
}
