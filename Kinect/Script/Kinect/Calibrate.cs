using UnityEngine;
using System.Collections;

public class Calibrate : MenuBase {
		
	protected Rect readyRect;
	public DepthWrapper dw;

	
	// Use this for initialization
	new void Start () {
		base.Start();			
	}


	void OnGUI() {		
		GUI.skin = menuSkin;
		dw.lowest = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(10, 50, 350, 30), (float)dw.lowest, 1, 6000));
		dw.highest = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(10, 130, 350, 30), (float)dw.highest, 1, 6000));
		dw.right = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2-155, Screen.height/2, 150, 30), (float)dw.right, 250, 320));
		dw.left = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2+15, Screen.height/2, 150, 30), (float)dw.left, 0 , 60));
		dw.top = Mathf.RoundToInt(GUI.VerticalSlider(new Rect(Screen.width/2, Screen.height/2-155, 30, 150), (float)dw.top, 50, 0));
		dw.bottom = Mathf.RoundToInt(GUI.VerticalSlider(new Rect(Screen.width/2, Screen.height/2+5, 30, 150), (float)dw.bottom,240, 180  ));
		dw.xDepth = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(10, 220, 150, 30), (float)dw.xDepth, 0, 1000));
		dw.yDepth = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(10, 300, 150, 30), (float)dw.yDepth, 0, 1000));

		string rotatex;
		if (dw.rotatex == true) {
			rotatex = "Rotate around X ON";		
		}
		else rotatex = "Rotate around X OFF";	
		dw.rotatex = GUI.Toggle(new Rect(10, 360, 220, 30), dw.rotatex, rotatex);

		string rotatey;
		if (dw.rotatey == true) {
			rotatey = "Rotate around Y ON";		
		}
		else rotatey = "Rotate around Y OFF";	
		dw.rotatey = GUI.Toggle(new Rect(10, 400, 220, 30), dw.rotatey, rotatey);

		GUI.Box (new Rect (10, 10,150,30), ("DepthMIN: "+dw.lowest));
		GUI.Box (new Rect (10, 90,150,30), ("DepthMAX: "+dw.highest));
		GUI.Box (new Rect (10, 180,200,30), ("X Depth Angle: "+dw.xDepth));
		GUI.Box (new Rect (10, 260,200,30), ("Y Depth Angle: "+dw.yDepth));
		GUI.Box (new Rect (Screen.width/2-155, Screen.height/2-35,100,30), ("Left: " + dw.right));
		GUI.Box (new Rect (Screen.width/2+100, Screen.height/2-35,100,30), ("Right: "+dw.left));
		GUI.Box (new Rect (Screen.width/2+15, Screen.height/2-155,80,30), ("Top: "+dw.top));
		GUI.Box (new Rect (Screen.width/2+25, Screen.height/2+130,110,30), ("Bottom: "+dw.bottom));

		readyRect = new Rect (Screen.width-120, 80,100,40);
		
		//Next Scene
		if(GUI.Button(readyRect,"Ready")){
//			gameObject.AddComponent("OutputSignals");
			GetComponent<OutputSignals>().enabled = true;
			Destroy (this);

		}
		GUI.Box (new Rect(0, 0, (float)(Screen.width), (float)(Screen.height)), "Configuration");
	}
}
