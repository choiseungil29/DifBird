using UnityEngine;
using System.Collections;

public class GetMainQuest : MonoBehaviour {
	
	public GameObject target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Vector3.Distance(this.transform.position, target.transform.position) < target.GetComponent<Girl_Quest>().getChatMargin()) {
			
			if(Input.GetMouseButtonUp(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					if(hit.transform.gameObject == target) {
						// click girl.
						
						if(target.GetComponent<Girl_Quest>().getIsChatBoxOff() == true)
							target.GetComponent<Girl_Quest>().ExplainQuest();
						
					}
				}
			}
		}
	}
}
