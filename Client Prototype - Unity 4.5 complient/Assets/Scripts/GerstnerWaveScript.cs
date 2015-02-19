using UnityEngine;
using System.Collections;

public class GerstnerWaveScript : MonoBehaviour 
{
	public float[] waveSteepness = new float[4] {0, 0, 0, 0};
	public float[] waveAmplitude = new float[4] {0, 0, 0, 0};
	public float[] waveWavelength = new float[4] {0, 0, 0, 0};
	public Vector2[] waveDirection = new Vector2[4] {new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0)};
	public Vector2[] waveCentre = new Vector2[4] {new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0)};
	public float[] waveSpeed = new float[4] {0, 0, 0, 0};
	public int[] waveCircular = new int[4] {0, 0, 0, 0};

	float time;
	public Texture texture;

	// Use this for initialization
	void Start () 
	{
		time = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.renderer.material.SetVector("steepness", new Vector4(waveSteepness[0], waveSteepness[1], waveSteepness[2], waveSteepness[3]));
		gameObject.renderer.material.SetVector("amplitude", new Vector4(waveAmplitude[0], waveAmplitude[1], waveAmplitude[2], waveAmplitude[3]));
		gameObject.renderer.material.SetVector("wavelength", new Vector4(waveWavelength[0], waveWavelength[1], waveWavelength[2], waveWavelength[3]));
		gameObject.renderer.material.SetVector("circular", new Vector4(waveCircular[0], waveCircular[1], waveCircular[2], waveCircular[3]));
		gameObject.renderer.material.SetVector("direction1", new Vector4(waveDirection[0].x, waveDirection[0].y, waveDirection[1].x, waveDirection[1].y));
		gameObject.renderer.material.SetVector("direction2", new Vector4(waveDirection[2].x, waveDirection[2].y, waveDirection[3].x, waveDirection[3].y));
		gameObject.renderer.material.SetVector("circular", new Vector4(waveCircular[0], waveCircular[1], waveCircular[2], waveCircular[3]));
		gameObject.renderer.material.SetVector("centre1", /*gameObject.transform.position.x +*/ new Vector4(waveCentre[0].x, waveCentre[0].y, waveCentre[1].x, waveCentre[1].y));
		gameObject.renderer.material.SetVector("centre2", /*gameObject.transform.position.y +*/ new Vector4(waveCentre[2].x, waveCentre[2].y, waveCentre[3].x, waveCentre[3].y));
		gameObject.renderer.material.SetVector("speed", new Vector4(waveSpeed[0], waveSpeed[1], waveSpeed[2], waveSpeed[3]));

		gameObject.renderer.material.SetFloat("deltaTime", time);
		gameObject.renderer.material.SetFloat("PI", Mathf.PI);
		gameObject.renderer.material.SetTexture("_MainTex", texture);

		time += Time.deltaTime;
	}
}
