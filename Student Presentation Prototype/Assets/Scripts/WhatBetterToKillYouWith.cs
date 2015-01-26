using UnityEngine;
using System.Collections;

public class WhatBetterToKillYouWith : MonoBehaviour 
{
	public Xbox360_Controls player;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	/*void OnTriggerEnter(Collider collision)
	{
		Debug.Log(collision.name);
		
		if(collision.CompareTag("Enemy"))
		{
			Debug.Log("Collision Occured");
			
			if(player.playerAnimator.GetBool("Attack1"))
			{
				Debug.Log("Should be dead");
				//check collider with enemy here
				collision.gameObject.SetActive(false);
			}
		}
	}*/

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.transform.name);

		if(collision.collider.CompareTag("Enemy"))
		{
			Debug.Log("Collision Occured");

			if(player.playerAnimator.GetBool("Attack1"))
			{
				Debug.Log("Should be dead");
				//check collider with enemy here
				collision.gameObject.SetActive(false);
			}
		}
	}
}
