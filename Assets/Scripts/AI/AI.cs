using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	//The destination of the target
	public Transform targetDestination;
	//The destination of the idle
	public Transform idleDestination;
	//A public 
	public IdleSpot ids;
	public Detection dt;
	public Acquired ta;
	public Attacking at;
	public Transform target;
	//A public GameObject to contain the enemyBullet
	public GameObject enemyBullet;
	//A private State called currentstate that will be the current state that the player is in.
	private State currentState;
	//A public enumerator for the States
	public enum State {
		Idle,
		Detect,
		TargetAcquired,
		Disengage,
		Attack,
		Follow
	}
	//A private space for the navmesh to interact withthe agent
	private UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		this.currentState = State.Idle;
		this.agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	void Update(){
	//Switches the states and checks them every frame. If the state is one of the ones underneath, do what is in the state and then break.
		switch (this.currentState) {
		case State.Idle: this.Idle(); break;
		case State.Detect: this.Detect(); break;
		case State.TargetAcquired: this.TargetAcquired(); break;
		case State.Follow: this.Follow(); break;
		case State.Disengage:this.Disengage ();break;
		case State.Attack:this.Attack ();break;
			

		}
	}
	//This is the Idle State for the  AI.
	void Idle()
	{
		//dt(Detection collider) is touched, the state becomes Detect.Otherwise, look at the wall.
		if (dt.touched == true) {
			this.currentState = State.Detect;
		} else {
			//look at the wall
			transform.LookAt (dt.wall);
		}
		//This will make the idlespot that the AI touches false.
		this.ids.touched = false;
	}
	//This is the Detect state for the AI
	void Detect()
	{//When in this state, look at the target 
		transform.LookAt (dt.target);
		//
		if (this.dt.touched == false) {
			this.currentState = State.Idle;
		}
		//if the collider is touched, the state becomes target acquired
		if(ta.touched == true){
			this.currentState = State.TargetAcquired;
		}

	}
			//This is the target acquired state conditions for the AI
	void TargetAcquired()
	{//if the collider is touched, the state becomes follow
		if(this.ta.touched == true){
			this.currentState = State.Follow;
		}
		//if the collider is touched, the state becomes detect
		if(this.ta.touched == false){
			this.currentState = State.Detect;
		}
	}
	//these are the follow state conditions for the AI
	void Follow()
	{//if the collider is touched, the destination of the agent(AI) becomes the target
		if(ta.touched == true){
			this.agent.destination = this.targetDestination.position;
		}
		//if the collider is touched, the state becomes disengage
		if(ta.touched == false){
			this.currentState = State.Disengage;
		}
		//if the collider is touched, the state becomes attack
		if (at.touched == true) {
			this.currentState = State.Attack;
		}
	}
	//These are the attack conditions for the AI
	void Attack(){
		//looks at the player
		transform.LookAt (target);
		//Instantiate the enemyBullet 
		Instantiate (enemyBullet, this.transform.position, this.transform.rotation); 
		//if a collider is touched, the state becomes follow

		if (at.touched == false) {
			this.currentState = State.Follow;
		}
	}
	//these are the disengage conditions for the AI
	void Disengage()
	{//the agent's (AI) destination becomes the idlespot
		this.agent.destination = this.idleDestination.position;
		//if a collider is touched, the state becomes idle
		if(ids.touched == true){
			this.currentState = State.Idle;
		}
		//if a collider is touched, the state becomes follow
		if(ta.touched == true){
			this.currentState = State.Follow;
		}
	}
}
