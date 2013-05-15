using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour {
	public float speed = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	 	this.transform.Translate(Vector3.forward * Input.GetAxis("Back") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.left * Input.GetAxis("Right") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.back * Input.GetAxis("Forward") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.right * Input.GetAxis("Left") * Time.fixedDeltaTime * speed);
	}
}
