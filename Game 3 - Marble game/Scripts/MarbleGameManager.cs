using UnityEngine;
using System.Collections;

public enum MarbleGameState {playing, won,lost };

public class MarbleGameManager : MenuBase
{
    public static MarbleGameManager SP;

    private int totalGems;
    private int foundGems;
    private MarbleGameState gameState;
	public int timeawake = 0;
	public int time = 0;

	public Texture logo;

    void Awake()
    {
        SP = this; 
        foundGems = 0;
        gameState = MarbleGameState.playing;
        totalGems = GameObject.FindGameObjectsWithTag("Pickup").Length;
		timeawake = (int)Time.fixedTime;
        Time.timeScale = 1.0f;
    }
	void Update () {
		if (time <= PlayerPrefs.GetInt("time2")) time = (int)Time.fixedTime - timeawake;
	}

	void OnGUI () {
		GUI.skin = menuSkin;
		GUI.Box(new Rect(0, Screen.height *4 /5, Screen.width /5, Screen.height/5), logo);
	    GUILayout.Label(" Found gems: "+foundGems+"/"+totalGems );
		int score=100*foundGems/totalGems;

        if (gameState == MarbleGameState.lost)
        {
			if(GUILayout.Button("Try again") ){
				Application.LoadLevel(2);
			}
			if(GUILayout.Button("Back to menu") ){
				Application.LoadLevel(0);
			}
        }
        else if (gameState == MarbleGameState.won)
        {
			GUI.Box (new Rect(0, 270, (float)(Screen.width), (float)(Screen.height)), "You coolected all the gems and won!");
			GUI.Box (new Rect(0, 370, (float)(Screen.width), (float)(Screen.height)), "Time: " + time);

            GUILayout.Label("You won!");
			if(GUILayout.Button("Play again") ){
				Application.LoadLevel(2);
			}
			if(GUILayout.Button("Back to menu") ){
				Application.LoadLevel(0);
			}
        }
		else if (time < PlayerPrefs.GetInt("time2"))
		{
			GUILayout.Label("Time: " + time + " sec");
		}
		else if(time >= PlayerPrefs.GetInt("time2"))
		{
			GUI.Box (new Rect(0, 270, (float)(Screen.width), (float)(Screen.height)), "You collected " + foundGems + " gems out of " + totalGems);
			GUI.Box (new Rect(0, 370, (float)(Screen.width), (float)(Screen.height)), "Success: " + score +"%");

			if(GUILayout.Button("Try again") ){
				Application.LoadLevel(2);
			}
			if(GUILayout.Button("Back to menu") ){
				Application.LoadLevel(0);
			}
		}
	}

    public void FoundGem()
    {
        foundGems++;
        if (foundGems >= totalGems)
        {
            WonGame();
        }
    }

    public void WonGame()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = MarbleGameState.won;
    }

    public void SetGameOver()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = MarbleGameState.lost;
    }
}
