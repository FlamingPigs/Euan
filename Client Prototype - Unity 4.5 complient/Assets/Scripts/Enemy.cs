using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private bool chasing;
	private Collider victim;

	private NavMeshAgent agent;

	public Animator enemyAnimator;

	// Use this for initialization
	void Start ()
	{
		
		agent = GetComponent<NavMeshAgent>();
		chasing = false;
		enemyAnimator = GetComponentInChildren<Animator>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (chasing)
		{
			agent.SetDestination(victim.transform.position);

			enemyAnimator.SetFloat("Speed", 1.0f);
		}
	}

	void OnTriggerEnter(Collider target) 
	{
		if (target.tag == "Player" && !chasing) 
		{
			victim = target;
			chasing = true;
		}
	}
}
