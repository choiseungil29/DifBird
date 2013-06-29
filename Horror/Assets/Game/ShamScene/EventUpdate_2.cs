using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

public class EventUpdate_2 : MonoBehaviour
{
    private List<eventclass> posList;
    public GameObject player;
    private float px;
    private float py;
    private float pz;

    private string Name;
    private string CharacterName;
    private string Discription;

    private bool out_flag = false;
    private bool lbutton_down = false;

    private float LButton_Time = 0f;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetMouseButtonUp(0) == true)
        {
            LButton_Time -= Time.fixedDeltaTime;
            Debug.Log("MouseButton1");
            if (lbutton_down == true && out_flag == true)
            {
                Debug.Log("MouseButton2");
                foreach (eventclass s in posList)
                {
                    if (s.getFlag() == true)
                    {
                        s.setFlag(false);
                        Name = s.getName();
                        CharacterName = s.getCharacter();
                        Discription = s.getDiscription();
                        Debug.Log(Discription);
                    }
                }
            }
        }
        if (out_flag == true)
        {
            //if (nullcheck() == true) out_flag = false;
        }


        px = player.transform.position.x;
        py = player.transform.position.y;
        pz = player.transform.position.z;
        if (out_flag == false)
        {
            if (eventLogic(61, 19, 37, "Event1"))
            {
                Debug.Log("Event 1 ½ÇÇà");
                out_flag = true;
            }
        }
        //if (GetComponent<Event>() != null) GetComponent<Event>().posList;
        if (Input.GetMouseButtonDown(0) == true)
            LButton_Time = 0.5f;
        
	}
    bool nullcheck()
    {
        foreach (eventclass s in posList)
        {
            if (s.getFlag() == true)
            {
                return false;
            }
        }
        return true;
    }
    bool eventLogic(float x, float y, float z, string EName)
    {
        bool flag = false;
        if (px >= x - 10 && px <= x + 10) flag = true; else flag = false;
        if (py >= y - 10 && py <= y + 10) flag = true; else flag = false;
        if (pz >= z - 10 && pz <= z + 10) flag = true; else flag = false;

        if (flag == false) return false;

        posList = new List<eventclass>();
        string dbtext = EName + ".db";
        string dbConnection = string.Format("Data Source={0}", dbtext);
        SqliteConnection connection = new SqliteConnection(dbConnection);
        connection.Open();
        SqliteCommand sqlcmd = new SqliteCommand(connection);
        sqlcmd.CommandText = "SELECT * FROM Event;";
        SqliteDataReader reader = sqlcmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(reader);

        DataRow[] rows = dt.Select();

        posList.Clear();

        foreach (DataRow row in rows)
        {
            posList.Add(new eventclass(row[0].ToString(), row[1].ToString(), row[2].ToString()));
            Debug.Log(row[0].ToString() + ", " + row[1].ToString() + ", " + row[2].ToString());
        }
        reader.Close();
        connection.Close();

        foreach (eventclass s in posList)
        {
            if (s.getFlag() == true)
            {
                s.setFlag(false);
                Name = s.getName();
                CharacterName = s.getCharacter();
                Discription = s.getDiscription();
                break;
            }
        }
        return flag;
    }
    void OnGUI()
    {
        if (out_flag == true)
        {
            GUILayout.Label("Name : " + Name);
            GUILayout.Label("CharacterName : " + CharacterName);
            GUILayout.Label("Discription : " + Discription);
        }
    }
}
