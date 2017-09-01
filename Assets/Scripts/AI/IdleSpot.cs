using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSpot : MonoBehaviour {
	
	//This is a public bool touched set to false.
	public bool touched = false;

	//If something with the tag guard enters the collider, touched will become true.
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Guard") 
		{
			touched = true;
		}
	}
}

