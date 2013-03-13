using UnityEngine;
using System.Collections;

public class GameCharacter : MonoBehaviour {
	
    public Camera camera;

	private int speed = 10;
    private int cameraDistance = 7;

    private int xSpeed = 250;
    private int ySpeed = 120;

    private float x = 0.0f;
    private float y = 0.0f;

	// Use this for initialization
	void Start () 
    {
        Vector3 angle = this.transform.eulerAngles;
        x = angle.x;
        y = angle.y;

        if (this.rigidbody)
            this.rigidbody.freezeRotation = true;
	}

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
	}

    void Movement()
    {
        this.transform.Translate(Vector3.forward * Input.GetAxis("Forward") * Time.deltaTime * speed);
        this.transform.Translate(Vector3.left * Input.GetAxis("Left") * Time.deltaTime * speed);
        this.transform.Translate(Vector3.back * Input.GetAxis("Back") * Time.deltaTime * speed);
        this.transform.Translate(Vector3.right * Input.GetAxis("Right") * Time.deltaTime * speed);
        // Character Movement
    }

    void Rotation()
    {
        Vector3 CharacterPos = this.transform.position;
        camera.transform.position = new Vector3(CharacterPos.x, CharacterPos.y, CharacterPos.z - cameraDistance);
        camera.transform.LookAt(this.transform);

        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f; // 0.02f -> Mouse Sensibility

        y = Mathf.Clamp(y, -5.0f, 20.0f);

        this.transform.rotation = Quaternion.Euler(y, x, 0); // axis of x, y, z rotation value transform

        Vector3 characterAng = this.transform.rotation.eulerAngles;
        camera.transform.RotateAround(CharacterPos, camera.transform.TransformDirection(Vector3.up), characterAng.y);
        camera.transform.RotateAround(CharacterPos, camera.transform.TransformDirection(Vector3.right), characterAng.x);
    }
}
