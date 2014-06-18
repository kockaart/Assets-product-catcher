using UnityEngine;
using System.Collections;

public class menu2: MenuBase {
	
	private bool curropt1=true; private bool curropt2=false; private bool curropt3=false;
	public Texture logo;

	void OnGUI() {		
		GUI.skin = menuSkin;		
		
		if(GUI.Toggle(new Rect(Screen.width/2-360,Screen.height-600,200,40),
		              curropt1, new GUIContent("Head")))
		{
			PlayerPrefs.SetString("control2", "head");
			curropt1=true;
			curropt2=false;
			curropt3=false;
		}
		if(GUI.Toggle(new Rect(Screen.width/2-360,Screen.height-550,200,40),
		              curropt2, new GUIContent("Left Hand")))
		{
			PlayerPrefs.SetString("control2", "left");
			curropt2=true;
			curropt1=false;
			curropt3=false;
		}
		if(GUI.Toggle(new Rect(Screen.width/2-360,Screen.height-500,220,50),
		              curropt3, new GUIContent("Right Hand")))
		{
			PlayerPrefs.SetString("control2", "right");	
			curropt3=true;
			curropt1=false;
			curropt2=false;	
		}		
		GUI.Box (new Rect(Screen.width/2-90, 30, 180, (float)(Screen.height)), "OPTIONS");
		
		PlayerPrefs.SetFloat("sense2", GUI.HorizontalSlider(new Rect(Screen.width/2-340, Screen.height-400, 150, 30), PlayerPrefs.GetFloat("sense2"), -0.7f , 0.7f));
		GUI.Box (new Rect (Screen.width/2-360, Screen.height-370,380,60), ("Distance: " + ((int)(PlayerPrefs.GetFloat("sense2")*10f)-0)));
		
		PlayerPrefs.SetFloat("speed2", GUI.HorizontalSlider(new Rect(Screen.width/2-340, Screen.height-300, 150, 30), PlayerPrefs.GetFloat("speed2"), 45f , 100f));
		GUI.Box (new Rect (Screen.width/2-360, Screen.height-270,240,60), ("Speed: " + ((int)(PlayerPrefs.GetFloat("speed2")*1)-44)));
		PlayerPrefs.SetInt("time2", 10*(int)GUI.HorizontalSlider(new Rect(Screen.width/2-340, Screen.height-200, 150, 30), PlayerPrefs.GetInt("time2")/10, 4f , 15f));
		GUI.Box (new Rect (Screen.width/2-360, Screen.height-170,360,60), ("Time: " + PlayerPrefs.GetInt("time2")+ " seconds"));
				
		if (GUI.Button (new Rect (Screen.width/2+100, Screen.height/2-50,200,100), "Play")) {
			Application.LoadLevel (2);
		}

		GUI.Box(new Rect(Screen.width *3 / 4, Screen.height *3 /4, Screen.width /4, Screen.height/4), logo);
		if (GUI.Button(new Rect( (Screen.width ) / 2-160, Screen.height-50,320, 50), "www.e-motion.tk"))
		{
			Application.OpenURL("http://www.e-motion.tk");
		}
	}
}
