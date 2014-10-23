using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmIBurningObject : MonoBehaviour {
	
	public Transform fire;
	//public Transform arena;
	bool burning;
	float objectBurnTime;
	bool objectCantBurn;
	float objectCantBurnTime;
	float burnEffectTime;
	
	List<Transform> objectBurning;
	
	// Use this for initialization
	void Start () {
		burning = false;
		objectBurnTime = 0; //player will burn for 5 seconds (at 30fps)
		objectCantBurn = false;
		objectCantBurnTime = 0;
		objectBurning = new List<Transform>();
		burnEffectTime = 0.01f; //The fire will burn for 1 second

		//Physics.IgnoreCollision(gameObject.collider, arena.collider); 
	}
	
	// Update is called once per frame
	void Update () {
		if(burning)
		{
			objectBurnTime++;

			if(objectBurning.Count < 10)
			{
				objectBurning.Add(Instantiate(fire, gameObject.transform.position, Quaternion.identity) as Transform);
			}
		}
		
		if(objectBurnTime > 150)
		{
			burning = false;
			objectCantBurnTime++;
		}
		
		if(objectCantBurnTime > 150)
		{
			objectBurnTime = 0;
			objectCantBurn = false;
			
			StartCoroutine(FireBurning());
		}
	}
	
	void OnCollisionEnter(Collision fireCollider)
	{
		Debug.Log("OBJECT HIT" + fireCollider.gameObject.name);

		if(fireCollider.gameObject.tag == "Player")
		{
			if(fireCollider.gameObject.GetComponent<AmIBurning>().burning)
			{
				if(!objectCantBurn)
				{
					burning = true;
					objectCantBurn = true;
				}
			}
		}
	}
	
	IEnumerator FireBurning()
	{
		objectCantBurnTime = 0;
		objectBurning.Reverse();
		
		for(int i = objectBurning.Count - 1; i >= 0; i--)
		{
			yield return new WaitForSeconds(burnEffectTime);
			
			if(objectBurning[i] != null)
			{
				Destroy(objectBurning[i].gameObject);
			}
		}

		Destroy(gameObject);
	}
}
