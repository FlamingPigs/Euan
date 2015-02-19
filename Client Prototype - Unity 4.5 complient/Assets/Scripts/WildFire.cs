using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WildFire : MonoBehaviour {

	public Transform fire;
	int numberOfObjects = 20;
	bool creatingFire;

	List<Transform> fireObject; // = new GameObject[];

	// Use this for initialization
	void Start () {
		fireObject = new List<Transform>();
		creatingFire = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!creatingFire)
		{
			StartCoroutine(CreateFire());
		}
	}

	IEnumerator CreateFire()
	{
		if(Input.GetButton("p1start")) //GetButton has to be used as it's the only one that doesn't have its flag reset each frame
		{
			creatingFire = true;
			for(int radius = 0; radius < 10; radius++)
			{
				for (int i = 0; i < numberOfObjects; i++)
				{
					//need to have a check here for position of new fire object, if one already exists here then continue;
					yield return new WaitForSeconds(0.02f);
					float angle = i * Mathf.PI * 2 / numberOfObjects;
					Vector3 pos = (new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius) + new Vector3(gameObject.transform.position.x + 25.0f, gameObject.transform.position.y, gameObject.transform.position.z + 20.0f); //Put in centre of arena
					fireObject.Add (Instantiate(fire, pos, Quaternion.identity) as Transform);
				}
			}

			fireObject.Reverse();

			for(int i = fireObject.Count - 1; i >= 0; i--)
			{
				yield return new WaitForSeconds(0.01f);

				if(fireObject[i] != null)
				{
					Destroy(fireObject[i].gameObject);
				}
			}

			fireObject.Clear();

			creatingFire = false;
		}
	}
}
