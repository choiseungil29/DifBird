using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    enum MousePressing { LEFT = 0, RIGHT = 1, MIDDLE = 2 };

    private int speed = 100;
    private int scrollSpeed = 400;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.Translate(Vector3.forward * Input.GetAxis("Forward") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.left * Input.GetAxis("Left") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.back * Input.GetAxis("Back") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.right * Input.GetAxis("Right") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.up * Input.GetAxis("Mouse ScrollWheel") * Time.fixedDeltaTime * scrollSpeed * -1);

        if (Input.GetMouseButtonDown((int)MousePressing.LEFT) == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float distance = 100;
            if(Physics.Raycast(ray, out hit, distance))
            {
                //Debug.Log(hit.collider.transform.position);
                //Debug.Log(hit.collider.tag);
                //Debug.Log(hit.collider.name);
                Debug.Log(ray.GetPoint(hit.distance)); // Àý´ëÁÂÇ¥
            }
        }
	}
}
