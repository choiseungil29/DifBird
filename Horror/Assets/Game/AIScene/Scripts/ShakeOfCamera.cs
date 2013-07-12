using UnityEngine;
using System.Collections;

public class ShakeOfCamera : MonoBehaviour {

    public float shakeTime = 1.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator Shake()
    {
        float deltaTime = 0.0f;

        Debug.Log("Success1");
        while ((shakeTime - deltaTime) >= 0.0f)
        {
            Debug.Log("Success2");
            this.transform.eulerAngles += new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-4.0f, 4.0f), 0);

            deltaTime += Time.deltaTime;
            Debug.Log("Delta Time : " + Time.deltaTime);
            Debug.Log("delta Time : " + deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return null;
    }
}
