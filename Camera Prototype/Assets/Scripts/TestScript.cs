using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	public Transform target;
	public Transform target2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraLookAt = target.position + (target2.position - target.position)/2;

		transform.LookAt(cameraLookAt);
	}
}
