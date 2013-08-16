using UnityEngine;
using System.Collections;

public class RoomOf2DEvent : SuddenEvent {
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
		
		isLoop = false;
		beQualified = false;
		Debug.Log("" + GameObject.Find("/Another/2D/Frank"));
		
		MeshRenderer[] array = GameObject.Find("/Another/2D/Frank").GetComponentsInChildren<MeshRenderer>();
		
		foreach(MeshRenderer renderer in array) {
			renderer.enabled = false;
		}
		
		GameObject.Find("/Another/2D/Frank").GetComponent<BoxCollider>().enabled = false;
		
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	
		if(Vector3.Distance(this.transform.position, player.transform.position) < 50.0f) {
			if(Input.GetMouseButtonUp(0)) {
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
				RaycastHit hit;
				
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					if((hit.transform.gameObject == this.gameObject)) {
						beQualified = true;
					}
				}
				
			} else {
			
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
				RaycastHit hit;
				
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					if(hit.transform.gameObject == this.gameObject) {
						
						ObjectOutliner outliner = this.GetComponent<ObjectOutliner>();
						outliner.enabled = true;
						
						this.renderer.material.SetFloat("_Outline", outliner.outlineSize);
						this.renderer.material.SetColor("_OutlineColor", outliner.outlineColor);
					} else {
						ObjectOutliner outliner = this.GetComponent<ObjectOutliner>();
						outliner.enabled = true;
						
						this.renderer.material.SetFloat("_Outline", 0);
						this.renderer.material.SetColor("_OutlineColor", new Color(0, 0, 0, 0));
					}
				}
			}
		}
	}
	
	protected override void SuddenEventLoop () {
		base.SuddenEventLoop ();

		MeshRenderer[] array = GameObject.Find("/Another/2D/Frank").GetComponentsInChildren<MeshRenderer>();		
		foreach(MeshRenderer renderer in array) {
			renderer.enabled = true;
		}
		
		GameObject.Find("/Another/2D/Frank").GetComponent<BoxCollider>().enabled = true;
		
	}
}
