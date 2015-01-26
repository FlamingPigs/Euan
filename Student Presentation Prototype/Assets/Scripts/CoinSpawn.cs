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
	void Update () 
	{
		if(Input.GetButtonDown("p1start"))
		{
			for(int i = 0; i < numberOfCoins; i++)
			{
				spawnedCoins[i] = Instantiate(coin, new Vector3(transform.position.x, transform.position.y, transform.position.z), coin.transform.rotation) as Transform;

				float radius = 5000.0f;
				float power = 10000.0f;

				spawnedCoins[i].rigidbody.AddExplosionForce(power, spawnedCoins[i].position, radius, 10.0f);
			}
		}
	}
}
