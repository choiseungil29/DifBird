using UnityEngine;
using System.Collections;

public class BlingBlingOfPlane : MonoBehaviour {

    private bool flag = false;

	// Use this for initialization
	void Start () { 
        StartCoroutine("StartLoad", "AIScene");
        StartCoroutine("Animate");
	}
	
	// Update is called once per frame
	void Update () {

	}

    public IEnumerator StartLoad(string strSceneName)
    {
        yield return new WaitForSeconds(3.0f);

        AsyncOperation async = Application.LoadLevelAsync(strSceneName);
    }

    public IEnumerator Animate()
    {
        int i = 1;
        while (true)
        {
            if (this.transform.position.x >= 100)
                i = -1;
            if (this.transform.position.x <= -100)
                i = 1;

            this.transform.Translate(Vector3.right * Time.deltaTime * 100 * i);
            yield return true;
        }
    }
}
