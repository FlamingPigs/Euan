using UnityEngine;
using System.Collections;

public class CamScroll : MonoBehaviour {
	
	float speed = 50.0f; //temporarily changed from 30 to 50 for class presentation
	float damping = 6.0f;	//to control the rotation 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MoveCamera(Vector3 newCameraPos, GameObject lookAt)
	{
		//Find the new camera position
		Vector3 cameraPosition = newCameraPos;

		//Set what the camera looks at while it moves
		//Look at and dampen the rotation - taken from the SmoothLookAt.js Unity script
		Quaternion rotation;

		if (gameObject.transform.position != lookAt.transform.position) 
		{
			rotation = Quaternion.LookRotation (lookAt.transform.position - gameObject.transform.position);
			gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * damping);
		} 

		//If you want to predetermine a set axis, simply place target.axis = number here

		//Get moving!
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, cameraPosition, speed*Time.deltaTime);
	}
}
