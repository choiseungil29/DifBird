using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject target;
	
	private NavMeshAgent agent;
	
	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.destination = target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.z);
		Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.z);
		if(Vector2.Distance(targetPos, thisPos) > 15.0f) // Stop Enemy AI
		{
			agent.Stop();
			return;
		}
		
		agent.Resume();
		agent.destination = target.transform.position;
	}
}
