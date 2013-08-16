using UnityEngine;
using System.Collections;

public class PauseScene : MonoBehaviour {
	
	private bool isPause = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyUp(KeyCode.Escape)) {
			
			if(Time.timeScale == 1.0f) {
				Time.timeScale = 0.0f;
				GameObject.Find("/Player").GetComponent<MouseLook>().enabled = false;
				GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = false;
				isPause = true;
				Screen.showCursor = true;
			} else {
				Time.timeScale = 1.0f;
				GameObject.Find("/Player").GetComponent<MouseLook>().enabled = true;
				GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = true;
				isPause = false;
				Screen.showCursor = false;
			}
		}
	}
	
	void OnGUI() {
		
		GUI.skin.button.fontSize = 15;
		
		int width = 300;
		int height = 50;
		
		int period = 30;
		
		if(isPause == true) {
			if(GUI.Button(new Rect((Screen.width - width)/2, Screen.height/10*8 - height/2 - period, width, height), "Go to game")) {
				isPause = false;
				GameObject.Find("/Player").GetComponent<MouseLook>().enabled = true;
				GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = true;
				Time.timeScale = 1.0f;
				Screen.showCursor = false;
			}
			
			if(GUI.Button(new Rect((Screen.width - width)/2, Screen.height/10*8 - height/2 + period, width, height), "Exit Game")) {
				Application.Quit();
			}
		}
	}
}
