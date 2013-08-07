using UnityEngine;
using System.Collections;

public class InitializeOfMainScene : MonoBehaviour {
	
	private GUITexture fadeTexture;
	private AsyncOperation aOperation;
	
	public float alpha = 1.0f;
	
	public Texture2D mainScreen;
	
	// Use this for initialization
	void Start () {
		fadeTexture = GameObject.Find("/FadeInOut").guiTexture;
		fadeTexture.color = new Color(128, 128, 128, 0);
		
		StartCoroutine("asyncLoadingScene");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!animation.IsPlaying("FadeInOut")) {
			mainScreen.Resize(Screen.width, Screen.height);
			fadeTexture.texture = mainScreen;
		} else {
			fadeTexture.pixelInset = new Rect(-Screen.width/2, -Screen.height/2, Screen.width, Screen.height * 2);
			fadeTexture.color = new Color(128, 128, 128, alpha);
		}
		
	}
	
	void OnGUI() {
		
		
		if(animation.IsPlaying("FadeInOut"))
			return;
		
		int width = 300;
		int height = 50;
		
		int period = 30;
		
		if(GUI.Button(new Rect((Screen.width - width)/2, (Screen.height - height)/2 - period, width, height), "new game")) {
			
		}
		
		if(GUI.Button(new Rect((Screen.width - width)/2, (Screen.height- height)/2 + period, width, height), "load game")) {
			
		}
		
	}
	
	IEnumerator asyncLoadingScene() {
		yield return new WaitForSeconds(3.0f);	
		
		aOperation = Application.LoadLevelAdditiveAsync("AIScene");
	}
}
