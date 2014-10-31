using UnityEngine;
using System.Collections;

public class AlpacaFollow : MonoBehaviour {

	private bool following;
	public GameObject leader;
	private float speed;

	// Use this for initialization
	void Start () 
	{
		speed = 5.0f;
		following = false;
		leader = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (following) 
		{
			float step = Time.deltaTime * speed;
			transform.position = Vector3.MoveTowards (transform.position, leader.transform.position, step);
		}
	}

	void OnTriggerEnter(Collider target) 
	{
		if (target.tag == "Player") 
		{
			following = false;
		}
	}

	void OnTriggerExit(Collider target) 
	{
		if (target.tag == "Player") 
		{
			following = true;
		}
	}
}