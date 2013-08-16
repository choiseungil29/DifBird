using UnityEngine;
using System.Collections;

public class SuddenEvent : MonoBehaviour {
	
	protected bool beQualified;
	protected bool isLoop;
	private bool isEnd;
	
	protected GameObject player;
	
	public bool getBeQualified() {
		return beQualified;
	}
	
	// Use this for initialization
	protected virtual void Start () {
		beQualified = false;
		isLoop = false;
		isEnd  = true;
		
		player = GameObject.Find("/Player");
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
		if(beQualified == true &&
			isEnd == true) {
			
			this.SuddenEventLoop();	
			beQualified = false;
			isEnd = false;
		}
		
		if(isLoop == true &&
			isEnd == true) {
			
			beQualified = false;
		}
	}
	
	protected virtual void SuddenEventLoop() {
		
	}
}
