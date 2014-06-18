using UnityEngine;
using System.Collections;

public class PlayerScript : MenuBase {
    
	public KinectPointController kpc;
	public Texture logo;

    public int theScore = 0;
	public int eggs = 0;
	public int timeawake = 0;
	public int time = 0;
	public float speed = 4.7f; //just to make it a lil more configurable
	public float currentHorizontal = 0;
	private float ratio;
	private float posx;
	public float sense=4f;
	public string control;
	public GameObject Head;
	public GameObject Hand_Left;
	public GameObject Hand_Right;
//	public GameObject Shoulder_Left;
//	public GameObject Shoulder_Right;
//	public GameObject Elbow_Left;
//	public GameObject Elbow_Right;

	void Awake () {
		timeawake = (int)Time.fixedTime;

	}

	void Update () {
        //These two lines are all there is to the actual movement..
      //  float moveInput = Input.GetAxis("Horizontal") * Time.deltaTime * 3; 
		
		//float dist = Vector3.Distance(other.position, transform.position);
		
		control = PlayerPrefs.GetString("control");
		if (control == "head") posx = Head.transform.localPosition.x;
		if (control == "left") posx = Hand_Left.transform.localPosition.x;
		if (control == "right") posx = Hand_Right.transform.localPosition.x;
		
		currentHorizontal = Mathf.Lerp(currentHorizontal, posx, Time.deltaTime);
//		transform.Translate (currentHorizontal * speed * Time.deltaTime, 0, 0);
		float moveInput = -posx * speed * Time.deltaTime * PlayerPrefs.GetFloat("sense");

	//*	transform.position -= new Vector3(moveInput, 0, 0);
		transform.position = new Vector3((posx*PlayerPrefs.GetFloat("sense")), transform.position.y, transform.position.z);
		time = (int)Time.fixedTime - timeawake;


        //Restrict movement between two values
		ratio = (float)(Screen.width/(float)300);
        if (transform.position.x <= -ratio || transform.position.x >= ratio)
        {
            float xPos = Mathf.Clamp(transform.position.x, -ratio, ratio); //Clamp between min -2.5 and max 2.5
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
	}

    //OnGUI is called multiple times per frame. Use this for GUI stuff only!
    void OnGUI()
    {
		GUI.skin = menuSkin;
		GUI.Box(new Rect(0, Screen.height *4 /5, Screen.width /5, Screen.height/5), logo);
        //We display the game GUI from the playerscript
        //It would be nicer to have a seperate script dedicated to the GUI though...
		if (time < PlayerPrefs.GetInt("time"))
		{
			GUILayout.Label("Score: " + theScore + "/"+ eggs);
			GUILayout.Label("Time: " + time + " sec");
		}
		else
		{
			int score=100*theScore/eggs;
			GUI.Box (new Rect(0, 270, (float)(Screen.width), (float)(Screen.height)), "You catched " + theScore + " eggs out of " + eggs);
			GUI.Box (new Rect(0, 370, (float)(Screen.width), (float)(Screen.height)), "Success: " + score +"%");

			if(GUILayout.Button("Try again") ){
				Application.LoadLevel(1);
			}
			if(GUILayout.Button("Back to menu") ){
			//	Destroy(GameObject.Find("Conf")); //Otherwise we'd have two of these..
				Application.LoadLevel(0);
			}
		}
    }    
}
