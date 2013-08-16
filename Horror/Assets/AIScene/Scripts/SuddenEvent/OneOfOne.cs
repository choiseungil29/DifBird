using UnityEngine;
using System.Collections;

public class OneOfOne : SuddenEvent { // add Component in Note
	
	public Texture2D letter;
	
	private bool viewLetter = false;
	
	private GameObject ghost;
	
	private bool isMouseOver = false;
	private bool isShoutting = false;
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
		
		isLoop = false;
		beQualified = false;
		
		ghost = GameObject.Find("/Ghost");
		ghost.renderer.enabled = false;
		ghost.GetComponent<Animation>().wrapMode = WrapMode.ClampForever;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		
		if(Input.GetMouseButtonDown(0)) {
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
			RaycastHit hit;
			
			Debug.Log(Input.mousePosition);
			
			if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				if(hit.transform.gameObject == this.gameObject) {
					
					// view 2d Texture
					// write to this note is my note...
					viewLetter = true;
					
					Time.timeScale = 0.0f;
					player.GetComponent<MouseLook>().enabled = false;
					GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = false;
				}
			}
		} else {
			
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
			RaycastHit hit;
			
			//Debug.Log(Input.mousePosition);
			
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
		
		/*Vector2 Coordinates = Camera.main.WorldToScreenPoint(ghost.transform.position);
		if(Coordinates.x > 0 &&
			Coordinates.x < Screen.width &&
			Coordinates.y > 0 &&
			Coordinates.y < Screen.height) {
			ghost.GetComponent<Animation>().Play();
		}*/
		//Debug.Log(Coordinates);
		if((ghost.renderer.isVisible == true) && 
			(ghost.renderer.enabled == true) &&
			(!ghost.GetComponent<Animation>().isPlaying) &&
			(isShoutting == false)) {
			ghost.GetComponent<Animation>().Play();
			ghost.GetComponent<AudioSource>().Play();
			isShoutting = true;
		}
		
		if(ghost.renderer.isVisible == true) {
			Debug.Log("Ghost is Visible");
		}
		
	}
	
	protected override void SuddenEventLoop() { // just one call
		base.SuddenEventLoop();
		
		Vector3 pPosition = player.transform.position;
		pPosition = pPosition + Vector3.Scale(player.transform.forward, new Vector3(1, 0, 1)) * 4;
		
		ghost.transform.position = new Vector3(pPosition.x, pPosition.y + 7, pPosition.z);
		ghost.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y + 25 + 180, player.transform.eulerAngles.z);
		ghost.renderer.enabled = true;
		// position set;
		
	}
	
	void OnGUI() {
		
		int margin = 20;
		
		if(viewLetter == true) {
			
			Screen.showCursor = true;
			
			GUI.Box(new Rect((Screen.width - letter.width)/2, (Screen.height - letter.height)/2, letter.width, letter.height), letter);
			if(GUI.Button(new Rect(Screen.width/2 + letter.width/2 - margin, Screen.height/2 - letter.height/2, margin, margin), "X")) {
				viewLetter = false;
				beQualified = true;
				
				Screen.showCursor = false;
				
				Time.timeScale = 1.0f;
				player.GetComponent<MouseLook>().enabled = true;
				GameObject.Find("/Player/Main Camera").GetComponent<MouseLook>().enabled = true;
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
