using UnityEngine;
using System.Collections;

public class Girl_Quest : MonoBehaviour {

    public GameObject player;
    private bool status;
    private int key_count = 8;
    private int[] key_inform = new int[8];
    private int now_key = 0;
    private string discription;
	// Use this for initialization
	void Start ()
    {
        status = false;
        now_key = Random.Range(1, 8);
        discription = "ㅁㄴㅇㄹ";// 내용초기화
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance <= 30f && status == false)
        {
            status = true;
        }
        else status = false;
	}
    void OnGUI()
    {
        if (status == true)
        {
            GUI.Label(new Rect(Screen.width / 10, Screen.height / 10, 400, 100), "니한테 주는 키의 힌트 값 : " + now_key);
            GUI.Label(new Rect(Screen.width / 10, Screen.height / 8, 400, 400), "내용 : " + discription);
        }
    }

    /// public
    
    public void set_keyInform(int[] _key_inform)
    {
        key_inform = _key_inform;
    }
    public void keyChange()
    {
        // 여자가 준 힌트에 대한 키를 찾으면 체인지
        // 
        now_key = Random.Range(1, 8);
        discription = "EEESSSS";
    }
    public int get_nowKey()
    {
        return now_key;
    }
}
