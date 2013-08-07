using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetKey : MonoBehaviour {
	
	public List<GameObject> listKey;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		foreach(GameObject key in listKey) {
			
			if(Vector3.Distance(this.transform.position, key.transform.position) < 15.0f) {
			
				if(Input.GetMouseButtonDown(0)) {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					
					if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
						if(key != null) {
							if(hit.transform.gameObject == key) {
								// click key
								listKey.Remove(key);
								Destroy(key);
								
								GameObject girl = GameObject.Find("/girl");
								girl.GetComponent<Girl_Quest>().getNowKey().setIsSuccess(true);
							}
						}
					}
				}
			}
		}
	}
}
