using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
	 
	//Setting up a public foat speed of 100 for the rocket to move at said speed.
	public float speed = 2.0f;

	void Update () {
		this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
		 Destroy(gameObject, 5);
		
	}
	//If the rocket hits a block, it will get destroyed.
	void OnCollisionEnter(Collision Block)
	{
		if (Block.gameObject.tag == "Bullseye") {
			Destroy (gameObject);
		}

	}


}














