using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	//List of keycards
	public enum Keycards {
		Redkeycard,
		Bluekeycard,
		Yellowkeycard
	}
	//The public struct called Keychain
	public struct Keychain {
		//a place to set the prefabs
		public GameObject prefab;
		//the int called count
		public int count;
	}

	//A public dictionary with the Key, Value, and Keys short for the dictionary
	public Dictionary<Keycards, Keychain> Keys;

	void Start () 
	{
		//On Start, the Keys become a new dictionary with the type of Key and Value in the dictionary
		this.Keys = new Dictionary<Keycards, Keychain> ();
	}
	//If the dictionary contains a type of resource, it will add the resource if it containts the proper typeOfResource, 
	//increases the count, and adds the prefab
	public void AddResource (InventoryResource ir) 
	{
		if (this.Keys.ContainsKey (ir.typeOfResource)) 
		{
			this.Keys [ir.typeOfResource].count++;
		} 
		else 
		{
			this.Keys.Add (ir.typeOfResource, new Keychain () { prefab = ir.resourcePrefab, count = 1 });
		}
	}
	//If the dictionary contains a type of resource, this will instantiate a resource if the count is larger than 0. 
	//It will reduce the count an remove the typeOfResource.
	public void UseResource (Keycards typeOfResource, Vector3 position) 
	{
		if (this.Keys.ContainsKey (typeOfResource)) 
		{
			if (this.Keys [typeOfResource].count > 0) 
			{
				Instantiate (this.Keys [typeOfResource].prefab, position, Quaternion.identity);
				Keychain ru = this.Keys [typeOfResource];
				ru.count--;
				this.Keys [typeOfResource] = ru;
				if (this.Keys [typeOfResource].count == 0)
				{
					this.Keys.Remove (typeOfResource);
				}
			}
		}
	}
}