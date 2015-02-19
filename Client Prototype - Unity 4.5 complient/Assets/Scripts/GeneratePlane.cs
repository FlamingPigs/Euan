using UnityEngine;
using System.Collections;

public class GeneratePlane : MonoBehaviour
{
	Vector3[] vertices;
	Vector2[] UV;
	int[] triangles;
	Vector3[] normals;
	public Mesh plane;
	public Material material;
	public int meshHeight = 100;
	public int meshWidth = 100;
	int index = 0;
	int triangleIndex = 0;
	
	// Use this for initialization
	void Start ()
	{
		Mesh planeMesh = new Mesh ();
		vertices = new Vector3[meshWidth * meshHeight];
		UV = new Vector2[meshWidth * meshHeight];
		triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
		normals = new Vector3[meshWidth * meshHeight];

		//calculate vertices for plane
		for(int i = 0; i < meshWidth; i++)
		{
			for(int j = 0; j < meshHeight; j++)
			{
				//get index for array
				index = (meshHeight * j) + i;
				vertices[index] = new Vector3(i, 0, j);
				normals[index] = new Vector3(0, 1, 0); //just have all normals point up to begin with, since mesh sits in x, z plane
			}
		}

		int test = 0;
		while (test < UV.Length) 
		{
			UV[test] = new Vector2(vertices[test].x/meshHeight, vertices[test].z/meshHeight);
			test++;
		}

		for(int j = 0; j < (meshHeight - 1); j++)
		{
			for(int i = 0; i < (meshWidth - 1); i++)
			{
				int index1 = (j * meshHeight) + i;
				int index2 = (j * meshHeight) + (i + 1);
				int index3 = ((j + 1) * meshHeight) + i;
				int index4 = ((j + 1) * meshHeight) + (i + 1);
				
				// Get three vertices from the face.
				//vertex1[0] = vertices[index1].x;
				triangles[triangleIndex] = index1;
				triangleIndex++;

				//vertex1[1] = vertices[index1].y;
				triangles[triangleIndex] = index3;
				triangleIndex++;

				//vertex1[2] = vertices[index1].z;
				triangles[triangleIndex] = index2;
				triangleIndex++;
				
				//vertex2[0] = vertices[index2].x;
				triangles[triangleIndex] = index2;
				triangleIndex++;

				//vertex2[1] = vertices[index2].y;
				triangles[triangleIndex] = index3;
				triangleIndex++;

				//vertex2[2] = vertices[index2].z;
				triangles[triangleIndex] = index4;
				triangleIndex++;
			}
		}

		planeMesh.vertices = vertices;
		planeMesh.uv = UV;
		planeMesh.triangles = triangles;
		planeMesh.normals = normals;

		gameObject.renderer.material = material;
		gameObject.GetComponent<MeshFilter>().mesh = planeMesh;
	}

	// Update is called once per frame
	void Update ()
	{
	}
}
