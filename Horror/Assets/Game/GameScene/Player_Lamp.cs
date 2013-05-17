using UnityEngine;
using System.Collections;

public class Player_Lamp : MonoBehaviour
{
    bool light_flag = true;
    public GameObject StandLight;
    bool mouse_flag = false;

	void Start ()
    {
	}

	void FixedUpdate ()
    {
        if (Input.GetMouseButtonDown(1) == true && mouse_flag == false)
        {
            mouse_flag = true;
            Switch();
        }
        else if(Input.GetMouseButtonUp(1) == true)
        {
            mouse_flag = false;
        }
	}

    void Switch()
    {
        if (light_flag == true)
        {
            StandLight.light.range = 0;
            light_flag = false;
        }
        else
        {
            StandLight.light.range = 75;
            light_flag = true;
        }
    }
}
