using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	private float spawnDelay;
	private float delay;
	public Transform enemy;

	// Use this for initialization
	void Start ()
	{
		delay = 10.0f;
		spawnDelay = delay;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (spawnDelay > 0.0f)
		{
			spawnDelay -= Time.deltaTime;
		}
	}

	void OnTriggerStay(Collider target)
	{
		if (target.tag == "Player" && spawnDelay <= 0.0f)
		{
			Instantiate(enemy, transform.position, Quaternion.identity);
			spawnDelay = delay;
		}
	}
}
