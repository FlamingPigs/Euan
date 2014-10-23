using UnityEngine;
using System.Collections;

public class AdventureCamScroll : MonoBehaviour {

	float speed = 50.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveCamera(string object1)
	{
		//Find the new camera position
		GameObject cameraPosition = GameObject.Find(object1);
		
		//Set the target as said new camera position
		Vector3 target = cameraPosition.transform.position;
		
		//If you want to predetermine a set axis, simply place target.axis = number here
		target.y = target.y + 15.0f;
		target.z = -5.0f;
		
		//Get moving!
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed*Time.deltaTime);
	}
}
