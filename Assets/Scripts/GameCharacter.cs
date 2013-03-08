using UnityEngine;
using System.Collections;

public class GameCharacter : MonoBehaviour {
	
    public Camera CharacterCamera;

	int speed = 1000;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Input.GetAxis("Forward") * Time.deltaTime);
        this.transform.Translate(Vector3.left * Input.GetAxis("Left") * Time.deltaTime);
        this.transform.Translate(Vector3.back * Input.GetAxis("Back") * Time.deltaTime);
        this.transform.Translate(Vector3.right * Input.GetAxis("Right") * Time.deltaTime);

        Vector3 CharacterPos = transform.position;
        CharacterCamera.transform.position = new Vector3(CharacterPos.x, CharacterPos.y, CharacterPos.z - 7);
        float Distance = Vector3.Distance(CharacterCamera.transform.position, this.transform.position);
        CharacterCamera.transform.LookAt(this.transform);
	}
}
