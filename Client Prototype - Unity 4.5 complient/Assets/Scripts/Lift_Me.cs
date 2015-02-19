//------------------------------------------------------------------------------
//Daniel Watson
//Lift Me
//Designed to Lift objects like crates above the players head
//and allow them to be dropped
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;

public class Lift_Me: MonoBehaviour 
{
	public GameObject Player;
	private Boolean Can_Lift;
	private Boolean lifted ;

	void Start()
	{
		//init values
		Can_Lift = false;
		lifted = false;
	}
	
	void Update()
	{
		if (lifted == true && Can_Lift == true && gameObject != null)
		{
			Vector3 Player_Pos = (Player.transform.position);
			Player_Pos.y += 3;//increase the Y valu by 3
			//Moves the Cube to the polayers position 3 units above them.
			
			transform.position = (Player_Pos);
			Debug.Log("Pos.y = "+transform.position.y.ToString());
			
		}
		if (lifted == false) 
		{
			Can_Lift = false;
		}
	}
	
	void OnTriggerEnter(Collider target)
	{
		//Debug.Log("Detected collisions");
		if (target.tag == "Player") //if the cube cillides with the player then return true
		{
			lifted = true;
			Player = target.gameObject;
		} 

	}

	public void Get_Buttons(Boolean lift)
	{
		Can_Lift = lift;//comes from Xbox360_Controls.cs 
	}
}
