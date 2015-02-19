using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmIBurningObject : MonoBehaviour {
	
	public Transform fire;
	public Transform burntCrate;
	bool burning;
	float objectBurnTime;
	bool objectCantBurn;
	float objectCantBurnTime;
	float burnEffectTime;
	
	List<Transform> objectBurning;
	List<Transform> burntObject;
	
	// Use this for initialization
	void Start () {
		burning = false;
		objectBurnTime = 0; //player will burn for 5 seconds (at 30fps)
		objectCantBurn = false;
		objectCantBurnTime = 0;
		objectBurning = new List<Transform>();
		burntObject = new List<Transform>();
		burnEffectTime = 0.01f; //Each particle emitter will wait 1 one hundredth of a second before being destroyed
	}
	
	// Update is called once per frame
	void Update () {
		if(burning)
		{
			objectBurnTime += Time.deltaTime;

			if(objectBurning.Count < 10)
			{
				objectBurning.Add(Instantiate(fire, gameObject.transform.position, Quaternion.identity) as Transform);
			}
		}
		
		if(objectBurnTime > 3)
		{
			burning = false;
			objectCantBurnTime += Time.deltaTime;
		}
		
		if(objectCantBurnTime > 3)
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
				if(burntObject.Count < 1)
				{
					burntObject.Add(Instantiate(burntCrate, gameObject.transform.position, Quaternion.identity) as Transform);
				}

				Destroy(objectBurning[i].gameObject);
			}
		}

		objectBurning.Clear();
		Destroy(gameObject);
	}
}
