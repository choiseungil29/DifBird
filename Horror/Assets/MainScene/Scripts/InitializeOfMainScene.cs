using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class InitializeOfMainScene : MonoBehaviour {
	
	private GUITexture fadeTexture;
	private AsyncOperation aOperation;
	
	public float alpha = 1.0f;
	
	public Texture2D mainScreen;
	
	private bool isCreditOn = false;
	private bool isNext = false;
	
	private string discription;
	
	// Use this for initialization
	void Start () {
		fadeTexture = GameObject.Find("/FadeInOut").guiTexture;
		fadeTexture.color = new Color(128, 128, 128, 0);
		
		StartCoroutine("asyncLoadingScene");
		
		string dbtext = "SubEvent.db";
        string dbConnection = string.Format("Data Source={0}", dbtext);
        SqliteConnection connection = new SqliteConnection(dbConnection);
        connection.Open();
        SqliteCommand sqlcmd = new SqliteCommand(connection);
        sqlcmd.CommandText = "SELECT * FROM Credit;";
        SqliteDataReader reader = sqlcmd.ExecuteReader();

		while(reader.Read()) {
			Debug.Log(reader.GetString(reader.RecordsAffected));
			discription = reader.GetString(reader.RecordsAffected);
		}
        reader.Close();
        connection.Close();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!animation.IsPlaying("FadeInOut")) {
			//mainScreen.Resize(Screen.width, Screen.height);
			fadeTexture.texture = mainScreen;
		} else {
			fadeTexture.pixelInset = new Rect(-Screen.width/2, -Screen.height/2, Screen.width, Screen.height * 2);
			fadeTexture.color = new Color(128, 128, 128, alpha);
		}
		
		if(isCreditOn == true) {
			if(Input.GetMouseButtonDown(0)) {
				isCreditOn = false;
			}
		}
		
	}
	
	void OnGUI() {
		
		
		if(animation.IsPlaying("FadeInOut"))
			return;
		
		int width = 300;
		int height = 50;
		
		int period = 30;
		
		GUI.skin.box.fontSize = 20;
		GUI.skin.button.fontSize = 15;
		//GUI.Box(new Rect(0, 0, Screen.width, Screen.height), mainScreen);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mainScreen);
		
		if(isCreditOn == false) {
			
			if(isNext == false) {
				if(GUI.Button(new Rect((Screen.width - width)/2, Screen.height/10*8 - height/2 - period, width, height), "Game Start")) {
					//aOperation = Application.LoadLevelAdditiveAsync("AIScene");
					isNext = true;
					Screen.showCursor = false;
					aOperation = Application.LoadLevelAsync("AIScene");
				}
				
				if(GUI.Button(new Rect((Screen.width - width)/2, Screen.height/10*8 - height/2 + period, width, height), "Credit")) {
					isCreditOn = true;
				}
			}
		} else {
			int pWidth = (Screen.width/10) * 4;
			int pHeight = (Screen.height/10) * 4;
			GUI.Box(new Rect(Screen.width/2 - pWidth/2, Screen.height/2 - pHeight/2 + pHeight/2, pWidth, pHeight), discription);
			
		}
		
	}
	
	IEnumerator asyncLoadingScene() {
		yield return new WaitForSeconds(3.0f);	
		
		//aOperation = Application.LoadLevelAdditiveAsync("AIScene");
	}
}
