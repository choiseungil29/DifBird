using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private bool dooropen = false;
    private Texture2D cursorTexture;

	// Use this for initialization
	void Start () {
        cursorTexture = (Texture2D)Resources.Load("Cursor");
	}
	
	// Update is called once per frame
	void Update () {

        Input.mousePosition.Set(Screen.width / 2, Screen.height / 2, 0);
	}

    void OnMouseDown()
    {
        Debug.Log("Clicked");

        
    }

    void OnMouseUp()
    {
        Vector3 pivot = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        if (dooropen == false)
        {
            this.transform.RotateAround(pivot, Vector3.up, -90.0f);
            this.transform.position = new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z + 2.5f);
            dooropen = true;
        }
        else
        {
            this.transform.RotateAround(pivot, Vector3.up, 90.0f);
            this.transform.position = new Vector3(this.transform.position.x - 2, this.transform.position.y, this.transform.position.z - 2.5f);
            dooropen = false;
        }

        Debug.Log("Up");
    }

    void OnMouseOver()
    {
        Debug.Log("Over");

        int mouseWidthHeight = 50;
        GUI.DrawTexture(new Rect(Screen.width / 2 - mouseWidthHeight, Screen.height / 2 - mouseWidthHeight, mouseWidthHeight, mouseWidthHeight), cursorTexture);
    }
}
