using UnityEngine;
using System.Collections;

public class MainMenuScript : MenuBase {

	public Texture first;
	public Texture second;
	public Texture logo;
	
    void Awake()
    {       
        //Make this script persistent(Not destroy when loading a new level)
//        DontDestroyOnLoad(this);

        Time.timeScale = 1.0f; //In case some game does not UN-pause..
    }

	void OnGUI () {    
		GUI.skin = menuSkin;
		GUI.Box(new Rect( (Screen.width )* 7/ 13, Screen.height/2-300, 520, 100),"PLEASE SELECT A GAME");
		GUI.Box(new Rect( (Screen.width )* 7/ 14, Screen.height/2-100,(Screen.width ) / 2, Screen.height/2), logo);

		//Detect if we're in the main menu scene
        if (Application.loadedLevel == 0)
        {
            MainMenuGUI();
        }
        else
        {
            //Game scene
            InGameGUI();
        }	
	}

    void StartGame(int nr)
    {
        Application.LoadLevel(nr);
    }

    void InGameGUI()
    {
        //Top-right
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, 50));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
//        if (GUILayout.Button("Back to menu"))
//        {
//            Destroy(gameObject); //Otherwise we'd have two of these..
//            Application.LoadLevel(0);
//        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    public GUIStyle invisibleButton;

    void MainMenuGUI()
    {
        int leftPix = (Screen.width ) / 2;
        int topPix = (Screen.height ) / 2;

		if (GUI.Button(new Rect(0,0,leftPix, topPix), first))
		{
			GetComponent<menu1>().enabled = true;
			this.enabled = false;
		}
		if (GUI.Button(new Rect(0, topPix,leftPix, topPix), second))
        {
		//	GetComponent<menu2>().enabled = true;
			GetComponent<menu2>().enabled = true;
			this.enabled = false;
        }
//        if (GUI.Button(new Rect(leftPix + 204 * 2, topPix, 204, 158), "", invisibleButton))
//        {
//            StartGame(3);
//        }
//
//
//        if (GUI.Button(new Rect(leftPix, topPix + 290, 204, 158), "", invisibleButton))
//        {
//            StartGame(4);
//        }
//        if (GUI.Button(new Rect(leftPix + 204, topPix + 290, 204, 158), "", invisibleButton))
//        {
//            StartGame(5);
//        }
		if (GUI.Button(new Rect( (Screen.width ) / 2, Screen.height-50,320, 50), "www.e-motion.tk"))
        {
            Application.OpenURL("http://www.e-motion.tk");
        }


        GUI.color = Color.black;

        GUILayout.BeginArea(new Rect(Screen.width/2-150, Screen.height/2-100, 300, 200));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
      
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
	}
}
