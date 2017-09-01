using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

	//Setting up the position1, position2, and the speed at which the object will move
	public Vector3 pos1 = new Vector3(0,4,0);
	public Vector3 pos2 = new Vector3(0,-4,0);
	public float speed = 1.0f;

	//Transform the object from position1 to position2 using the lerp to interpolate between the two points. It will move between the angle Sine of speed and 1, divided by two.
	void Update() {
		transform.position = Vector3.Lerp (pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);

	}
}