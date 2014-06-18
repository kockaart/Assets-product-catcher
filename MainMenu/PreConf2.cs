using UnityEngine;
using System.Collections;

public class PreConf2 : MonoBehaviour {

	public Vector3 position;
	public string control;
	
	public float speed = 60;
	
	// Use this for initialization
	void Start () {
		control = "head";
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
