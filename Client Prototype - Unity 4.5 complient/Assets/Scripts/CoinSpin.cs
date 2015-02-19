using UnityEngine;
using System.Collections;

public class CoinSpin : MonoBehaviour 
{
	float rotationAmount = 45.0f;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 rot = transform.rotation.eulerAngles; 
		
		rot.y = rot.y + rotationAmount * Time.deltaTime; 
		
		if(rot.y > 360) 
		{
			rot.y -= 360; 
		}
		else if(rot.y < 360) 
		{
			rot.y += 360;
		}
		
		transform.eulerAngles = rot;
	}
}