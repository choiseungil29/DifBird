using UnityEngine;
using System.Collections;

public class StartEventForPlayer : SuddenEvent {
	
	public GameObject target;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		
		isLoop = false;
		beQualified = false;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		
		if(Vector3.Distance(this.transform.position, target.transform.position) < 30.0f) {
			beQualified = true;
		}
	}
			
	protected override void SuddenEventLoop() {
		base.SuddenEventLoop();
		
		Debug.Log("SuccesssefaWEgbvaerdhbaerhg");
	}
}
