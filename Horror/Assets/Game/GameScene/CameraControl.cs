using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour {

    private List<Vector3> startingList;

    public GameObject Enemy;

	// Use this for initialization
	void Start () {
        Starting();

        Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float speed = 18.0f;
        //float scrollSpeed = 300.0f;
        //this.transform.Translate(Vector3.forward * Input.GetAxis("Forward") * Time.fixedDeltaTime * speed);
        //this.transform.Translate(Vector3.left * Input.GetAxis("Left") * Time.fixedDeltaTime * speed);
        //this.transform.Translate(Vector3.back * Input.GetAxis("Back") * Time.fixedDeltaTime * speed);
        //this.transform.Translate(Vector3.right * Input.GetAxis("Right") * Time.fixedDeltaTime * speed);
        //this.transform.Translate(Vector3.up * Input.GetAxis("Mouse ScrollWheel") * Time.fixedDeltaTime * scrollSpeed * -1);
        // TestCode

        if (Vector3.Distance(this.transform.position, Enemy.transform.position) < 10.0f)
        {
            // Lose Game!
            Application.LoadLevel("Lose");
        }

        Debug.Log(Application.dataPath);
	}

    private void Starting()
    {
        SqliteConnection connection = new SqliteConnection(string.Format("Data Source={0}", "Starting.db"));
        //SqliteConnection connection = new SqliteConnection("Starting.db");
        connection.Open();

        SqliteCommand sqlCmd = new SqliteCommand(connection);
        sqlCmd.CommandText = "SELECT * FROM Position";
        SqliteDataReader reader = sqlCmd.ExecuteReader();

        startingList = new List<Vector3>();
        while (reader.Read())
        {
            startingList.Add(new Vector3(reader.GetFloat(0), reader.GetFloat(1), reader.GetFloat(2)));
        }
        reader.Close();
        connection.Close();

        int i = Random.Range(0, 5);
        this.transform.position = startingList[i];
    }
}
