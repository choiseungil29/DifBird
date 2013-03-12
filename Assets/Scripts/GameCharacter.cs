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
        this.transform.Translate(Vector3.forward * Input.GetAxis("Forward") * Time.deltaTime * speed);
        this.transform.Translate(Vector3.left * Input.GetAxis("Left") * Time.deltaTime * speed);
        this.transform.Translate(Vector3.back * Input.GetAxis("Back") * Time.deltaTime * speed);
        this.transform.Translate(Vector3.right * Input.GetAxis("Right") * Time.deltaTime * speed);
        // Character Movement

        Vector3 CharacterPos = transform.position;
        camera.transform.position = new Vector3(CharacterPos.x, CharacterPos.y, CharacterPos.z - cameraDistance);
        camera.transform.LookAt(this.transform);

        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f; // 좌, 우(x방향)으로 돌아감
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f; // 상, 하(y방향)으로 돌아감

        y = Mathf.Clamp(y, -5.0f, 20.0f);

        this.transform.rotation = Quaternion.Euler(y, x, 0); // x, y, z를 축으로 한 회전값 반환

        Vector3 characterAng = this.transform.rotation.eulerAngles;
        camera.transform.RotateAround(CharacterPos, camera.transform.TransformDirection(Vector3.up), characterAng.y); // Z축도 같이 내려간다. 이걸 수정
        camera.transform.RotateAround(CharacterPos, camera.transform.TransformDirection(Vector3.right), characterAng.x);
	}
}
