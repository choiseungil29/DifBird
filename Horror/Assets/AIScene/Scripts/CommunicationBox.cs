using UnityEngine;
using System.Collections;

public class CommunicationBox : MonoBehaviour {
	
	public float chattingBoxMargin = 30.0f;
	
	private bool isChatBoxOn = false;
	private bool isChatting = false;
	
	private string nowDiscription;
	private string[] arrayDiscription;
	private int discriptionIdx;
	
	private static CommunicationBox instance = null;
	
	public static CommunicationBox getInstance() {
		
		if(instance == null) {
			//instance = FindObjectOfType(typeof(CommunicationBox)) as CommunicationBox;
			instance = new GameObject("Communicationbox").AddComponent<CommunicationBox>();
		}
		return instance;
	}
	
	private GameObject target;
	
	public void setTarget(GameObject obj) {
		target = obj;
	}
	
	private GameObject player;
	
	void Awake() {
		DontDestroyOnLoad(this);
		Debug.Log("Awake");
		
		isChatBoxOn = false;
		isChatting = false;
		
		arrayDiscription = new string[] {};
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log("Start");
		
		player = GameObject.Find("/Player");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonUp(0)) {
			discriptionIdx++;
			
			if(discriptionIdx < arrayDiscription.Length) {
				nowDiscription = arrayDiscription[discriptionIdx];
			} else {
				isChatting = false;
				isChatBoxOn = false;
				nowDiscription = "";
			}
		}
		
	}
	
	void OnGUI() {
		
		GUI.skin.box.fontSize = 15;
		
        if (isChatBoxOn == true) {
			int width = Screen.width/2;
            GUI.Box(new Rect((Screen.width - width)/2, Screen.height / 10 * 9, width, Screen.height / 10), "Discription : " + nowDiscription);
        }
		
		
		
	}
	
	public void Initiate(string[] discription, GameObject pTarget) {
		
		if(isChatting == true)
			return;
		
		arrayDiscription = discription;
		target = pTarget;
		discriptionIdx = 0;
		isChatting = true;
		isChatBoxOn = true;
		nowDiscription = arrayDiscription[discriptionIdx];
		
	}
	
	void OnApplicationQuit() {
		instance = null;
	}
}
