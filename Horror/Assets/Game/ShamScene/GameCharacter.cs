using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;

public class GameCharacter : MonoBehaviour
{

    public Camera camera = null;
    public Camera camera2 = null;
    public Texture2D cursorTexture;

    private int speed = 40;
    private int cameraDistance = -1;

    private int xSpeed = 250;
    private int ySpeed = 120;

    private float x = 0.0f;
    private float y = 0.0f;

    private float gravity = -10;

    private List<Vector3> startingList;

    public bool isControl = false;

    // Use this for initialization
    void Start()
    {
        Vector3 angle = this.transform.eulerAngles;
        x = angle.x;
        y = angle.y;

        if (this.rigidbody)
            this.rigidbody.freezeRotation = true;

        //Starting();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Rotation();
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.32f, this.transform.position.z);
    }

    void Movement()
    {
        this.transform.Translate(Vector3.forward * Input.GetAxis("Forward") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.left * Input.GetAxis("Left") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.back * Input.GetAxis("Back") * Time.fixedDeltaTime * speed);
        this.transform.Translate(Vector3.right * Input.GetAxis("Right") * Time.fixedDeltaTime * speed);
        this.transform.Translate(new Vector3(0, gravity, 0) * Time.fixedDeltaTime);
        // Character Movement
    }

    void Rotation()
    {
        Vector3 CharacterPos = this.transform.position;
        camera.transform.position = new Vector3(CharacterPos.x, CharacterPos.y, CharacterPos.z - cameraDistance);
        camera.transform.LookAt(this.transform);

        x += Input.GetAxis("Mouse X") * xSpeed * 0.008f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.008f; // 0.02f -> Mouse Sensibility

        y = Mathf.Clamp(y, -80.0f, 80.0f);

        this.transform.rotation = Quaternion.Euler(0, x, 0); // axis of x, y, z rotation value transform

        camera2.transform.rotation = Quaternion.Euler(y, x, 0);

        Vector3 characterAng = this.transform.rotation.eulerAngles;
        camera.transform.RotateAround(CharacterPos, camera.transform.TransformDirection(Vector3.up), characterAng.y);
        camera.transform.Rotate(Vector3.up * 180.0f);
        camera.transform.RotateAround(CharacterPos, camera.transform.TransformDirection(Vector3.right), characterAng.x);
        //camera.transform.Rotate(Vector3.right * 180.0f);
    }

    void OnGUI()
    {
    }

    private void Starting()
    {
        SqliteConnection connection = new SqliteConnection(string.Format("Data Source={0}", "Starting.db"));
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
        Debug.Log(this.transform.position);
        Debug.Log(i);
        Debug.Log("Success!");
    }
}
