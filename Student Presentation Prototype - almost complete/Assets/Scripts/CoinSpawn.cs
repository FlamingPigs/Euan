using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinSpawn : MonoBehaviour 
{
	public Transform coin;
	public int numberOfCoins;
	Transform[] spawnedCoins;


	void Start()
	{
		spawnedCoins = new Transform[numberOfCoins];
	}

	// Update is called once per frame
	public void Update() 
	{

	}

	void OnDisable()
	{
		if(!gameObject.activeSelf && gameObject.transform.parent.gameObject.activeInHierarchy)
		{
			for(int i = 0; i < numberOfCoins; i++)
			{
				Vector3 centre = transform.position;
				
				Vector3 coinPos = RandomCircle(centre, 5.0f);
				
				coinPos.y = 5.0f;
				
				spawnedCoins[i] = Instantiate(coin, coinPos, coin.transform.rotation) as Transform;
			}
		}
	}

	Vector3 RandomCircle(Vector3 center, float radius)  
	{
		// create random angle between 0 to 360 degrees 
		float ang = Random.value * 360; 
		Vector3 pos; 
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad); 
		pos.y = center.y; 
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad); 
		return pos; 
	}
}
