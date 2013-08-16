using UnityEngine;
using System.Collections;

public class BeatenOfPlayer : MonoBehaviour
{

    private const float beatenTime = 1.0f; // 1000 ms, just define variable
    private const int distance = 8; // just define variable

    private float lastBeatenTime; // uses variable
    private int lastDistance;

    private int PlayerHP = 3;

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
		if(PlayerHP < 0) {
			Application.LoadLevel("LoseScene");
		}
		
		Debug.Log("HP : " + PlayerHP);
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
					Debug.Log("WTFWTFWTF!!");

                    //Stalker.gameObject.SendMessage("AnimHit");
                    Stalker.GetComponent<HitOfGuard>().AnimHit();
                    // 둘다 된다
                    // sendMessage는 컴포넌트 내 모든 함수 호출용.

                    GameObject MainCamera = GameObject.Find("/Player/Main Camera");
                    MainCamera.GetComponent<ShakeOfCamera>().StartCoroutine("Shake");
                }
            }
            else
            {
                Stalker.GetComponent<HitOfGuard>().StopAnimHit();
                lastBeatenTime = beatenTime;
            }

            yield return null;
        }
    }
}
