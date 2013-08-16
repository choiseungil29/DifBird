using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class WorkRoomEvent : SuddenEvent {
	
	private bool isChatting = false;
	private bool isChatBoxOff = true;
	
	private string nowDiscription = "";
	private string fullDiscription  = "";
	
	public Texture letter;
	private bool viewLetter = false;
		
	// Use this for initialization
	protected override void Start () {
		base.Start();
		
		isLoop = false;
		beQualified = false;
		
		Debug.Log("Hello");
		
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
			
			if(count == 1) {
				fullDiscription = reader[0].ToString();
				int i=50;
				while(fullDiscription.Length >= i) {
					fullDiscription = fullDiscription.Insert(i, "\n");
					i += 50;
				}
			}
			
			count++;
		}
		
		Debug.Log("Full discription : " + fullDiscription);
		
        reader.Close();
        connection.Close();
		
		
		
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		
		if(Vector3.Distance(this.transform.position, player.transform.position) < 30.0f) {
			if(Input.GetMouseButtonUp(0)) {
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
				RaycastHit hit;
				
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					if((hit.transform.gameObject == this.gameObject) && 
						(isChatting == false) &&
						(viewLetter == false)) {
						
						viewLetter = true;
						Time.timeScale = 0.0f;
						player.GetComponent<MouseLook>().enabled = false;
						GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = false;
						
						beQualified = true;
						
					}
				}
				
			} else {
			
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
				RaycastHit hit;
				
				Debug.Log(Input.mousePosition);
				
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					if(hit.transform.gameObject == this.gameObject) {
						
						ObjectOutliner outliner = this.GetComponent<ObjectOutliner>();
						outliner.enabled = true;
						
						this.renderer.material.SetFloat("_Outline", outliner.outlineSize);
						this.renderer.material.SetColor("_OutlineColor", outliner.outlineColor);
					} else {
						ObjectOutliner outliner = this.GetComponent<ObjectOutliner>();
						outliner.enabled = true;
						
						this.renderer.material.SetFloat("_Outline", 0);
						this.renderer.material.SetColor("_OutlineColor", new Color(0, 0, 0, 0));
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
				}
				
			} else {
				
			}
		}
		
	}
	
	protected override void SuddenEventLoop () {
		base.SuddenEventLoop ();
		
	}
	
	void OnGUI() {
		
		GUI.skin.box.fontSize = 15;

        if (viewLetter == true)
        {
			Screen.showCursor = true;
			int margin = 30;
			GUI.Box(new Rect((Screen.width - letter.width)/2, (Screen.height - letter.height)/2, letter.width, letter.height), letter);
			if(GUI.Button(new Rect(Screen.width/2 + letter.width/2 - margin, Screen.height/2 - letter.height/2, margin, margin), "X")) {
				
				viewLetter = false;
				beQualified = true;
				
				Screen.showCursor = false;
				
				if(isChatting == false) 
					StartCoroutine("typeDiscription");
				
				//GameObject.Find("/Guard").transform.position = new Vector3(-328.0862f, 113.2796f, -315.7768f);
			}
        }
		
		if ((isChatting == true ||
			isChatBoxOff == false) &&
			viewLetter == false)
        {
			int width = Screen.width/2;
            //GUI.Box(new Rect(Screen.width / 10, Screen.height / 10, 400, 100), "Hint : " + now_key);
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
	
	void OnMouseOver() {
		/*ObjectOutliner outliner = this.GetComponent<ObjectOutliner>();
		outliner.enabled = true;
		
		this.renderer.material.SetFloat("_Outline", outliner.outlineSize);
		this.renderer.material.SetColor("_OutlineColor", outliner.outlineColor);*/
	}
	
	void OnMouseExit() {
		/*ObjectOutliner outliner = this.GetComponent<ObjectOutliner>();
		outliner.enabled = true;
		
		this.renderer.material.SetFloat("_Outline", 0);
		this.renderer.material.SetColor("_OutlineColor", new Color(255, 255, 255, 255));*/
	}
}
