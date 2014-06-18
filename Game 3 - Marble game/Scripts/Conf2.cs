using UnityEngine;
using System.Collections;

public class Conf2 : MonoBehaviour {
	
	public GameObject Head;
	public GameObject Hand_Left;
	public GameObject Hand_Right;
	public Vector3 position;
	public string control;
	public PreConf pc;
	
	public float speed = 66;

	// Use this for initialization
	void Start () {
	//	control = pc.control;
		
	}
	
	// Update is called once per frame
	void Update () {
		pc = GameObject.Find("PreConf").GetComponent<PreConf>();
		control = pc.control;
		speed = (float)(pc.speed*2f+66f);
		
		if (control == "head") position = Head.transform.localPosition;
		if (control == "left") position = Hand_Left.transform.localPosition;
		if (control == "right") position = Hand_Right.transform.localPosition;
		if (position.y!=0) transform.position = new Vector3 (-position.x, position.y-1, speed);
	}
}
