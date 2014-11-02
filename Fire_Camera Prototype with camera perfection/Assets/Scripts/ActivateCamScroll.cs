using UnityEngine;
using System.Collections;

public class ActivateCamScroll : MonoBehaviour {

	public CamScroll activateCameraScrolling;
	public AdventureCamScroll activateAdventureTime;
	bool moving;
	bool adventure;
	bool smooth;
	Vector3 newCameraPosition;
	string objectHit;
	string camTriggerObject;
	float damping = 6.0f;	//to control the camera's rotation

	GameObject gameCamera;
	Vector3 cameraLookAt;
	Transform lookAtMe; //Position for the camera to look at within each arena

	// Use this for initialization
	void Start () 
	{
		moving = false; //We don't want the camera moving before the player does anything
		adventure = false; //Is the player on an adventure?!
		lookAtMe = GameObject.Find("CameraLookAtMe").transform;
		gameCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (moving == true) 
		{
			if (!adventure) 
			{
				//If the camera has reached its destination then stop moving the camera
				if (gameCamera.transform.position == newCameraPosition)
				{
					moving = false;
				}

				activateCameraScrolling.MoveCamera (newCameraPosition, gameObject); //Using the CamScroll script, move the camera to its new position
			} 
			else 
			{
				activateAdventureTime.MoveCamera (gameObject); //Using the AdventureCamScroll script, have the camera follow the player on the x and y axis (z defined in script)
			}
				
		}
		else
		{
			cameraLookAt = (lookAtMe.position + (gameObject.transform.position - lookAtMe.position) / 2); //what we want to look at

			//Smooth down recalculation of what the camera looks at
			//Look at and dampen the rotation - taken from the SmoothLookAt.js Unity script
			if (gameCamera.transform.forward != cameraLookAt) 
			{
				Quaternion rotation = Quaternion.LookRotation (cameraLookAt - gameCamera.transform.position); //difference between what you need to look at and what you are currently looking at
				gameCamera.transform.rotation = Quaternion.Slerp(gameCamera.transform.rotation, rotation, Time.deltaTime * damping);
			}
			else
			{
				//this perhaps never gets used - does it need to be?
				gameCamera.transform.LookAt (cameraLookAt); //change to smooth
			}
		}
	}

	void OnTriggerEnter(Collider wall)
	{
		//Determine the name of the last trigger triggered
		objectHit = wall.gameObject.name; 

		switch (objectHit)
		{
		case "CamShiftPanelRight": //Hit right side of bridge, move to second camera pos
			camTriggerObject = objectHit;
			newCameraPosition = GameObject.Find ("Cam2Pos").transform.position;
			adventure = false;
			lookAtMe = GameObject.Find("CameraLookAtMe2").transform; //Empty GameObjects at centre of each arena
			break;
		case "CamShiftPanelLeft": //Hit left side of bridge, move to first camera pos
			camTriggerObject = objectHit;
			newCameraPosition = GameObject.Find ("Cam1Pos").transform.position;
			adventure = false;
			lookAtMe = GameObject.Find("CameraLookAtMe").transform;
			break;
		case "CamShiftPanelLeft2":
			if(!adventure)
			{
				//If player is not yet adventuring, make them do so and have camera follow
				adventure = true; //Let's go on an adventure! Yeah!
				camTriggerObject = objectHit;
				newCameraPosition = gameObject.transform.position; //gameObject refers to the object the script is attached to, in this case Player1
				lookAtMe = gameObject.transform;
			}
			else
			{
				//If the player has finished adventuring, lock the camera to the new arenas camera position
				adventure = false;
				camTriggerObject = objectHit;
				newCameraPosition = GameObject.Find ("Cam2Pos").transform.position;
				lookAtMe = GameObject.Find("CameraLookAtMe2").transform;
			}

			break;
		case "CamShiftPanelRight2":
			if(!adventure)
			{
				//If player is not yet adventuring, make them do so and have camera follow
				adventure = true;
				camTriggerObject = objectHit;
				newCameraPosition = gameObject.transform.position;
				lookAtMe = gameObject.transform;
			}
			else
			{
				//If the player has finished adventuring, lock the camera to the new arenas camera position
				adventure = false;
				camTriggerObject = objectHit;
				newCameraPosition = GameObject.Find ("Cam3Pos").transform.position;
				lookAtMe = GameObject.Find("CameraLookAtMe3").transform;
			}

			break;
		}

		//Set the camera in motion if the last trigger triggered was a camera movement trigger
		//This checks the last object hit against the 'camTriggerObject' in the switch case
		if(wall.gameObject.name == camTriggerObject)
		{
			if(gameCamera.transform.position != newCameraPosition)
			{
				moving = true;
			}
		}
	}
}
