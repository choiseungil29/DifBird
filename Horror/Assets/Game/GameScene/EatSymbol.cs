using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EatSymbol : MonoBehaviour {

    public GUIText countText;
    public List<GameObject> symbolList;
    private int Count = 6;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        GameObject destroyer = null;

        foreach (GameObject value in symbolList)
        {
            float distance = Vector3.Distance(this.transform.position, value.transform.position);
            if (distance < 10.0f)
            {
                destroyer = value;
                Count--;
            }
        }

        if (destroyer != null)
        {
            symbolList.Remove(destroyer);
            Destroy(destroyer);
        }

        countText.text = Count.ToString();

        if (Count <= 0)
        {
            // Win Game!
        }
        Debug.Log(Application.dataPath);
	}
}
