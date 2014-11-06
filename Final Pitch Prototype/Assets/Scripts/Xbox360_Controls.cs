//------------------------------------------------------------------------------
//daniel watson
//xbxo 360 controller controls
//used to take in Xbox 360 controls 
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;

public class Xbox360_Controls : MonoBehaviour 
{


	 public int count;
	 public Lift_Me lifter;
	 protected Boolean Can_Lift;
	void start()
	{
		//init values
		Can_Lift = false;
	}
	void Update ()
	{
		UserInputs();

	}

	void OnGUI()
	{
		//for testing purposes
		//GUI.Label (new Rect (10, 10, 150, 20), "Attack.exe called :" +count.ToString());
	}
		

	
	void UserInputs()
	{
		if(Input.GetButton("A"))//gets the A button
		{
			Debug.Log("A Button!");
		}
		if(Input.GetButton("B"))//gets the B button
		{
			Debug.Log("B Button!");
		}
		if(Input.GetButton("X"))//Gets the X button
		{
			Can_Lift = true;

			if(lifter != null)
			{
				lifter.Get_Buttons(Can_Lift);// When X is Pushed Pick up an object that is clode by
				lifter.gameObject.rigidbody.useGravity = false;
			}

			Debug.Log("X Button!");
			
		}
		if(Input.GetButton("Y"))//Gets the Y button
		{
			Can_Lift = false;

			if(lifter != null)
			{
				lifter.Get_Buttons(Can_Lift);//When Y is pushed Put down an Object that is Picked up
				lifter.gameObject.rigidbody.useGravity = true;
			}

			Debug.Log("Y Button!");
		}
		
	}
}