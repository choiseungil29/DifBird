using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class FourFloorEvent : SuddenEvent {
	
	private float chatMargin = 30.0f;
	private bool viewLetter = false;
	
	public Texture2D letter; // view to letter. Shit. fucking coding.
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
	
		isLoop = true;
		beQualified = false;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		
		if(Vector3.Distance(this.transform.position, player.transform.position) < 30.0f) {
			if(Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
				RaycastHit hit;
				
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					if(hit.transform.gameObject == this.gameObject) {
						
						// view 2d Texture
						// write to this note is my note...
						viewLetter = true;
						Time.timeScale = 0.0f;
						player.GetComponent<MouseLook>().enabled = false;
						GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = false;
						
						beQualified = true;
						
					}
				}
				
			} else {
			
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
				RaycastHit hit;
				
				Debug.Log(Input.mousePosition);
				
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
	
	protected override void SuddenEventLoop ()
	{
		base.SuddenEventLoop ();
	}
	
	void OnGUI()
    {

		GUI.skin.box.fontSize = 15;

        if (viewLetter == true)
        {
			Screen.showCursor = true;
			int margin = 30;
			GUI.Box(new Rect((Screen.width - letter.width)/2, (Screen.height - letter.height)/2, letter.width, letter.height), letter);
			
			if(GUI.Button(new Rect(Screen.width/2 + letter.width/2 - margin, Screen.height/2 - letter.height/2, margin, margin), "X")) {
				
				viewLetter = false;
				beQualified = true;
				Time.timeScale = 1.0f;
				player.GetComponent<MouseLook>().enabled = true;
				GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = true;
				
				Screen.showCursor = false;
			}
        }
    }
	
	void OnMouseOver() {
		/*ObjectOutliner outliner = this.GetComponent<ObjectOutliner>();
		outliner.enabled = true;
		
		this.renderer.material.SetFloat("_Outline", outliner.outlineSize);
		this.renderer.material.SetColor("_OutlineColor", outliner.outlineColor);*/
	}
	
	void OnMouseExit() {
		/*ObjectOutliner outliner = this.GetComponent<ObjectOutliner>();
		outliner.enabled = true;
		
		this.renderer.material.SetFloat("_Outline", 0);
		this.renderer.material.SetColor("_OutlineColor", new Color(255, 255, 255, 255));*/
	}
}
