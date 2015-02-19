using UnityEngine;
using System.Collections;

//Script written by Oskar Lauritzen with audio additions by Euan Watt
public class AlpacaFollow : MonoBehaviour {

	private bool following;
	private bool owned; // does it currently have a leader?
	public GameObject leader;
	private float leash;
	public AudioClip[] alpacaSounds;
	private AudioSource[] audioSources;
	private Vector3[] idlePoints;
	private int idleTarget;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () 
	{
		leash = 5.0f;
		following = false;
		owned = false;
		leader = GameObject.FindWithTag("Player");

		agent = GetComponent<NavMeshAgent>();

		LoadAudio ();

		SetIdlePoints ();
		idleTarget = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float dist = Vector3.Distance(leader.transform.position, transform.position);

		if (following /*&& dist > leash*/) 
		{
			Follow(dist);
		}
		else
		{
			Idle();
		}
	}

	void SetIdlePoints()
	{
		NavMeshHit pointOnMesh;
		idlePoints = new Vector3[4];

		for (int i = 0; i < 4; i++)
		{
			idlePoints[i] = new Vector3 (transform.position.x + Random.Range(-leash, leash), transform.position.y, transform.position.z + Random.Range(-leash, leash));
			NavMesh.SamplePosition(idlePoints[i], out pointOnMesh, leash, 1);
			idlePoints[i] = pointOnMesh.position;
		}
	}

	void OnTriggerEnter(Collider target) 
	{
		if (target.tag == "Player" && !owned) 
		{
			leader = target.gameObject;
			owned = true;
		}
		else if  (target.gameObject == leader)
		{
			following = false;
			SetIdlePoints ();
			ScreamForMeYaFilthyAnimal();
		}
	}

	void OnTriggerExit(Collider target) 
	{
		if (target.gameObject == leader)
		{
			following = true;
			ScreamForMeYaFilthyAnimal();
		}
	}

	void Follow(float dist)
	{
		// Maybe use the trigger instead of a new leash variable? Readability vs less variables or something. This way seemed easier for now
//		float catchUp = dist - leash;
//		float stayGroundedYouCrazyAnimal = transform.position.y; // Needs better fix. Maybe disable jumping while leading instead? Freeze y-position gets overrode by script :(
//		transform.position = Vector3.MoveTowards (transform.position, leader.transform.position, /*catchUp*/0.1f);
//		transform.position = new Vector3 (transform.position.x, stayGroundedYouCrazyAnimal, transform.position.z);
//
//		Vector3 targetPosition = leader.transform.position;
//		targetPosition.y = 0.0f; //'lock' the leaders y position so that it doesn't rotate to look at the leader when they are jumping
//
//		transform.LookAt(targetPosition);

		agent.SetDestination (leader.transform.position);
	}

	void Idle()
	{
		//transform.position = Vector3.MoveTowards (transform.position, idlePoints [idleTarget], 0.1f);
		//transform.LookAt(idlePoints[idleTarget]);

		agent.SetDestination (idlePoints [idleTarget]);

		if (Vector3.Distance(idlePoints [idleTarget], transform.position) <= 1.0f)
		{
			idleTarget++;
		}
		if (idleTarget >= 4)
		{
			idleTarget = 0;
		}
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