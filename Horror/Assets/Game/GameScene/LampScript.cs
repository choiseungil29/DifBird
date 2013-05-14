using UnityEngine;
using System.Collections;

public class LampScript : MonoBehaviour {

    public GameObject pivot;
    bool flag = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Debug.Log("x : " + transform.localRotation.eulerAngles.x);
        if (this.transform.localRotation.eulerAngles.x > 290)
        {
            if (flag == false) flag = true;
            else flag = false;
            Debug.Log("false"); 
        }

        if (flag == true) gameObject.transform.RotateAround(pivot.transform.position, this.transform.TransformDirection(Vector3.up), 10 * Time.deltaTime);
        else gameObject.transform.RotateAround(pivot.transform.position, this.transform.TransformDirection(Vector3.down), 10 * Time.deltaTime);
	}
}
