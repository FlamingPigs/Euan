using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMedianCalculation : MonoBehaviour 
{
	public Transform[] players = new Transform[8];
	List<Transform> activePlayers;
	Vector3 cameraLookAt;
	int numberOfPlayers;
	new public CamScroll camera;
	Vector3 originalPos;

	// Use this for initialization
	void Start () 
	{
		activePlayers = new List<Transform>();

		foreach(Transform pos in players)
		{
			if(pos.gameObject.activeSelf)
			{
				activePlayers.Add(pos.transform);
				Debug.Log("Added " + pos.name);
			}
		}

		originalPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(Transform pos in activePlayers)
		{
			if(!pos.gameObject.activeSelf)
			{
				if(activePlayers.Contains(pos))
				{
					activePlayers.Remove(pos);
					break;
				}
			}
		}

		cameraLookAt = originalPos;

		for(int i = 0; i < activePlayers.Count; i++)
		{
			cameraLookAt += activePlayers[i].position;
		}

		if(activePlayers.Count != 0)
		{
			gameObject.transform.position = cameraLookAt/activePlayers.Count;
		}

		camera.MoveCamera(new Vector3(gameObject.transform.position.x, 18.9f, -22.7f), gameObject);
	}
}
