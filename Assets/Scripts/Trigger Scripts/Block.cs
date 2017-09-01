using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	// When a rocket collides with the block, and their tag is Rocket, destroy the block.
	void OnCollisionEnter(Collision Rocket)
	{
		if (Rocket.gameObject.tag == "Rocket")
			Destroy (gameObject);

			
	
}
}