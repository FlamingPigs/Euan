using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour {

	public Text scoreText;
	private int score;
	public AudioClip sounds;

	void Start() 
	{
		score = 0;

		if(gameObject.activeSelf)
		{
			scoreText.text = score.ToString();
		}
		else
		{
			scoreText.gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(!gameObject.activeSelf)
		{
			scoreText.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider target) 
	{
		if (target.tag == "Pickup") 
		{
			target.gameObject.SetActive (false);
			score += 1;
			scoreText.text = score.ToString();
			audio.Play();
		}
	}
}
