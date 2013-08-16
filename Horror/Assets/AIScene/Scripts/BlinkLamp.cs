using UnityEngine;
using System.Collections;

public class BlinkLamp : MonoBehaviour {
	
	public float termMin = 0.05f;
	public float termMax = 0.1f;
	
	public float delayTime = 0.0f;
	
	// Use this for initialization
	void Start () {
		
		StartCoroutine("Blink");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator Blink() {
		
		yield return new WaitForSeconds(delayTime);
		
		int count = 0;
		int maxCount = 10;
		while(true) {
			
			this.light.enabled = !this.light.enabled;
			
			count++;
			
			if(count < maxCount) {
				yield return new WaitForSeconds(Random.Range(termMin, termMax));
			} else {
				count = 0;
				this.light.enabled = false;
				yield return new WaitForSeconds(Random.Range(4.0f, 7.0f));
			}
			
		}
	}
}
