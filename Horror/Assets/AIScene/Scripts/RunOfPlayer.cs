using UnityEngine;
using System.Collections;

public class RunOfPlayer : MonoBehaviour {
	
	public float maxRunForwardSpeed = 24.0f;
	public double runPeriod = 12.0f;
	
	BobOfCamera bobOfCamera;
	
	private CharacterMotor motor;
	private CharacterMotorMovement movement;
	
	// Use this for initialization
	void Start () {
		
		GameObject mainCamera = GameObject.Find("/Player/Main Camera");
		bobOfCamera = mainCamera.GetComponent<BobOfCamera>();
		
		motor = this.GetComponent<CharacterMotor>();
		movement = motor.movement;
		
        StartCoroutine("Run");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Run()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                //Debug.Log("Success Run");
				
				
				
				if(movement.maxForwardSpeed != null) {
					movement.maxForwardSpeed = maxRunForwardSpeed;
					bobOfCamera.Period = runPeriod;
				}
				
            } else {
				//movement.maxForwardSpeed = maxRunForwardSpeed/2;
				//Debug.Log("no");
				
				if(movement.maxForwardSpeed != null) {
					movement.maxForwardSpeed = maxRunForwardSpeed/2;
					bobOfCamera.Period = runPeriod/2;
				}
			}
			

            yield return null;
        }
    }
}
