using UnityEngine;
using System.Collections;

public class Conf : MonoBehaviour {

	public GameObject Head;
	public GameObject Hand_Left;
	public GameObject Hand_Right;
	public Vector3 position;
	public string control;
	public PreConf pc;

	public float speed = 4.5f;
	public int time = 100;

	// Use this for initialization
	void Start () {
		//control = pc.control;

	}

	// Update is called once per frame
	void Update () {
		control = PlayerPrefs.GetString("control");
		speed = PlayerPrefs.GetFloat("speed");
		time = PlayerPrefs.GetInt("time");

		if (control == "head") position = Head.transform.localPosition;
		if (control == "left") position = Hand_Left.transform.localPosition;
		if (control == "right") position = Hand_Right.transform.localPosition;
		transform.position = new Vector3 (-position.x, time, speed);
	}
}
