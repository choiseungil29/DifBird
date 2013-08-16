using UnityEngine;
using System.Collections;

public class LightOfPlayer : MonoBehaviour {

    private Light light;

	// Use this for initialization
	void Start () {
        light = GameObject.Find("/Player/Main Camera/Spotlight").light;

        StartCoroutine("OnRightMouseDown");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator OnRightMouseDown()
    {
        while (true)
        {
            if(Input.GetMouseButtonDown(1))
                light.enabled = !light.enabled;
            yield return null;
        }
    }
}
