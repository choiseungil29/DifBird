using UnityEngine;
using System.Collections;

public class GameSceneGUI : MonoBehaviour {

    public GUIText Count;

    private float seconds = 60.0f;
    private int minute = 14;

	// Use this for initialization
	void Start () {
        Screen.lockCursor = true;
        Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {

        seconds -= Time.deltaTime;

        if (minute > 4)
            Count.enabled = false;
        else
            Count.enabled = true;

        if (seconds <= 0.0f)
        {
            minute -= 1;
            seconds = 60.0f;
        }

        if (minute < 0)
        {
            Application.LoadLevel("Lose");
        } // Lose Game Code

        Count.text = minute.ToString() + " : " + ((int)seconds).ToString();

        Debug.Log(Screen.lockCursor);
        Debug.Log(Screen.showCursor);
	}

	void OnGUI() {

	}
}
