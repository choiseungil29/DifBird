using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;

public class Enemy : MonoBehaviour {
	
	public GameObject target;

    private NavMeshAgent agent;
    private List<Vector3> posList;
    private int dest_idx;

    private Dictionary<string, List<Contents>> eventMap; // EventName, 0 : CharacterName, 1 : Discription

    private GUIText text;

    private int flag = 0;
    private bool discoverCharacter = false;
    private bool spacebarDown = false;

    void Awake()
    {
        SqliteConnection connection = new SqliteConnection("URI=file:" + "Test.db");
        connection.Open();
        Debug.Log("Connected DB");

        SqliteCommand sqlCmd = new SqliteCommand(connection);
        sqlCmd.CommandText = "SELECT * FROM Position";
        SqliteDataReader reader = sqlCmd.ExecuteReader();

        string[] readArray = new string[reader.RecordsAffected];
        Debug.Log(reader.RecordsAffected);

        posList = new List<Vector3>();
        while (reader.Read())
        {
            Debug.Log("(" + reader.GetFloat(0) + ", " + reader.GetFloat(1) + ", " + reader.GetFloat(2) + ")");
            posList.Add(new Vector3(reader.GetFloat(0), reader.GetFloat(1), reader.GetFloat(2)));
        }

        reader.Close();
        connection.Close();

        eventMap = new Dictionary<string, List<Contents>>();
        List<Contents> contentsList = new List<Contents>();

        // reading database code

        connection.ConnectionString = "URI=file:" + "EventDB.db";
        connection.Open();

        SqliteCommand sqlCmd2 = new SqliteCommand(connection);
        sqlCmd2.CommandText = "SELECT * FROM Event";
        SqliteDataReader reader2 = sqlCmd2.ExecuteReader();

        string eventName = "";
        while (reader2.Read())
        {
            eventName = reader2.GetString(0);
            Contents content = new Contents(reader2.GetString(1), reader2.GetString(2));
            Debug.Log("Event Name : " + eventName);
            Debug.Log("Character Name : " + content.charName);
            Debug.Log("Description : " + content.description);
            contentsList.Add(content);
        }

        eventMap.Add(eventName, contentsList);

        reader2.Close();
        connection.Close();

        text = this.GetComponent<GUIText>();
    }
	
	// Use this for initialization
	void Start () 
    {
        dest_idx = 0;

        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = posList[dest_idx];
	}
	
	// Update is called once per frame
	void Update () 
    {
        // TestCode(); It just test code

        if (Input.GetKeyDown("space"))
        {
            spacebarDown = true;
        }

        if (spacebarDown)
        {
        }
	}

    void FixedUpdate()
    {
        if (!discoverCharacter)
        {
            AI(); // AI Code
        }
        else
        {
            return; // relative navigation agent code.
        }
    }

    private void OnDrawGizmos()
    {
    }

    void AI()
    {
        if (Vector3.Distance(this.transform.position, posList[dest_idx]) < 10.0f)
        {
            if (dest_idx < posList.Count - 1)
            {
                agent.SetDestination(posList[++dest_idx]);
            }
            else
            {
                dest_idx = 0;
                agent.SetDestination(posList[dest_idx]);
            }
        }
    }

    private Vector3 RotationAroundPoint(Vector3 pivot, Vector3 point, Quaternion angle)
    {
        Vector3 finalPoint = point - pivot;
        finalPoint = angle * finalPoint;

        finalPoint += pivot;

        return finalPoint;
    }

    private Vector3 WorldToLocalPoint(Vector3 point)
    {
        return this.RotationAroundPoint(this.transform.position, point, this.transform.rotation);
    }
}
