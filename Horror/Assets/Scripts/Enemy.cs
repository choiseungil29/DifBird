using UnityEngine;
using Pathfinding;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject target;

    private Seeker seeker;
    private CharacterController controller;
    private GridGraph graph;

    public Path path;
    public float speed = 10000.0f;
    public float nextWaypointDistance = 3.0f;

    private int currentWaypoint = 0;
    
	// Use this for initialization
	void Start () 
    {
        seeker = this.GetComponent<Seeker>();
        controller = this.GetComponent<CharacterController>();
        AstarPath astar = this.GetComponent<AstarPath>();
        graph = AstarPath.active.astarData.gridGraph;

        seeker.StartPath(this.transform.position, target.transform.position, this.OnPathComplete);
	}

    private void OnPathComplete(Pathfinding.Path p)
    {
        if (p.error)
        {
            Debug.Log("Error!");
            Debug.Log(p.errorLog);
        }
        else
        {
            Debug.Log("Success!");
            Debug.Log(p.vectorPath.Count);

            seeker.StartPath(this.transform.position, target.transform.position, this.OnPathComplete);

            path = p;
        }
    }

    public void FixedUpdate()
    {
        if (path == null)
        {
            Debug.Log("FixedUpdate -> path == null");
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("End Of Path Reached");
            return;
        }

        graph.center = target.transform.position;
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir = dir * speed * Time.fixedDeltaTime;
        controller.SimpleMove(dir);

        if (Vector3.Distance(this.transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
