using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	//Setting up a public foat speed of 50 for the enemy bullet to move at said speed.
	public float speed = 50.0f;

	void Update () {
		//The bullet will move forward at the speed designated and it will destroy itself after three seconds
		this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
		Destroy(gameObject, 3);

	}
	//When the player enters the collider of the bullet, they will be teleported into the designated position.
	void OnTriggerEnter (Collider Player)
	{
		if(Player.tag =="Player")
			Player.transform.position = new Vector3(400f,2f,3f);


	}

}
