using UnityEngine;
using System.Collections;

public class DespawnPlayer : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.transform.position.y < -5.0f)
		{
			gameObject.SetActive(false);
		}
	}
}
