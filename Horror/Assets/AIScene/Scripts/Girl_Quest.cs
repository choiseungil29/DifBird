using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

public class Girl_Quest : MonoBehaviour {
	
	public class KeyInfo {
		private int nowKey;
		private string nowKeyDiscription;
		private bool isSuccess;
		
		public KeyInfo(int pNowKey, string pNowKeyDiscription, bool pIsSuccess) {
			nowKey = pNowKey;
			nowKeyDiscription = pNowKeyDiscription;
			isSuccess = pIsSuccess;
		}
		
		public KeyInfo(string pNowKeyDiscription, bool pIsSuccess) {
			nowKeyDiscription = pNowKeyDiscription;
			isSuccess = pIsSuccess;
		}
		
		public int getNowKey() {
			return nowKey;
		}
		
		public string getNowKeyDiscription() {
			return nowKeyDiscription;	
		}
		
		public bool getIsSuccess() {
			return isSuccess;
		}
		
		public void setIsSuccess(bool n) {
			isSuccess = n;	
		}
	}
	
	public GameObject player;
	public float chatMargin;
	
    private string nowDiscription;
	private string realDiscription;
	
	private List<KeyInfo> listKeyInfo;
	private KeyInfo nowKey;
	
	
	private bool isChat;
	private bool isChatEnd;
	
	public KeyInfo getNowKey() {
		return nowKey;	
	}
	
	public bool getIsChatEnd() {
		return isChatEnd;	
	}
	
	public float getChatMargin() {
		return chatMargin;	
	}
	
	// Use this for initialization
	void Start ()
    {
		isChat = false;
		isChatEnd = true;
		
		realDiscription = "";
		nowDiscription = "";
		
		listKeyInfo = new List<KeyInfo>();
		
		string dbtext = "SubEvent.db";
        string dbConnection = string.Format("Data Source={0}", dbtext);
        SqliteConnection connection = new SqliteConnection(dbConnection);
        connection.Open();
        SqliteCommand sqlcmd = new SqliteCommand(connection);
        sqlcmd.CommandText = "SELECT * FROM KeyInfo;";
        SqliteDataReader reader = sqlcmd.ExecuteReader();
		
		while(reader.Read()) {
			Debug.Log(reader.GetString(reader.RecordsAffected));
			listKeyInfo.Add(new KeyInfo(reader.GetString(reader.RecordsAffected), false));
		}
        reader.Close();
        connection.Close();
		
		nowKey = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
		if(isChat == false && isChatEnd == false) {
			if(Input.GetMouseButtonUp(0)) {
				
				if(realDiscription.Length == nowDiscription.Length) {
					isChat = false;
					isChatEnd = true;
					Debug.Log("Same");
				}
			}
			
			if(Vector3.Distance(this.transform.position, player.transform.position) > chatMargin) {
				isChat = false;
				isChatEnd = true;
				Debug.Log("Same");
			}
			
		}
		
		if(isChat == true && isChatEnd == false) {
			if(Input.GetMouseButtonUp(0)) {
				
				if(realDiscription.Length != nowDiscription.Length) {
					isChat = false;
					isChatEnd = false;
					nowDiscription = realDiscription;
				}
			}
		}
	}
	
    void OnGUI()
    {
		
		GUI.skin.box.fontSize = 15;
		
        if (isChat == true ||
			isChatEnd == false)
        {
			int width = Screen.width/2;
            //GUI.Box(new Rect(Screen.width / 10, Screen.height / 10, 400, 100), "Hint : " + now_key);
            GUI.Box(new Rect((Screen.width - width)/2, Screen.height / 10 * 9, width, Screen.height / 10), "Discription : " + nowDiscription);
        }
    }
	
	
	public void ExplainQuest() { 		
		if(isChat == true)
			return;
		
		if(listKeyInfo.Count <= 0) {
			realDiscription = "All Clear!!!";
			StartCoroutine(this.typeDiscription(realDiscription));
			return;
		}
		
		if(nowDiscription != "")
			nowDiscription = "";
		
		
		
		if(nowKey == null) {
			
			int idx = Random.Range(0, listKeyInfo.Count);
			nowKey = listKeyInfo[idx];
		
		} else {
			
			if(nowKey.getIsSuccess()) {
				listKeyInfo.Remove(nowKey);
				nowKey = listKeyInfo[Random.Range(0, listKeyInfo.Count)];
			} else {
				//return;	
			}
		
		}
		
		StartCoroutine(this.typeDiscription(nowKey.getNowKeyDiscription()));
	}
	
	IEnumerator typeDiscription(string discription) {
		
		realDiscription = discription;
		
		isChatEnd = false;
		int count = 0;
		while(nowDiscription.Length < realDiscription.Length) {
			isChat = true;
			
			nowDiscription += realDiscription.ToCharArray()[count];
			count++;
			
			yield return null;
			yield return null;
		}
		
		Debug.Log(realDiscription);
		
		isChat = false;
	}
}
