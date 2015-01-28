using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewAmIBurning : MonoBehaviour 
{
	public Transform fire;
	public bool burning;
	bool playerCantBurn;
	float deloreanEffectTime;
	bool fireBurning;

	Transform[] deloreanEffect = new Transform[240];

	// Use this for initialization
	void Start () 
	{
		burning = false;
		playerCantBurn = false;
		deloreanEffectTime = 0.01f; //Each particle emitter will wait 1 one hundredth of a second before being destroyed

		for (int i = 0; i < deloreanEffect.Length; i++) 
		{
			deloreanEffect[i] = (Instantiate (fire, new Vector3 (-100, -100, -100), Quaternion.identity) as Transform);
			deloreanEffect[i].renderer.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(burning)
		{
			StartCoroutine(PlaceDeloreanEffect());

			burning = false;
		}
	}
	
	void OnTriggerEnter(Collider fireCollider)
	{
		if(fireCollider.gameObject.CompareTag("Fire"))
		{
			if(!playerCantBurn)
			{
				burning = true;
				playerCantBurn = true;
			}
		}
	}
	
	IEnumerator FireBurning()
	{
		for(int i = deloreanEffect.Length - 1; i >= 0; i--)
		{	
			if(deloreanEffect[i] != null)
			{
				yield return new WaitForSeconds(deloreanEffectTime);
				deloreanEffect[i].renderer.enabled = false;
				deloreanEffect[i].position = new Vector3(-100, -100, -100); //reset to original position... or throw it waaaaaay off screen
			}
		}

		playerCantBurn = false;
	}

	IEnumerator PlaceDeloreanEffect()
	{
		for (int i = deloreanEffect.Length - 1; i >= 0; i--) 
		{
			yield return new WaitForSeconds(deloreanEffectTime);
			deloreanEffect[i].position = gameObject.transform.position;
			deloreanEffect[i].renderer.enabled = true;
		}

		yield return new WaitForSeconds(1.5f); //Don't delete the trail immediately
		StartCoroutine(FireBurning());
	}
}

