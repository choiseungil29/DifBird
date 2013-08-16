using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;

public class AIOfGuard : MonoBehaviour {

    public int distance = 0;

    private NavMeshAgent agent;
    private GameObject target;
	
	private List<Vector3> posList;
	private int posIdx;

	// Use this for initialization
	void Start () 
    {
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.Find("/Player");
		
		SqliteConnection connection = new SqliteConnection(string.Format("Data Source={0}", "Test.db"));
        connection.Open();

        SqliteCommand sqlCmd = new SqliteCommand(connection);
        sqlCmd.CommandText = "SELECT * FROM Position";
        SqliteDataReader reader = sqlCmd.ExecuteReader();
		
		posList = new List<Vector3>();
		posIdx = 0;
		
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
        if (this.hasObstacle())
            AI(); // DB의 경로대로 움직임
        else {
            agent.SetDestination(target.transform.position); // 경비의 Run기능 추가구현 
			AudioSource[] array = target.GetComponents<AudioSource>();
			foreach(AudioSource source in array) {
				if((source.clip.name == "NeedRunning") &&
					(source.isPlaying == false)) {
					source.Play();
				}
			}
		}
	}

    bool hasObstacle()
    {
        Vector3 rayPosition = this.transform.position;
        rayPosition.y += 5;

        Debug.DrawRay(rayPosition, this.transform.TransformDirection(Vector3.forward) * distance, Color.red);

        Quaternion rotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
        this.transform.rotation = rotation;
        this.transform.eulerAngles = Vector3.Scale(this.transform.eulerAngles, Vector3.up);

        RaycastHit hit;
        if (Physics.Raycast(rayPosition, this.transform.TransformDirection(Vector3.forward) * distance, out hit))
        {
            // SetDestination!
            if (hit.transform.name == target.transform.name)
                return false;
            else
                return true;
        }

        return true;
    }
	
	void AI()
    {
		if(Vector3.Distance(this.transform.position, target.transform.position) < 100.0f) {
			if((Input.GetKeyDown(KeyCode.A) || 
				(Input.GetKeyDown(KeyCode.S) || 
				(Input.GetKeyDown(KeyCode.D) || 
				(Input.GetKeyDown(KeyCode.W)))))) {
				agent.SetDestination(target.transform.position);
			} else {
				agent.SetDestination(posList[posIdx]);
			}
				
	    
		} else {
			
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
}
