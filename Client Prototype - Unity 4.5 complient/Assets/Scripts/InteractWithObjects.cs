//------------------------------------------------------------------------------
//Daniel Watson
//Last Edited : 05/02/15
//Purpose of class:
//	to be attached to players and then be used to determine what object a play is touching
//	and then call an action from said object
//	for now the acton is called "doStuff()"
//	Initially the script was suppost to use "Tags"
//	however this wont do anything useful ans the tag wont retun the object type
//	i.e. if the alpaca is "interactable" and we touch it all the "tags" will tell us 
//	is that it is "Interactable" but wont tell us its an alpaca

// NOTES: The most recent version of the program taken form github
//	wont run
//	Plane to solve - go in tommorow and ask Euan to add this to his game that works
//	and ask him to upload the most recent working version to github
//	named the "xxx_xxx_06/02/15" 
//	thisa will let me access the most recent version and let it run

//	Also Note im not sure how we can implement the controls vbeing "held" etc through the player
// 	interactions - ask Euan/Oscar for logical advice
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	public class InteractWithObjects :MonoBehaviour
	//extends monobehaviour so that we can use the the 
	//Invoke() function
	{
	public GameObject G_Object;
	//since we want to know what object this is it must have a generic name
	public String Object_Type;
	//lets us know what we are touchhing?

	void Start()
	{

	}
	void OnTriggerEnter(Collider target)
	{
		/*
		if (target.tag == "Iteractable") 
		//if the object is the alpaca then retun true
		{

			G_Object = target.gameObject;
			Object_Type = G_Object.GetType ();
			
			switch (Object_Type) 
			{
			case "Alpaca":
				doStuff(Object_Type);
				break;
			case "Weapon":
				doStuff(Object_Type);
				break;
			case "Torch":
				doStuff(Object_Type);
				break;
			case "Crate":
				doStuff(Object_Type);
				break;
			case "Chest":
				doStuff(Object_Type);
				break;
			case "Bridge":
				doStuff(Object_Type);
				break;
			}
		} */
	}
	void doStuff(String Object_Type)
	{
		Debug.Log("Doing the Stuffs from : "+Object_Type);
	}
}


