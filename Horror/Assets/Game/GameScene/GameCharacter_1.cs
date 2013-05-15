using UnityEngine;
using System.Collections;

public class GameCharacter_1 : MonoBehaviour {
	   
    public Camera camera = null;

	private int speed = 10;
    private int cameraDistance = -1;

    private int xSpeed = 250;
    private int ySpeed = 120;

    private float x = 0.0f;
    private float y = 0.0f;
	
	public bool isControl = false;
	
	public void Set_isControl(bool x)
    {
        isControl = x;
    }

	// Use this for initialization
	void Start () 
    {
		camera.enabled = false;
        Vector3 angle = this.transform.eulerAngles;
        x = angle.x;
        y = angle.y;

        if (this.rigidbody)
            this.rigidbody.freezeRotation = true;
	}

    void FixedUpdate()
    {
<<<<<<< HEAD:Horror/Assets/Game/GameScene/GameCharacter.cs
        //Movement();
        //Rotation();
    }

    // Update is called once per frame
    void Update()
    {
		//if(isControl == false) return;
        Movement();
        Rotation();
	}
=======
		if(PhotonNetwork.isMasterClient == true)
		{
			camera.enabled = true;
		}
		else return;
		if(isControl == false) 
			return;
        Movement();
        Rotation();
    }

    // Update is called once per frame
>>>>>>> b2641c3689950e84b26f542fd82593fb6ea712a2:Horror/Assets/Game/GameScene/GameCharacter_1.cs

    void Movement()
    {
        this.transform.Translate(Vector3.forward * Input.GetAxis("Back") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.left * Input.GetAxis("Right") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.back * Input.GetAxis("Forward") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.right * Input.GetAxis("Left") * Time.fixedDeltaTime * speed);
        // Character Movement
    }

    void Rotation()
    {
        Vector3 CharacterPos = this.transform.position;
        camera.transform.position = new Vector3(CharacterPos.x, CharacterPos.y, CharacterPos.z - cameraDistance);
        camera.transform.LookAt(this.transform);

        x += Input.GetAxis("Mouse X") * xSpeed * 0.008f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.008f; // 0.02f -> Mouse Sensibility

        y = Mathf.Clamp(y, -80.0f, 80.0f);

        this.transform.rotation = Quaternion.Euler(y, x, 0); // axis of x, y, z rotation value transform

        Vector3 characterAng = this.transform.rotation.eulerAngles;
        camera.transform.RotateAround(CharacterPos, camera.transform.TransformDirection(Vector3.up), characterAng.y);
        camera.transform.RotateAround(CharacterPos, camera.transform.TransformDirection(Vector3.right), characterAng.x);
    }
}
