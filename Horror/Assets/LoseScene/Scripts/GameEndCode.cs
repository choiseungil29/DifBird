using UnityEngine;
using System.Collections;

public class GameEndCode : MonoBehaviour {
	
	public Texture loseTexture;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.anyKeyDown == true) {
			Application.LoadLevel("MainScene");
		}
	
	}
	
	void OnGUI() {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), loseTexture);
	}
}

