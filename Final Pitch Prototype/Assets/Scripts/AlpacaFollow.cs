using UnityEngine;
using System.Collections;

//Script written by Oskar Lauritzen with audio additions by Euan Watt
public class AlpacaFollow : MonoBehaviour {

	private bool following;
	public GameObject leader;
	private float leash;
	public AudioClip[] alpacaSounds;
	AudioSource[] audioSources;

	// Use this for initialization
	void Start () 
	{
		leash = 5.0f;
		following = false;
		leader = GameObject.FindWithTag("Player");

		LoadAudio ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (following) 
		{
			Follow();
		}
	}

	void OnTriggerEnter(Collider target) 
	{
		if (target.tag == "Player") 
		{
			following = false;
			ScreamForMeYaFilthyAnimal();
		}
	}

	void OnTriggerExit(Collider target) 
	{
		if (target.tag == "Player") 
		{
			following = true;
			leader = target.gameObject;
		}
	}

	void Follow()
	{
		// Maybe use the trigger instead of a new leash variable? Readability vs less variables or something. This way seemed easier for now
		float dist = Vector3.Distance(leader.transform.position, transform.position);
		if (dist > leash)
		{
			float catchUp = dist - leash;
			float stayGroundedYouCrazyAnimal = transform.position.y; // Needs better fix. Maybe disable jumping while leading instead? Freeze y-position gets overrode by script :(
			transform.position = Vector3.MoveTowards (transform.position, leader.transform.position, catchUp);
			transform.position = new Vector3 (transform.position.x, stayGroundedYouCrazyAnimal, transform.position.z);

			Vector3 targetPosition = leader.transform.position;
			targetPosition.y = 0.0f; //'lock' the leaders y position so that it doesn't rotate to look at the leader when they are jumping

			transform.LookAt(targetPosition);
		}
		// Idle movement - needs to look more natural
		/*else
		{
			transform.position = new Vector3 (transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y, transform.position.z + Random.Range(-0.1f, 0.1f));
		}*/
	}

	void ScreamForMeYaFilthyAnimal() //Totally added a Home Alone 2 reference, you know you love it
	{
		if (audio.isPlaying) 
		{
			return; //don't want to play more than one you silly goose
		}

		audio.clip = audioSources [Random.Range (0, alpacaSounds.Length)].clip; //choose a sound at random from the list
		audio.Play ();
	}

	void LoadAudio() //Taken from here: http://forum.unity3d.com/threads/how-to-have-multiple-audio-sources-on-one-object.181968/
	{
		audioSources = new AudioSource[alpacaSounds.Length];

		int i = 0;

		while (i < alpacaSounds.Length) 
		{
			GameObject child = new GameObject("Al-pack-a");
			child.transform.parent = gameObject.transform;
			audioSources[i] = child.AddComponent("AudioSource") as AudioSource;
			audioSources[i].clip = alpacaSounds[i];
			i++;
		}
	}
}