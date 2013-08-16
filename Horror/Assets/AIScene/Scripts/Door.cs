using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private float smooth = 5.0f;
    private float DoorOpenAngle = 90.0f;
    private bool close = true;

    private Vector3 defaultRot;
    private Vector3 openRot;
	// Use this for initialization
	void Start ()
    {
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (close)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
        }
        else
        {
            if (transform.eulerAngles.y <= 359)
				transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
            
        }
	}
	
	public void KnockDoor() {
		close = !close;
	}
}
