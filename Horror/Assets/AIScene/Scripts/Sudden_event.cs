using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;

public class Sudden_event : MonoBehaviour
{
    
    public class Inform
    {
        int _NUM; // �̺�Ʈ ����
        string _eventname; // �̺�Ʈ �̸�
        Vector3 _position; // ���� �̺�Ʈ�� �Ͼ�� ��ǥ
        bool _looping; // �ѹ� ����ǰ� ������ �̺�Ʈ���� �ƴ��� üũ

        public Inform(int NUM, string eventname, Vector3 position, bool looping)
        {
            _NUM = NUM;
            _eventname = eventname;
            _position = position;
            _looping = looping;
        }

        public int getNUM() { return _NUM; }
        public string geteventname() { return _eventname; }
        public Vector3 getposition() { return _position; }
        public bool getlooping() { return _looping; }
    };

    List<Inform> list;
	List<Inform> removeList;

    AnimEvent[] pAnim;

    string str;

    delegate void AnimEvent();

    void Start()
    {
        list = new List<Inform>();
		removeList = new List<Inform>();

        /////////////////// �Լ� �߰� �κ� ////////////////////////////
        AnimEvent[] temps = {
								//new AnimEvent(Suddenevent_move.StartEvent),
                                new AnimEvent(Suddenevent_move.event1),
                                new AnimEvent(Suddenevent_move.event2),
                                new AnimEvent(Suddenevent_move.gaa)
                            };

        //////////////////////////////////////////////////////////////

        pAnim = temps;

        ///////////////////////////SQLite3///////////////////////////
        /*Inform temp = new Inform(0, "event1", new Vector3(-612.4244f, 267.8988f, -276.3685f), true);

        list.Add(temp);

        Inform temp2 = new Inform(1, "event2", new Vector3(-512.4244f, 767.8988f, -276.3685f), true);

        list.Add(temp2);

        Inform temp3 = new Inform(2, "gaa", new Vector3(-612.4244f, 767.8988f, -276.3685f), true);

        list.Add(temp3);*/

        string dbtext = "SubEvent.db";
        string dbConnection = string.Format("Data Source={0}", dbtext);
        SqliteConnection connection = new SqliteConnection(dbConnection);
        connection.Open();
        SqliteCommand sqlcmd = new SqliteCommand(connection);
        sqlcmd.CommandText = "SELECT * FROM SubEvent;";
        SqliteDataReader reader = sqlcmd.ExecuteReader();
		
		while(reader.Read()) {
			Debug.Log("" + reader["Name"]);
			list.Add(new Inform(
				int.Parse(reader["Num"].ToString()),
				reader["Name"].ToString(),
				new Vector3(int.Parse(reader["X"].ToString()),
					int.Parse(reader["Y"].ToString()),
					int.Parse(reader["Z"].ToString())),
				bool.Parse(reader["IsLoop"].ToString())));
			
		}
		
        reader.Close();
        connection.Close();

        ///////////////////////////////////////////////////////////////
        
    }
    void OnGUI()
    {
        //GUI.Label(new Rect(Screen.width / 10, Screen.height / 10, 400, 100), "Debug : " + str);
    }
    void Update()
    {
        foreach(Inform inform in list)
        {
            Debug.Log("Distance : " + Vector3.Distance(this.transform.position, inform.getposition()));
            if (Vector3.Distance(this.transform.position, inform.getposition()) <= 30f)
            {
                //pAnim[inform.getNUM()]();

                //str = inform.geteventname();
				
				this.SendMessage(inform.geteventname());

                if (inform.getlooping() == false)
                {
					removeList.Add(inform);
                }
            }
        }
		
		foreach(Inform inform in removeList) {
			list.Remove(inform);
		}
		removeList.Clear();
    }
}
