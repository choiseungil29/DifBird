using UnityEngine;
using System.Collections;

public class Suddenevent_move : MonoBehaviour {


    
	// Use this for initialization
	void Start ()
    {
	}
	
	public void StartEvent()
	{
		Debug.Log("Start");
	}
	
    public static void event1()
    {
        Debug.Log("event1");
    }
    public static void event2()
	{
        Debug.Log("event2");
    }
    public static void gaa()
    {
        Debug.Log("gaa");
    }
}
