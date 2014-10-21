using UnityEngine;
using System.Collections;

public class ZoomScript : MonoBehaviour {

	GameObject[] target = new GameObject[16];

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
