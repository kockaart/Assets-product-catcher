using UnityEngine;
using System.Collections;

public class EggScript : MonoBehaviour {

	public float speed = 0.6F;
	private Transform m;

    void Awake()
    {
		m = GameObject.Find("Conf").transform;	

        //rigidbody.AddForce(new Vector3(0, -100, 0), ForceMode.Force);
    }

    //Update is called by Unity every frame
	void Update () {
		//speed = m.position.z;
		float fallSpeed = speed * Time.deltaTime;
        transform.position -= new Vector3(0, fallSpeed, 0);

        if (transform.position.y < -1 || transform.position.y >= 20)
        {
            //Destroy this gameobject (and all attached components)
            Destroy(gameObject);
        }
	}
}
