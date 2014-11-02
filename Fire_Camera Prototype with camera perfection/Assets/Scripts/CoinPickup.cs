using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public GUIText scoreText;
	private int score;

	void Start() 
	{
		score = 0;
		scoreText.text = "Booty: " + score.ToString();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider target) 
	{
		if (target.tag == "Pickup") 
		{
			target.gameObject.SetActive (false);
			score += 1;
			scoreText.text = "Booty: " + score.ToString();
		}
	}
}
