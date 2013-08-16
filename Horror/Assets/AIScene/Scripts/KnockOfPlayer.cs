using UnityEngine;
using System.Collections;

public class KnockOfPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				
				if(Vector3.Distance(hit.transform.position, this.transform.position) < 30.0f) {
					
					if(hit.transform.gameObject.name == "Door") {
						hit.transform.parent.gameObject.GetComponent<Door>().KnockDoor();
					}
					
					Debug.Log(hit.transform.gameObject.name);
				}
			}
		}
		
	}
}
