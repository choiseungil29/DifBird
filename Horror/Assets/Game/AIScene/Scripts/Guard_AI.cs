using UnityEngine;
using System.Collections;

public class Guard_AI : MonoBehaviour {

    public Transform target;
    public int distance = 0;

    NavMeshAgent agent;

	// Use this for initialization
	void Start () 
    {
        agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (this.hasObstacle())
            agent.SetDestination(this.transform.position);
        else
            agent.SetDestination(target.position);
        
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
            if (hit.transform.gameObject.name == "Character")
                return false;
            else // add AI code 
                return true;
        }

        return true;
    }
}
