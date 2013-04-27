using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;

public class Enemy : MonoBehaviour {
	
	public GameObject target;

    private NavMeshAgent agent;
    private List<Vector3> posList;
    private int dest_idx;

    private int flag = 0;
    private bool discoverCharacter = false;

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
    }
	
	// Use this for initialization
	void Start () 
    {
		agent = gameObject.GetComponent<NavMeshAgent>();
        //agent.destination = target.transform.position;

        dest_idx = 0;
        agent.SetDestination(posList[dest_idx]);
	}
	
	// Update is called once per frame
	void Update () 
    {
        // TestCode(); It just test code
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
            if (dest_idx >= posList.Count)
                dest_idx = 0;
            else
                agent.SetDestination(posList[++dest_idx]);
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
