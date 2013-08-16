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
	
	private List<Vector3> posList;
	private int posIdx = 0;

    private string nowDiscription;
	private string realDiscription;
	
	private NavMeshAgent agent;

	private List<KeyInfo> listKeyInfo;
	private KeyInfo nowKey;
	private int nowKeyIdx;

	private bool isChatting;
	private bool isChatBoxOff;

	public KeyInfo getNowKey() {
		return nowKey;	
	}
	
	public float getChatMargin() {
		return chatMargin;
	}
	
	public bool getIsChatBoxOff() {
		return isChatBoxOff;
	}

	// Use this for initialization
	void Start ()
    {
		agent = this.GetComponent<NavMeshAgent>();
		
		isChatting = false;
		isChatBoxOff = true;

		realDiscription = "";
		nowDiscription = "";

		listKeyInfo = new List<KeyInfo>();
		nowKeyIdx = 0;

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

		nowKey = listKeyInfo[nowKeyIdx];
		
		GameObject.Find("/Chapter_1").SetActive(true);
		GameObject.Find("/Chapter_2").SetActive(false);
		GameObject.Find("/Chapter_3").SetActive(false);
		GameObject.Find("/Chapter_4").SetActive(false);
		
		posList = new List<Vector3>();
		posIdx = 0;
		
		dbtext = "Test.db";
		dbConnection = string.Format("Data Source={0}", dbtext);
        connection = new SqliteConnection(dbConnection);
        connection.Open();
        sqlcmd = new SqliteCommand(connection);
        sqlcmd.CommandText = "SELECT * FROM Position;";
        reader = sqlcmd.ExecuteReader();

		while (reader.Read())
        {
            posList.Add(new Vector3(reader.GetFloat(0), reader.GetFloat(1), reader.GetFloat(2)));
			Debug.Log("1 : " + reader.GetFloat(0));
			Debug.Log("2 : " + reader.GetFloat(1));
			Debug.Log("3 : " + reader.GetFloat(2));
        }
        reader.Close();
        connection.Close();
	} 

	// Update is called once per frame
	void Update ()
    {
		this.AI();
		
		//GameObject.Find("/girl/Bip001").transform.position = new Vector3(0, 0, 0);
		
		/*if(Input.GetMouseButtonUp(0)) {
			
			if(isChatting == true) {
				
				isChatting = false;
				
				if(nowDiscription.Length == realDiscription.Length)
					isChatBoxOff = true;
				nowDiscription = realDiscription;
				
			}
			
		}

		if(isChatting == false && isChatBoxOff == false) {
			if(Input.GetMouseButtonUp(0)) {

				isChatting = false;
				isChatBoxOff = true;
				Time.timeScale = 1.0f;
				player.GetComponent<MouseLook>().enabled = true;
				GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = true;
			}

			if(Vector3.Distance(this.transform.position, player.transform.position) > chatMargin) {
				isChatting = false;
				isChatBoxOff = true;
			}

		}

		if(isChatting == true && isChatBoxOff == false) {
			if(Input.GetMouseButtonUp(0)) {

				isChatting = false;
				isChatBoxOff = false;
				nowDiscription = realDiscription;
			}
		}*/
		
		/*if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				if(hit.transform.gameObject == this.gameObject) {
					
					// view 2d Texture
					// write to this note is my note...
					
					Time.timeScale = 0.0f;
					player.GetComponent<MouseLook>().enabled = false;
					GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = false;
				}
			}
		}*/
		
		
		if(isChatting == true) {
			
			if(Input.GetMouseButtonDown(0)) {
				//nowDiscription = fullDiscription;
				//isChatting = false;
				//isChatBoxOff = false;
			}
			
			Debug.Log(" Why Here");
			
		} else {
			
			Debug.Log("Why Here??");
			
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
		
		
		switch(nowKeyIdx) {
		case 0:
			if((GameObject.Find("/Chapter_1/3floor").GetComponent<ThreeFloorEvent>().getBeQualified() == true) &&
				(GameObject.Find("/Chapter_1/Corpse/Man").GetComponent<WorkRoomEvent>().getBeQualified())) {
				nowKey.setIsSuccess(true);
				Debug.Log("Clear!!");
				
				nowKeyIdx++; // Legacy code. only for KWC
			}
			break;
		case 1:
			break;
		case 2:
			
			// now Code
			
			break;
		case 3:
			break;
		}
	}

    void OnGUI()
    {

		GUI.skin.box.fontSize = 15;

        if (isChatting == true ||
			isChatBoxOff == false)
        {
			int width = Screen.width/2;
            GUI.Box(new Rect((Screen.width - width)/2, Screen.height / 10 * 9, width, Screen.height / 10), "" + nowDiscription);
        }
    }


	public void ExplainQuest() { 		
		if(isChatting == true)
			return;

		nowDiscription = "";


		if(nowKey == null) {

			//int idx = Random.Range(0, listKeyInfo.Count);
			nowKey = listKeyInfo[nowKeyIdx];

		} else {

			if(nowKey.getIsSuccess()) {
				//listKeyInfo.Remove(nowKey);
				Debug.Log("Welcome!");
				
				if(nowKeyIdx < listKeyInfo.Count) {
					nowKeyIdx++;
					nowKey = listKeyInfo[nowKeyIdx];
					
					GameObject.Find("/Chapter_" + (nowKeyIdx-1)).SetActive(false);
					GameObject.Find("/Chapter_" + nowKeyIdx).SetActive(true);
				} else {
					// End Game.
				}
			} else {
				//return;	
			}

		}
		
		Time.timeScale = 0.0f;
		player.GetComponent<MouseLook>().enabled = false;
		GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = false;
		
		StartCoroutine(this.typeDiscription(nowKey.getNowKeyDiscription()));
	}

	IEnumerator typeDiscription(string discription) {

		realDiscription = discription;
		int i=50;
		while(realDiscription.Length >= i) {
			realDiscription = realDiscription.Insert(i, "\n");
			i += 50;
		}

		isChatBoxOff = false;
		int count = 0;
		while(nowDiscription.Length < realDiscription.Length) {
			isChatting = true;
			
			nowDiscription += realDiscription[count];
			count++;

			yield return null;
			yield return null;
		}

		Debug.Log(realDiscription);

		isChatting = false;
	}
	
	void AI()
    {
		Debug.Log(posList);
		if (Vector3.Distance(this.transform.position, posList[posIdx]) < 10.0f)
        {
            if (posIdx < posList.Count - 1)
            {
                agent.SetDestination(posList[++posIdx]);
            }
            else
            {
                posIdx = 0;
                agent.SetDestination(posList[posIdx]);
            }
        }
        else {
            agent.SetDestination(posList[posIdx]);
			
		}
    }
}