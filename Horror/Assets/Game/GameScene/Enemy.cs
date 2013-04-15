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
        if (agent.speed > 50.0f)
            Debug.Log("Broken");

        if (Physics.Raycast(this.transform.position, this.transform.position, out hit, 100))
        {
            if (hit.collider.gameObject.tag != "Character")
            {
                // 추가처리하기
            }
        }

        Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.z);
        /*if (Vector2.Distance(targetPos, thisPos) > 50.0f) // Stop Enemy AI
        {
            agent.Stop();
            return;
        }*/

        agent.destination = target.transform.position;
    }
}