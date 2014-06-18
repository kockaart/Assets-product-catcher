using UnityEngine;
using System.Collections;

public class OutputSignals : MenuBase {

	public DepthWrapper dw;

	// Use this for initialization
	void Start () {

	}

	void OnGUI() {
		GUI.skin = menuSkin;

		GUI.Box (new Rect(0, Screen.height/4, (float)(Screen.width), (float)(Screen.height)), "Represents the activation of the selectad area");
		
		GUI.Box (new Rect (0, Screen.height/5,Screen.width/4,Screen.height/4), ("Start: " + dw.start));
		GUI.Box (new Rect (0, Screen.height/5*2,Screen.width/4,Screen.height/4), ("Left: " + dw.l));
		GUI.Box (new Rect (0, Screen.height/5*3,Screen.width/4,Screen.height/4), ("Right: " + dw.r));
		GUI.Box (new Rect (0, Screen.height/5*4,Screen.width/4,Screen.height/4), ("Hit: " + dw.hit));
	}

}
