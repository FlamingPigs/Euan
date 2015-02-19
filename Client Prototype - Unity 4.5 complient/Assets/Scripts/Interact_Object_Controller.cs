//------------------------------------------------------------------------------
//Daniel Watson
//Last Edited : 05/02/15
//Purpose of Script:
//	 To be attached to "Interacable" objects
//	This will determine what kind of press has happened to the object
//	This class is called from "InteractWithObjects.cs" which attached to players
//	
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interact_Object_Controller :MonoBehaviour
{
	public float time_to_hold ;
	//note if time_to_hold is zero then the item will pickup instantly
	private float init_time;//the time when we pressed Y
	private float time_held;//the current time of the game
	
	public Boolean delay_press;
	public float delay_time;
	//will control whether or not the press is a single press instantly picking up
	//or a single press with a delay on picking up
	public Boolean holder;
	//or a hold
	public Boolean lift;
	public GameObject Player;
	// need to know when a playe is near the object
	private Boolean in_range;


	//new variables for the new class structure
	public String Object_Type;
	void Start()
	{
		//init values that need inits
		lift = false;
		in_range = true;
	}
	//to be called from the xbox 360 controller class
	void Interact(GameObject G_Object )
	{
		/*Object_Type = G_Object.GetType ();

		switch (Object_Type) 
		{
		case "Alpaca":
			doStuff();
			break;
		case "Weapon":
			doStuff();
			break;
		case "Torch":
			doStuff();
			break;
		case "Crate":
			doStuff();
			break;
		case "Chest":
			doStuff();
			break;
		case "Bridge":
			doStuff();
			break;
		}*/
		//==============================THE CODE BELOW IS FOR REFERENCE ONLY========================================
		/*if (in_range) //should only be true when the player is next to the object
		{
			//----------------------------For "HOLD" objects----------------------------
			if(holder)//if the object is a "hold to use" object
			{ 
				time_held = Time.time;
				
				//when we let go of Y Init time held is 0 so
				if (init_time >0)
				{
					//time held : being the current time
					//init time held : being the time when we first pressed Y
					if((time_held - init_time) >= time_to_hold)
					{
						doStuff();
					}
				}
			}
			//----------------------------^For "HOLD" objects^----------------------------
			
			//----------------------------For "Delay Press" objects----------------------------		
			if(delay_press)//when delayed pickups are enabled
			{
				//invoke runs a function AFTER a set time
				Invoke(doStuff (),delay_time);
				//in this case the time is delay time which we set
			}
			//----------------------------^For "Delay Press" objects^----------------------------		
			//----------------------------For "Normal Presses" objects----------------------------
			if(!holder && !delay_press)
			{
				doStuff ();
			}
			//----------------------------^For "Normal Presses" objects^----------------------------		
		}
		//==============================THE CODE ABOVE IS FOR REFERENCE ONLY========================================
		*/
	}
	void doStuff()
	{
		
		//this is where  the code will go to manipulate objects
		Debug.Log("Doing the Stuffs from : "+Object_Type);
		
	}
	
	void OnTriggerExit(Collider target)
	{
		/*if (target = "Player")
		{
			//when the player leaves the area stop doing stuff
		
			in_range = false;
		}*/
	}
	
}