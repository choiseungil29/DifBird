using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
    }

    private void OnDrawGizmos()
    {
        float degree = 60.0f;
        float distance = 100.0f;
        Gizmos.color = Color.red;

        Vector3 from = new Vector3(0, 0, 0);
        Vector3 to = new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad) * distance, Mathf.Sin(degree * Mathf.Deg2Rad) * distance, 0);        

        Gizmos.DrawLine(from, to);
    }
}
