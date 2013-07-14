using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;

public class Sudden_event : MonoBehaviour
{
    
    public class Inform
    {
        int _NUM; // 이벤트 순서
        string _eventname; // 이벤트 이름
        Vector3 _position; // 돌발 이벤트가 일어나는 좌표
        bool _looping; // 한번 실행되고 끝나는 이벤트인지 아닌지 체크

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

    AnimEvent[] pAnim;

    string str;

    delegate void AnimEvent();

    void Start()
    {
        list = new List<Inform>();

        /////////////////// 함수 추가 부분 ////////////////////////////
        AnimEvent[] temps = {
                                new AnimEvent(suddenevent_move.event1),
                                new AnimEvent(suddenevent_move.event2),
                                new AnimEvent(suddenevent_move.gaa)
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

        string dbtext = "SubEventDB.db";
        string dbConnection = string.Format("Data Source={0}", dbtext);
        SqliteConnection connection = new SqliteConnection(dbConnection);
        connection.Open();
        SqliteCommand sqlcmd = new SqliteCommand(connection);
        sqlcmd.CommandText = "SELECT * FROM SubEvent;";
        SqliteDataReader reader = sqlcmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(reader);

        DataRow[] rows = dt.Select();

        list.Clear();

        foreach (DataRow row in rows)
        {
            Vector3 pVec = new Vector3(
                            float.Parse(row[2].ToString()),
                            float.Parse(row[3].ToString()),
                            float.Parse(row[4].ToString()));

            list.Add(new Inform(
                            int.Parse(row[0].ToString()),
                            row[1].ToString(),
                            pVec,
                            bool.Parse(row[5].ToString())
                            ));
        }
        reader.Close();
        connection.Close();

        ///////////////////////////////////////////////////////////////
        
    }
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 10, Screen.height / 10, 400, 100), "Debug : " + str);
    }
    void Update()
    {


        Debug.Log("list Count : " + list.Count);
        foreach (Inform v in list)
        {
            Debug.Log("Distance : " + Vector3.Distance(this.transform.position, v.getposition()));
            if (Vector3.Distance(this.transform.position, v.getposition()) <= 30f)
            {
                pAnim[v.getNUM()]();

                str = v.geteventname();

                if (v.getlooping() == false)
                {
                    list.Remove(v);
                }
            }
        }


    }
}
