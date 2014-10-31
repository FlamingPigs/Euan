using UnityEngine;
using System.Collections;

public class ActivateCamScroll : MonoBehaviour {

	public CamScroll activateCameraScrolling;
	public AdventureCamScroll activateAdventureTime;
	bool moving;
	bool adventure;
	bool smooth;
	string newCameraPosition;
	string objectHit;
	string camTriggerObject;

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
		if(moving == true)
		{
			if(!adventure)
			{
				cameraLookAt = gameObject.transform.position; //look at the player

				activateCameraScrolling.MoveCamera(newCameraPosition, gameCamera); //Using the CamScroll script, move the camera to its new position

				//If the camera has reached its destination then stop moving the camera
				if(gameCamera.transform.position == GameObject.Find(newCameraPosition).transform.position)
				{
					moving = false;
				}
			}
			else
			{
				activateAdventureTime.MoveCamera(newCameraPosition); //Using the AdventureCamScroll script, have the camera follow the player on the x and y axis (z defined in script)
			}
		}

		cameraLookAt = (lookAtMe.position + (gameObject.transform.position - lookAtMe.position)/2); //what we want to look at
		gameCamera.transform.LookAt(cameraLookAt);
	}

	void OnTriggerEnter(Collider wall)
	{
		//Determine the name of the last trigger triggered
		objectHit = wall.gameObject.name; 

		switch (objectHit)
		{
		case "CamShiftPanelRight": //Hit right side of bridge, move to second camera pos
			camTriggerObject = objectHit;
			newCameraPosition = "Cam2Pos";
			adventure = false;
			lookAtMe = GameObject.Find("CameraLookAtMe2").transform; //Empty GameObjects at centre of each arena
			break;
		case "CamShiftPanelLeft": //Hit left side of bridge, move to first camera pos
			camTriggerObject = objectHit;
			newCameraPosition = "Cam1Pos";
			adventure = false;
			lookAtMe = GameObject.Find("CameraLookAtMe").transform;
			break;
		case "CamShiftPanelLeft2":
			if(!adventure)
			{
				//If player is not yet adventuring, make them do so and have camera follow
				adventure = true; //Let's go on an adventure! Yeah!
				camTriggerObject = objectHit;
				newCameraPosition = gameObject.name; //gameObjectr refers to the object the script is attached to, in this case Player1
				lookAtMe = gameObject.transform;
			}
			else
			{
				//If the player has finished adventuring, lock the camera to the new arenas camera position
				adventure = false;
				camTriggerObject = objectHit;
				newCameraPosition = "Cam2Pos";
				lookAtMe = GameObject.Find("CameraLookAtMe2").transform;
			}

			break;
		case "CamShiftPanelRight2":
			if(!adventure)
			{
				//If player is not yet adventuring, make them do so and have camera follow
				adventure = true;
				camTriggerObject = objectHit;
				newCameraPosition = gameObject.name;
				lookAtMe = gameObject.transform;
			}
			else
			{
				//If the player has finished adventuring, lock the camera to the new arenas camera position
				adventure = false;
				camTriggerObject = objectHit;
				newCameraPosition = "Cam3Pos";
				lookAtMe = GameObject.Find("CameraLookAtMe3").transform;
			}

			break;
		}

		//Set the camera in motion if the last trigger triggered was a camera movement trigger
		//This checks the last object hit against the 'camTriggerObject' in the switch case
		if(wall.gameObject.name == camTriggerObject)
		{
			moving = true;
		}
	}
}
