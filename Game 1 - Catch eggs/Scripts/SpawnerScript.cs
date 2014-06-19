using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

	public PlayerScript myPlayerScript;

	public Transform eggPrefab;
	public Transform chicken;
	public Vector3 spawnPos;
	public float addXPos;
	
	private float nextEggTime = 1.0f;
	//time between eggs
	public float spawnRate = 6f;
	public float rate = 1f;
	public int timeawake = 0;
	public int time = 0;

	void Awake () {
		timeawake = (int)Time.fixedTime;
		//spawnRate = PlayerPrefs.GetFloat("speed");
		spawnRate = 6;
	}

	void Update () {
		time = (int)Time.fixedTime - timeawake;
		//spawnRate = PlayerPrefs.GetFloat("speed");
		//if (time < PlayerPrefs.GetInt("time") && nextEggTime < Time.time)
		if (time < 60 && nextEggTime < Time.time)
		{
			//Speed up the spawnrate for the next egg
			rate *= 0.99f;
			rate = Mathf.Clamp(rate, 0.7f, 99f);

			SpawnEgg();
			nextEggTime = Time.time + (float)spawnRate*rate;
			myPlayerScript.eggs++;

		}
	}
	
	void SpawnEgg()
	{
		addXPos = Random.Range(-2.4f, 2.4f);
		spawnPos = transform.position + new Vector3(addXPos,0,0);
		chicken.transform.position =new Vector3(addXPos,3.9f,-6.824616f);
		Instantiate(eggPrefab, spawnPos, Quaternion.identity);

	}
}
