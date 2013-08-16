using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class LastEvent : SuddenEvent {
	
	private string fullDiscription = "";
	private string nowDiscription = "";
	
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
        sqlcmd.CommandText = "SELECT * FROM SubEvent;";
        SqliteDataReader reader = sqlcmd.ExecuteReader();
		
		int count = 0;
		while(reader.Read()) {
			Debug.Log("" + reader[reader.RecordsAffected].ToString());
			
			if(count == 7) {
				fullDiscription = reader[0].ToString();
				int i=50;
				while(fullDiscription.Length >= i) {
					fullDiscription = fullDiscription.Insert(i, "\n");
					i += 50;
				}
			}
			
			count++;
		}
		
		nowDiscription = "";
	
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	
		if(Vector3.Distance(this.transform.position, player.transform.position) < 30.0f) {
			if(Input.GetMouseButtonUp(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				if(!GameObject.Find("/Chapter_1/3floor").GetComponent<ThreeFloorEvent>().getBeQualified() ||
					!GameObject.Find("/Chapter_1/Corpse/Man").GetComponent<WorkRoomEvent>().getBeQualified()) {
					return;
				}
				
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					if((hit.transform.gameObject == this.gameObject) && 
						(isChatting == false)) {
						
						Time.timeScale = 0.0f;
						player.GetComponent<MouseLook>().enabled = false;
						GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = false;
						
						beQualified = true;
						
						StartCoroutine("typeDiscription");
						
					}
				}
			}
		}
		
		if(isChatting == true) {
			
			if(Input.GetMouseButtonDown(0)) {
				nowDiscription = fullDiscription;
				isChatting = false;
				isChatBoxOff = false;
			}
			
		} else {
			
			if(isChatBoxOff == false) {
				
				if(Input.GetMouseButtonDown(0)) {
					isChatting = false;
					isChatBoxOff = true;
					
					Time.timeScale = 1.0f;
					player.GetComponent<MouseLook>().enabled = true;
					GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = true;
					
					Application.LoadLevel("MainScene");
				}
				
			} else {
				
			}
		}
		
	}
	
	protected override void SuddenEventLoop () {
		base.SuddenEventLoop();
	}
	
	void OnGUI() {
		if ((isChatting == true ||
				isChatBoxOff == false))
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
			yield return null;
		}
		
		isChatting = false;
		
	}
		
	
}
