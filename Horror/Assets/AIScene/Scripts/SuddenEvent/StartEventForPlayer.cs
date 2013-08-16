using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class StartEventForPlayer : SuddenEvent {
	
	private string fullDiscription;
	private string nowDiscription;
	
	private bool isChatting = false;
	private bool isChatBoxOff = true;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		
		string dbtext = "SubEvent.db";
        string dbConnection = string.Format("Data Source={0}", dbtext);
        SqliteConnection connection = new SqliteConnection(dbConnection);
        connection.Open();
        SqliteCommand sqlcmd = new SqliteCommand(connection);
        sqlcmd.CommandText = "SELECT * FROM StartEvent;";
        SqliteDataReader reader = sqlcmd.ExecuteReader();
		
		while(reader.Read()) {
			Debug.Log("" + reader[reader.RecordsAffected].ToString());
		
			fullDiscription = reader[0].ToString();
			int i=50;
			while(fullDiscription.Length >= i) {
				fullDiscription = fullDiscription.Insert(i, "\n");
				i += 50;
			}
		}
		
		Time.timeScale = 0.0f;
		player.GetComponent<MouseLook>().enabled = false;
		GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = false;
		
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		
		beQualified = true;
		
		if(isChatting == true) {
			
			if(Input.GetMouseButtonDown(0)) {
				//nowDiscription = fullDiscription;
				//isChatting = false;
				//isChatBoxOff = false;
			}
			
		} else {
			
			if(isChatBoxOff == false) {
				
				if(Input.GetMouseButtonDown(0)) {
					isChatting = false;
					isChatBoxOff = true;
					
					Time.timeScale = 1.0f;
					player.GetComponent<MouseLook>().enabled = true;
					GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = true;
				}
				
			} else {
				
			}
		}
	}
			
	protected override void SuddenEventLoop() {
		base.SuddenEventLoop();
		
		
		StartCoroutine("typeDiscription");
		
		//CommunicationBox.getInstance().StartChat("Hello", target);
	}
	
	void OnGUI() {
		
		GUI.skin.box.fontSize = 15;
		
		if ((isChatting == true) ||
			(isChatBoxOff == false))
        {
			int width = Screen.width/2;
            GUI.Box(new Rect((Screen.width - width)/2, Screen.height / 10 * 9, width, Screen.height / 10), "" + nowDiscription);
        }
		
	}
	
	IEnumerator typeDiscription() {
		
		isChatting = true;
		isChatBoxOff = false;
		
		nowDiscription = "";
		
		int count = 0;
		while(nowDiscription.Length < fullDiscription.Length) {
			
			nowDiscription += fullDiscription[count];
			
			count++;
			
			yield return null;
		}
		
		isChatting = false;
		
	}
}
