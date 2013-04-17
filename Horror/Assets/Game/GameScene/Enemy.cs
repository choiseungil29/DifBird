using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject target;

    private NavMeshAgent agent;
    private RaycastHit hit;
    private Ray ray;
	
	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;

        hit = new RaycastHit();
        ray = new Ray();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        float rad = 70.0f * Mathf.Deg2Rad;
        float distance = 20.0f;

        Vector3 to_1 = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
        Vector3 to_2 = new Vector3(-Mathf.Cos(rad), 0, Mathf.Sin(rad));

        float line_distance = Vector2.Distance(
            new Vector2(0.0f, 0.0f),
            new Vector2(distance * Mathf.Cos(rad), distance * Mathf.Sin(rad))
            );
        

        if (Physics.Raycast(this.transform.position, this.transform.InverseTransformDirection(to_1), out hit, line_distance))
        {
            if (hit.collider.gameObject.tag != "Character")
            {
                // Right light manage
                Debug.Log("Right light");
                Debug.Log(hit.collider.gameObject.tag);       
            }
        }
        Vector3 distance_to_1 = new Vector3(this.transform.position.x + Mathf.Cos(rad) * distance, this.transform.position.y, this.transform.position.z + Mathf.Sin(rad) * distance);
        Debug.DrawRay(this.transform.position, this.transform.InverseTransformDirection(distance_to_1), Color.blue);

        if (Physics.Raycast(this.transform.position, this.transform.InverseTransformDirection(to_2), out hit, line_distance))
        {
            if (hit.collider.gameObject.tag != "Character")
            {
                // Left light manage
                Debug.Log("Left light");
                Debug.Log(hit.collider.gameObject.tag);
            }
        }
        Vector3 distance_to_2 = new Vector3(this.transform.position.x - Mathf.Cos(rad) * distance, this.transform.position.y, this.transform.position.z + Mathf.Sin(rad) * distance);
        Debug.DrawRay(this.transform.position, this.transform.InverseTransformDirection(distance_to_2), Color.blue);


        Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.z);
        if (Vector2.Distance(targetPos, thisPos) > 10.0f) // Stop Enemy AI
        {
            agent.Stop();
            return;
        }

        agent.destination = target.transform.position;
    }

    private void OnDrawGizmos()
    {
        float rad = 70.0f * Mathf.Deg2Rad;
        float distance = 20.0f;

        Gizmos.color = Color.red;

        Vector3 from = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector3 to_1 = new Vector3(this.transform.position.x + distance * Mathf.Cos(rad), this.transform.position.y, this.transform.position.z + distance * Mathf.Sin(rad));
        Vector3 to_2 = new Vector3(this.transform.position.x - distance * Mathf.Cos(rad), this.transform.position.y, this.transform.position.z + distance * Mathf.Sin(rad));

        /*Gizmos.DrawLine(from, to_1);
        Gizmos.DrawLine(from, to_2);*/
    }
}
