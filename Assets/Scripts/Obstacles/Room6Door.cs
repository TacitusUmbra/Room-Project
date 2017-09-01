using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room6Door : MonoBehaviour {
	//Creating a public means of contacting the RedPad, YellowPad, and BluePad to be told if they are unlocked.
	public RedPad rp;
	public YellowPad yp;
	public BluePad bp;

	void Update(){

		//If the RedPad, Bluepad, and yelloPad are unlocked, destroy the gameObject.
		if ((rp.unlocked == true) && (yp.unlocked == true) && (bp.unlocked == true)) {
			Destroy(gameObject);
		} 

	}

}
