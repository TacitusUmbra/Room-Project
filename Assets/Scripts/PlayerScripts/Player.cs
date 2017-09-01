using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	//Making a place to set the PlayerInput so that the player can get access to the inputs
	public PlayerInput pi;
	//Making a place to set the PlayerConfig so that the player can get access to the configurations
	public PlayerConfig pc;
	//creating a private transform anchortransform to be used as a parent
	private Transform anchorTransform;
	//the max pitch of the camera when it is moving along the pitch
	public float maxPitch = 20f;
	//the min pitch of the camera when it is moving along the pitch
	public float minPitch = 340f;
	//connecting to the character controller and calling it cc
	private CharacterController cc;
	//creating a private camera
	private Camera cam;
	//On awake, or before the game is started, the character controller is cc, teh camera is cam, and the anchor is 
	public LayerMask camLayerMask;
	//Creating a private float for the camera distance
	private float originalCameraDistance;
	//Creating a private State called currenState that will determine the currentstate the player is in.
	private State currentState;
	//Creating a private State called defaultState and making the state Idle
	private State defaultState = State.Idle;
	//Creating a private float that is originalSpeed 
	private float originalSpeed;
	//Creating a private Animator and calling it anim
	private Animator anim;
	//Creating a private Vector3 lastPosition for the animations in game
	private Vector3 lastPosition;
	//Creating a privage float velocity and setting it at 0
	private float velocity = 0f;
	//Creating a Vector3 called nully that equals a new Vector3 for the sake of nullifying an animation when going upwards
	private Vector3 nullY = new Vector3(1,0,1);
	//Creating a private Grabdis that will speak to the Grabdis script and nearestGrab
	private Grabdis nearestGrab;
	//Creating a private Grabdis that will speak to the Grabdis script and currentGrab
	private Grabdis currentGrab;
	//Creating a priavate Inventory called myInventory to access the Inventory script
	private Inventory myInventory;
	//State machine with the Idle, Walk, Jump, and Run
	public enum State {
		Idle,
		Walk,
		Jump,
		Run
	}
	//Here is a header with all the plyer movements, walkspeed, runspeed, sensitivity, the gravity, and the jumpstrength
	[Header("Player Movement")]
		public float walkspeed = 10f;
		public float runSpeed = 20f;
		public float sensitivity = 100f;
		private float gravity = 5.0f;
		public float jumpStrength = -100f;


	void Awake () {
		//The anim will access the Animator inside of the child of the parent it is in
		this.anim = GetComponentInChildren<Animator>();
		//The currentState is the defaultState on Awake, which is Idle
		this.currentState = this.defaultState;
		//cc is short for the CharacterController 
		this.cc = this.GetComponent<CharacterController>();
		//cam is short for the Camera we will get inside of the child
		this.cam= this.GetComponentInChildren<Camera>();
		//anchorTransform is short for the camera transform in the parent
		this.anchorTransform = this.cam.transform.parent;
		//the originalCameraDistance is goign to set the distance of the camera that is goes back to if nothing is in the way
		this.originalCameraDistance = (this. cam.transform.parent.position -  this.cam.transform.position).magnitude;
		//jumpStrenght represents the power of the jump on the Awake
	 	jumpStrength = -75f;

	}
	
	void Update ()
	{
		//Checks my inventory each frame.
		this.myInventory = GetComponent<Inventory>();
		//Checks the lastPosition  each frame.
		this.lastPosition = Vector3.Scale (this.transform.position, nullY);
		//The states that switch between one another, update on each frame.
		switch (this.currentState) {
			case State.Idle: this.Idle(); break;
			case State.Walk: this.Walk(); break;
			case State.Jump: this.Jump(); break;
			case State.Run: this.Run(); break;
		}
		//This calls the gravity in the game. When it is not grounded, the gravity will increase to give the impression that the player is picking up velocity.
		//If it isn't falling, the gravity is stable. It does these each frame.
		if (cc.isGrounded == false) 
			gravity += 5f / 2;
		 else if (gravity > 0f)
				gravity = 1f;
			this.pi.direction.y -= gravity * Time.deltaTime;
		
		//Calling on the AdjustCamera each frame.
		this.AdjustCamera();
		//Updating the direction each frame.
		Vector3 direction = (transform.rotation * this.pi.direction) * walkspeed;
		direction *= Time.deltaTime;
		//Checking the motion each frame
		this.cc.Move (direction);
		//Checking the rotation each frame
		this.transform.rotation *= Quaternion.Euler (0f, this.pi.rotation.x, 0f);
		//Checking the camera's rotation each frame
		this.cam.transform.parent.rotation *= Quaternion.Euler (this.pi.rotation.y, 0f, 0f);

		// ( <-> max pitch && <180) || (min pitch <-> 360 && > 180)
		Vector3 angles = this.anchorTransform.localRotation.eulerAngles;
		if (angles.x >= 0 && angles.x < 180f && angles.x > this.maxPitch) {
			this.anchorTransform.localRotation = Quaternion.Euler (this.maxPitch, 0f, 0f);
		} else if (angles.x < this.minPitch && angles.x > 180f && angles.x <= 360) {
			this.anchorTransform.localRotation = Quaternion.Euler (this.minPitch, 0f, 0f);

		}		

		this.velocity = (this.lastPosition - Vector3.Scale (this.transform.position, nullY)).magnitude;

	}
	//This is the Idle. It will have these conditions when inside the Idle state.
	void Idle () {
		//This is the animation when in the Idle state
		this.anim.SetFloat ("Speed", this.velocity);
		//If the character is moving in the x or z direction, the currenstate will become Walk
		if (this.pi.direction.x != 0 || this.pi.direction.z != 0) {
			this.currentState = State.Walk;
		} else {
			//This will enable the player to grab items
			if (Input.GetKeyDown (this.pc.grab)) {
				if (this.nearestGrab != null) {
					this.Grab (this.nearestGrab);
				}
				//this will enable the character to store the items they have grabbed into the Bag
			} else if (Input.GetKeyDown (this.pc.store)) {
				if (this.currentGrab != null) {
					this.Bag (this.currentGrab);
				}
				//The character will drop the Redkeycard when they press a specific key.
			} else if (Input.GetKeyDown (this.pc.drop1)) {
				this.myInventory.UseResource (Inventory.Keycards.Redkeycard, this.transform.position);
				//The character will drop the Yellowkeycard when they press a specific key.
			} else if (Input.GetKeyDown (this.pc.drop2)) {
				this.myInventory.UseResource (Inventory.Keycards.Yellowkeycard, this.transform.position);
				//The character will drop the Bluekeycard when they press a specific key.
			} else if (Input.GetKeyDown (this.pc.drop3)) {
				this.myInventory.UseResource (Inventory.Keycards.Bluekeycard, this.transform.position);
				//If the character is grounded and they press the key that corresponds with jump in the PlayerConfig, they will jump and teh currenState will become jump.
			} else if (Input.GetKeyDown (this.pc.jump) && cc.isGrounded) {
				gravity = jumpStrength;
				this.currentState = State.Jump;
			}
		}
	}
	//This is the Walk. It will have these conditions when inside the Walk state.
	void Walk () {
		//This is the animation that will play when the character is in the Walk state.
		this.anim.SetFloat("Speed",this.velocity);
		//If the player ceases to move in the x or z direction, the currentState will become Idle.
		if (this.pi.direction.x == 0 && this.pi.direction.z == 0) {
			this.currentState = State.Idle;
			//If the character is grounded and the player presses the key that corresponds with jump in the PlayerConfig,
			//they will jump and the currentState will become jump.
		} else {
			if (Input.GetKeyDown(this.pc.jump) && cc.isGrounded) {
				gravity = jumpStrength;
				this.currentState = State.Jump;
				//If the player presses down the key corresponding to the run in the PlayerConfig, the player will begin to run and the currentState will be Run.
			} else if (Input.GetKeyDown(this.pc.run)) {
				
				this.originalSpeed = this.walkspeed;
				this.currentState = State.Run;
			}
		}
	}
	//This is the Jump. It will have these conditions in the Jump State.
	void Jump () {
		//If the character is moving in the x or z direction, the currenstate will become Walk.

		if (this.cc.isGrounded == false && this.pi.direction.magnitude > 0.1f) {
		} else {
			this.pi.direction = Vector3.zero;
			this.currentState = State.Walk;
		}
	}
	//This is the Run. It will have these conditions in the Run state.
	void Run () {
		//This is the animation that will play during the Run state.
		this.anim.SetFloat("Speed",this.velocity);

		//If the character is moving in the x or z direction, the currenstate will become Walk.
		if (Input.GetKey(KeyCode.LeftShift)) {
			this.walkspeed = this.runSpeed;
		} else {
			this.walkspeed = this.originalSpeed;
			this.currentState = State.Walk;
		}
	}

	//Adjusts the camera if it collides with objects
	void AdjustCamera(){

		//Calls the RaycastHit hit to use it easier.
		RaycastHit hit;
		//direction is the camera position minus the camera's parent position
		Vector3 direction = this.cam.transform.position - this.cam.transform.parent.position;
		//Setting up the origin
		Vector3 origin = this.cam.transform.parent.position;
		// If the camera hits something, it will move closer to the origin, otherwise, it will move away.
        if (Physics.SphereCast(origin, 0.35f, direction, out hit, this.originalCameraDistance, this.camLayerMask.value))			
		{

			Vector3 newVector = direction.normalized  * hit.distance;
			this.cam.transform.position = newVector + this.cam.transform.parent.position;
	
		
		}else{
			Vector3 newVector = direction.normalized * this.originalCameraDistance;
			this.cam.transform.position = newVector + this.cam.transform.parent.position;
		}
	}
	//When the object is grabbed, we can ungrab it if it is parented.
	public void Grab (Grabdis grabdis) {
		if (grabdis.transform.parent == this.transform) {
			grabdis.transform.SetParent(null);
			this.currentGrab = null;
			//Otherwise, you can grab the item and it will be parented.
		} else {
			grabdis.transform.SetParent(this.transform);
			this.currentGrab = grabdis;
		}
	}
	//The nearestGrab becomes what is grabbed
	public void SetNearestGrab (Grabdis grabdis) {
		this.nearestGrab = grabdis;
	}
	//destroys the object that is grabbed when it is added to the Bag
	public void Bag (Grabdis grabdis) {

		InventoryResource ir = grabdis.GetComponent<InventoryResource> ();
		if (ir != null) {
			this.myInventory.AddResource (ir);
			Destroy (grabdis.gameObject);
		}
	}
}