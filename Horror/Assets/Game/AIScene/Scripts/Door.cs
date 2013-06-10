using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private float smooth = 5.0f;
    private float DoorOpenAngle = 90.0f;
    private bool open = false;

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
        Debug.Log(open);
        if (open)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
            //Open door
        }
        else
        {
            //Close door
            if (transform.eulerAngles.y <= 359) transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
            
        }

        if (Input.GetKeyDown("f"))
        {
            open = !open;
        }
	}
}
