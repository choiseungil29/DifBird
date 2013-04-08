using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    // Timer Count (Mills)
    private float count = 0;
    private float abs_count= 0;
    private timer_func var;
    private bool isSet = false;
    private bool isLoop = false;

    public delegate void timer_func();

	// Use this for initialization
	private void Start () 
    {

	}
	
	// Update is called once per frame
	private void Update () 
    {
        if (isSet == false)
            return;

        if (count <= 0)
        {
            var();

            if (this.isLoop == true)
                count = abs_count;
            else
                Destroy(this);
        }
        else if (count > 0)
        {
            count -= (Time.deltaTime * 1000);
        }
	}

    // param -> callback func
    // param_count -> Timer Count (Mills)
    // if has already settings Timer, quit function
    public void SetTimer(timer_func param, int count, bool loop = false)
    {
        if (this.isSet)
            return;

        this.var = param;
        this.count = (float)count;
        this.abs_count = (float)count;
        this.isSet = true;
        this.isLoop = loop;
    }
}
