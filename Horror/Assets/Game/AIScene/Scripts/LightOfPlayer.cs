using UnityEngine;
using System.Collections;

public class LightOfPlayer : MonoBehaviour {

    private GameObject light;

	// Use this for initialization
	void Start () {
        light = GameObject.Find("/Player/Spotlight");

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
                light.light.enabled = !light.light.enabled;
            yield return null;
        }
    }
}
