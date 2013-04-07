using UnityEngine;
using System.Collections;

public class MainSceneGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		
		int width = 100;
		int height = 30;

		int paddingX = 9;
		int paddingY = 6;

		if(GUI.Button(new Rect(Screen.width - width - paddingX, Screen.height - height - paddingY, width, height), "GameStart!")) {
			print("Button Click!");
            Timer timer = gameObject.AddComponent<Timer>();
            timer.SetTimer(this.LoadLevel, 3000);
		}
	}

    void LoadLevel()
    {
        Debug.Log("Before LoadLevel");
        Application.LoadLevel("GameScene");
        Debug.Log("After LoadLevel");
    }
}
