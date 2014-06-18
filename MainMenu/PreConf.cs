using UnityEngine;
using System.Collections;

public class PreConf : MonoBehaviour {

	public Vector3 position;
	public string control;
	
	public float speed = 4.5f;
	public int time = 100;
	public float sense = 2.5f;
	
	// Use this for initialization
	void Start () {
		control = "head";
		if (Application.loadedLevel != 0)
		{
			DontDestroyOnLoad(transform.gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (speed, time,sense);
	}
}
