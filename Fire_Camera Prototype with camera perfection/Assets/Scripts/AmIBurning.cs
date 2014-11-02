using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmIBurning : MonoBehaviour {

	public Transform fire;
	public bool burning;
	float playerBurnTime;
	bool playerCantBurn;
	float playerCantBurnTime;
	float deloreanEffectTime;
	bool fireBurning;

	List<List<Transform>> deloreanEffectList;
	List<Transform> deloreanEffect;

	// Use this for initialization
	void Start () {
		burning = false;
		playerBurnTime = 0; //player will burn for 5 seconds
		playerCantBurn = false;
		playerCantBurnTime = 0;
		deloreanEffectList = new List<List<Transform>>();
		deloreanEffect = new List<Transform>();
		deloreanEffectTime = 0.01f; //Each particle emitter will wait 1 one hundredth of a second before being destroyed
	}

	// Update is called once per frame
	void Update () {
		if(burning)
		{
			playerBurnTime += Time.deltaTime;
			deloreanEffect.Add(Instantiate(fire, gameObject.transform.position, Quaternion.identity) as Transform);
		}

		if(playerBurnTime > 5)
		{
			burning = false;
			playerCantBurnTime += Time.deltaTime;
		}

		if(playerCantBurnTime > 3)
		{
			playerBurnTime = 0;
			playerCantBurn = false;
			deloreanEffectList.Add(deloreanEffect);

			StartCoroutine(FireBurning());
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
		playerCantBurnTime = 0;

		foreach(List<Transform> list in deloreanEffectList)
		{
			if(list.Count != 0)
			{
				list.Reverse(); //remove last added object first

				for(int i = list.Count - 1; i >= 0; i--)
				{	
					if(list[i] != null)
					{
						yield return new WaitForSeconds(deloreanEffectTime);

						Destroy(list[i].gameObject);
					}
				}
		
				list.Clear();
			}
		}

		//deloreanEffectList.Clear();

//		for(int i = deloreanEffect.Count - 1; i >= 0; i--)
//		{
//			yield return new WaitForSeconds(deloreanEffectTime);
//
//			if(deloreanEffect[i] != null)
//			{
//				Destroy(deloreanEffect[i].gameObject);
//			}
//		}
//
//		deloreanEffect.Clear();
	}
}
