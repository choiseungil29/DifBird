using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	
	public GameObject target;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public Transform point5;
    public Transform point6;
    public Transform point7;
    public Transform point8;
    public Transform point9;
    public Transform point10;

    private NavMeshAgent agent;
    private RaycastHit hit;

    private int flag = 0;
    private bool discoverCharacter = false;
	
	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = point1.position;

        hit = new RaycastHit();
	}
	
	// Update is called once per frame
	void Update () {
        // TestCode(); It just test code
	}

    void FixedUpdate()
    {
        if (discoverCharacter)
            AI(); // AI Code
        else
            return; // relative navigation agent code.

        Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.z);
        if (Vector2.Distance(targetPos, thisPos) > 10.0f) // Stop Enemy AI
        {
            //agent.Stop();
            return;
        }

        //agent.destination = target.transform.position;
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

    void AI()
    {
        float rad = 70.0f * Mathf.Deg2Rad;
        float distance = 15f;

        Vector3 LeftLine = new Vector3(
                this.transform.position.x - Mathf.Cos(rad) * distance,
                this.transform.position.y,
                this.transform.position.z + Mathf.Sin(rad) * distance);
        Debug.DrawLine(this.transform.position, this.WorldToLocalPoint(LeftLine), Color.black);

        Vector3 RightLine = new Vector3(
                this.transform.position.x + Mathf.Cos(rad) * distance,
                this.transform.position.y,
                this.transform.position.z + Mathf.Sin(rad) * distance);
        Debug.DrawLine(this.transform.position, this.WorldToLocalPoint(RightLine), Color.black);

        float line_distance = Vector3.Distance(this.transform.position, LeftLine);

        if (Physics.Raycast(this.transform.position, this.WorldToLocalPoint(LeftLine), out hit, line_distance, ~(1 << LayerMask.NameToLayer("Guard")))) // Left light manage
        {
            Debug.Log("Left light");
            Debug.Log(hit.collider.gameObject.tag);
            Debug.Log(hit.collider.gameObject.transform);

            this.transform.Rotate(Vector3.down, 2f);
        }

        if (Physics.Raycast(this.transform.position, this.WorldToLocalPoint(RightLine), out hit, line_distance, ~(1 << LayerMask.NameToLayer("Guard")))) // Right light manage
        {
            Debug.Log("Right light");
            Debug.Log(hit.collider.gameObject.tag);
            Debug.Log(hit.collider.gameObject.transform);

            this.transform.Rotate(Vector3.up, 2f);
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

    void TestCode()
    {
        if (Vector3.Distance(agent.destination, this.transform.position) < 10.0f)
        {
            switch (flag)
            {
                case 0:
                    agent.SetDestination(point2.position);
                    break;
                case 1:
                    agent.SetDestination(point3.position);
                    break;
                case 2:
                    agent.SetDestination(point4.position);
                    break;
                case 3:
                    agent.SetDestination(point5.position);
                    break;
                case 4:
                    agent.SetDestination(point6.position);
                    break;
                case 5:
                    agent.SetDestination(point7.position);
                    break;
                case 6:
                    agent.SetDestination(point8.position);
                    break;
                case 7:
                    agent.SetDestination(point9.position);
                    break;
                case 8:
                    agent.SetDestination(point10.position);
                    break;
                default:
                    flag = 0;
                    break;
            }
            flag++;
        }
    }
}
