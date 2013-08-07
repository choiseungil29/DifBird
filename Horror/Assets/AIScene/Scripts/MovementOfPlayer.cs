using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementOfPlayer : MonoBehaviour {

    private GameObject mainCamera = null;
    private GameObject subCamera = null;

    private int speed = 15;
    private int cameraDistance = -1;

    private int xSpeed = 250;
    private int ySpeed = 120;

    private float x = 0.0f;
    private float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.Find("/Player/Main Camera");
        subCamera = GameObject.Find("/Player/Sub Camera");

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
        //this.transform.Translate(new Vector3(0, gravity, 0) * Time.fixedDeltaTime);
        // Character Movement
    }

    void Rotation()
    {
        Vector3 CharacterPos = this.transform.position;
        mainCamera.transform.position = new Vector3(CharacterPos.x, CharacterPos.y, CharacterPos.z - cameraDistance);
        mainCamera.transform.LookAt(this.transform);

        x += Input.GetAxis("Mouse X") * xSpeed * 0.008f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.008f; // 0.02f -> Mouse Sensibility

        y = Mathf.Clamp(y, -80.0f, 80.0f);

        this.transform.rotation = Quaternion.Euler(0, x, 0); // axis of x, y, z rotation value transform

        subCamera.transform.rotation = Quaternion.Euler(y, x, 0);

        Vector3 characterAng = this.transform.rotation.eulerAngles;
        mainCamera.transform.RotateAround(CharacterPos, mainCamera.transform.TransformDirection(Vector3.up), characterAng.y);
        mainCamera.transform.Rotate(Vector3.up * 180.0f);
        mainCamera.transform.RotateAround(CharacterPos, mainCamera.transform.TransformDirection(Vector3.right), characterAng.x);
    }
}
