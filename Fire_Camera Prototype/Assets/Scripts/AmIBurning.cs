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

	List<Transform> deloreanEffect; //make this a list!

	// Use this for initialization
	void Start () {
		burning = false;
		playerBurnTime = 0; //player will burn for 5 seconds (at 30fps)
		playerCantBurn = false;
		playerCantBurnTime = 0;
		deloreanEffect = new List<Transform>();
		deloreanEffectTime = 0.01f; //The fire will burn for 1 second
	}

	// Update is called once per frame
	void Update () {
		if(burning)
		{
			playerBurnTime++;
			deloreanEffect.Add(Instantiate(fire, gameObject.transform.position, Quaternion.identity) as Transform);
		}

		if(playerBurnTime > 150)
		{
			burning = false;
			playerCantBurnTime++;
		}

		if(playerCantBurnTime > 150)
		{
			playerBurnTime = 0;
			playerCantBurn = false;

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
		deloreanEffect.Reverse();

		for(int i = deloreanEffect.Count - 1; i >= 0; i--)
		{
			yield return new WaitForSeconds(deloreanEffectTime);

			if(deloreanEffect[i] != null)
			{
				Destroy(deloreanEffect[i].gameObject);
			}
		}
	}
}
