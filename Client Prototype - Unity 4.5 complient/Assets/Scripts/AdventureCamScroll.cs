using UnityEngine;
using System.Collections;

public class AdventureCamScroll : MonoBehaviour {

	float speed = 50.0f;
	float damping = 6.0f;	//to control the camera's rotation

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveCamera(GameObject lookAt)
	{
		//Find the new camera position
		Vector3 cameraPosition = lookAt.transform.position;
		
		//If you want to predetermine a set axis, simply place target.axis = number here
		cameraPosition.y = cameraPosition.y + 15.0f;
		cameraPosition.z = -5.0f;
		
		//Get moving!
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, cameraPosition, speed*Time.deltaTime);

		//Set what the camera looks at while it moves
		//Look at and dampen the rotation - taken from the SmoothLookAt.js Unity script
		if (gameObject.transform.position != lookAt.transform.position) 
		{
			Quaternion rotation = Quaternion.LookRotation (lookAt.transform.position - gameObject.transform.position);
			gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * damping);
		} 
	}
}
