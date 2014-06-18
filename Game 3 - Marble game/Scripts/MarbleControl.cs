using UnityEngine;
using System.Collections;

public class MarbleControl : MonoBehaviour {

    public float movementSpeed = 60f;
	public float dist = 1;
	public string control;
	public Vector3 pos;
	public GameObject Head;
	public GameObject Hand_Left;
	public GameObject Hand_Right;
	
	void Update () {
//		hor=hor+0.1;	
//		elseif
//			hor=hor-0.1;
//		else
//			hor=0;
////		ver=ver+0.1;
//		elseif	
//			ver=ver-01;	
//		else
//			ver=0;

		control = PlayerPrefs.GetString("control2");

		if (control == "head") pos = Head.transform.localPosition;
		if (control == "left") pos = Hand_Left.transform.localPosition;
		if (control == "right") pos = Hand_Right.transform.localPosition;
		
		movementSpeed = PlayerPrefs.GetFloat("speed2");
		Vector3 movement = (-pos.x * Vector3.left * movementSpeed) + ((pos.z-PlayerPrefs.GetFloat("sense2")) * Vector3.forward *movementSpeed);
		movement *= Time.deltaTime;
		if (pos.z!=0) rigidbody.AddForce(movement, ForceMode.Force);
	}

    void OnTriggerEnter  (Collider other  ) {
        if (other.tag == "Pickup")
        {
            MarbleGameManager.SP.FoundGem();
            Destroy(other.gameObject);
        }
        else
        {
            //Other collider.. See other.tag and other.name
        }        
    }

}
