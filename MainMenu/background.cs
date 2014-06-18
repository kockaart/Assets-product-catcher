using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {

	public Texture back;
	// Use this for initialization
	void Start () {
	
	}
	void OnGUI () {    
		GUI.Box (new Rect (0, 0, Screen.width, Screen.height), back);

	}
	// Update is called once per frame
	void Update () {
	
	}
}
