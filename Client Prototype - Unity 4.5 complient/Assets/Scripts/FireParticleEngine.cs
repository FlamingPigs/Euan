using UnityEngine;
using System.Collections;

public class FireParticleEngine : MonoBehaviour 
{
	/*
	 * For particle system
	 * instantiate an array of particles - maybe 100? will need to play around with number of particles needed at once to look good
	 * 
	 * give particle a start position
	 * give it a 'life' time
	 * give it a velocity
	 * give it an angle (0 for standard fire, more for embers)
	 * 
	 * Change the position of the particle using the velocity over delta time
	 * Change its size over delta time
	 * Change its alpha value over delta time
	 * Reduce its life over delta time (link this to alpha)
	 * 
	 * Keep track of particles life, once dead, swap with last 'alive' particle in array
	 * Update only 'alive' particles (number of alive particles = array size, when particle dies reduce number by 1 and reiterate through list)
	 * When a new particle is needed, select first 'dead' particle, reset all variables and treat it as new
	 * 
	 * Consider physics for embers - simple boolean flip to control physics parameters
	 * 
	 * FUTURE: REDUCE VERTICAL RANGE/ LIFE OF PARTICLE DEPENDENT ON RADIUS (FURTHER AWAY FROM ORIGIN, LESS LIFE)
	 */

	Transform[] fire = new Transform[100]; 
	public Transform fireParticle;
	public float particleLife;

	class Particle
	{
		public Transform particle;
		public float life = 0;
		public bool alive = false;
	};

	public float velocity;
	public float angle;
	public float radius;
	Particle[] particleList = new Particle[100];

	// Use this for initialization
	void Start () 
	{
		//initialise a new particle class for each element in the particle array
		for (int i = 0; i < particleList.Length; i++)
		{
			particleList[i] = new Particle();
		}

		//create particle objects and push them onto the array
		for (int i = 0; i < fire.Length; i++)
		{
			fire[i] = Instantiate(fireParticle, Random.insideUnitSphere * 10 + gameObject.transform.position - new Vector3(0.0f, -100.0f, 0.0f), Quaternion.identity) as Transform;

			particleList[i].particle = fire[i];
			particleList[i].life = particleLife;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//start the fire with 25 alive particles
		for (int i = 0; i < particleList.Length/4; i++) 
		{
			particleList[i].alive = true;
		}

		foreach(Particle aliveParticle in particleList)
		{
			//check if particle is alive, if it is then render it
			if(!aliveParticle.alive)
			{
				aliveParticle.particle.gameObject.renderer.enabled = false;
				PositionParticle(aliveParticle);
			}
			else
			{
				//Render and move the particle
				aliveParticle.particle.gameObject.renderer.enabled = true;
				MoveParticle(aliveParticle);

				//Change alpha of particle
				Color particleColour = aliveParticle.particle.renderer.material.color;
				particleColour.a = aliveParticle.life/particleLife;
				aliveParticle.particle.renderer.material.color = particleColour;

				//Change size of particle
				aliveParticle.particle.localScale = new Vector3(aliveParticle.life/particleLife, aliveParticle.life/particleLife, aliveParticle.life/particleLife);
			}
		}

		if(Input.GetButton("p0action"))
		{
			for (int i = 0; i < particleList.Length/2; i++) 
			{
				particleList[i].alive = true;
			}
		}
	}

	//NOT FINISHED
	void ThroughTheFireAndFlames() //yes, that is a Dragonforce reference
	{
		//iterate through list to find first dead particle, if particle is less than 25 into array, bump to end of array and make 25th particle alive
		//when making new element 'alive', reset all associated variables for it
		int count = 0;

		foreach(Particle aliveParticle in particleList)
		{
			if(!aliveParticle.alive)
			{
				Particle tempParticle = aliveParticle;
			}

			count++;
		}
	}

	void PositionParticle(Particle particles)
	{
		Vector3 initialPosition = gameObject.transform.position;
		Vector2 posInRadius = Random.insideUnitCircle * radius;

		initialPosition.x += posInRadius.x;
		initialPosition.z += posInRadius.y;
		particles.particle.transform.position = initialPosition;
	}

	void MoveParticle(Particle particles)
	{
		Vector3 yPosition = particles.particle.transform.position;
		yPosition.y += velocity * Time.deltaTime;
		particles.particle.transform.position = yPosition;
		particles.life--;

		if (particles.life < 0)
		{
			particles.alive = false;
		}
	}
}
