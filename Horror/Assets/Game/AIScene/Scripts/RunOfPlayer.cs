using UnityEngine;
using System.Collections;

public class RunOfPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
                Debug.Log("Success Run");
                // 존나 sendMessage보내장 Player내의 Script에
            }

            yield return null;
        }
    }
}
