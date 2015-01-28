using UnityEngine;
using System.Collections;
using GAF.Core;

public class StartMenuAudio : MonoBehaviour 
{
	public AudioClip[] sounds;
	private AudioSource[] audioSources;
	public GAFMovieClip splashAnimation;
	public bool musicPlayed = false;

	// Use this for initialization
	void Start () 
	{
		LoadAudio();
	}

	// Update is called once per frame
	void Update () 
	{
		if(splashAnimation.getCurrentFrameNumber() == 51)
		{
			OAlpacaMyAlpaca();
		}
	}

	void OAlpacaMyAlpaca() //O CAPTAIN! MY CAPTAIN!
	{
		if (audio.isPlaying) 
		{
			return; //don't want to play more than one you silly goose
		}
		
		audio.clip = audioSources [Random.Range (0, sounds.Length)].clip; //choose a sound at random from the list
		audio.Play ();

		StartCoroutine(IsMyAudioDoneYet()); //wait until end of audio clip to flip boolean and fade in GUI
	}

	IEnumerator IsMyAudioDoneYet()
	{
		yield return new WaitForSeconds(audio.clip.length);

		musicPlayed = true;
	}
	
	void LoadAudio() //Taken from here: http://forum.unity3d.com/threads/how-to-have-multiple-audio-sources-on-one-object.181968/
	{
		audioSources = new AudioSource[sounds.Length];
		
		int i = 0;
		
		while (i < sounds.Length) 
		{
			GameObject child = new GameObject("Al-capt-a");
			child.transform.parent = gameObject.transform;
			audioSources[i] = child.AddComponent("AudioSource") as AudioSource;
			audioSources[i].clip = sounds[i];
			i++;
		}
	}
}
