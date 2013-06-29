using UnityEngine;
using System.Collections;

public class BeatenOfPlayer : MonoBehaviour
{

    private const float beatenTime = 1.0f; // 1000 ms, just define variable
    private const int distance = 8; // just define variable

    private float lastBeatenTime; // uses variable
    private int lastDistance;

    private int PlayerHP = 5;

    // Use this for initialization
    void Start()
    {
        lastBeatenTime = beatenTime;
        lastDistance = 0;

        StartCoroutine("BeatenPlayer");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator BeatenPlayer()
    {
        GameObject Stalker = GameObject.Find("/Guard");

        while (true)
        {
            lastDistance = (int)Vector3.Distance(this.transform.position, Stalker.transform.position);

            if (lastDistance < distance)
            {
                lastBeatenTime -= Time.deltaTime;

                if (lastBeatenTime <= 0.0f)
                {
                    lastBeatenTime = beatenTime;
                    // beaten, camera shake, camera 2d color rendering

                    PlayerHP -= 1;

                    Stalker.gameObject.SendMessage("AnimHit");
                    //Stalker.GetComponent<HitOfGuard>().AnimHit();
                    // 둘다 된다
                }
            }
            else
            {
                lastBeatenTime = beatenTime;
            }

            yield return null;
        }
    }
}
