
using UnityEngine;
using System.Collections;
 
public class MainGame : Photon.MonoBehaviour
{
 
    #region
    //public GameObject Player1;
    //public GameObject enemy;
    #endregion
	int s=0;
	
 
    private Vector3[] GenPoint = new Vector3[]{
        new Vector3(-4.5f, 10f, 0f), 
        new Vector3(-3f, 10f, 0f), 
        new Vector3(-1.5f, 10f, 0f),
        new Vector3(0f, 10f, 0f),
        new Vector3(1.5f, 10f, 0f),
        new Vector3(3f, 10f, 0f),
        new Vector3(4.5f, 10f, 0f)};
 
    private float GenStart = 0f;
    private float GenTerm = 3f;
    private float DropSpeed = 5f;
 
 
 
    // Use this for initialization
    void Start () {
        GenStart = Time.time;
        //Instantiate(Player1, new Vector3(0f, 0f, 0f), Quaternion.Euler(new Vector3(0f,0f,180f)));
    }
     
    // Update is called once per frame
    void Update () {
        if (PhotonNetwork.isMasterClient == true ) EnemyGen();
    }
 
    void EnemyGen()
    {
        if (GenStart + GenTerm > Time.time) return;
		
        //GenTerm -= 0.01f;
        DropSpeed += 0.02f;
        StartCoroutine("EnemyDrop");
        GenStart = Time.time;
    }
 
    IEnumerator EnemyDrop()
    {
        foreach (Vector3 genposition in GenPoint)
		{
			PhotonNetwork.InstantiateSceneObject("Enemy", genposition, Quaternion.identity, 9, null);
			//GameObject gobj = PhotonNetwork.InstantiateSceneObject("Enemy", genposition, Quaternion.identity, 9, null) as GameObject;
			Debug.Log("Enemy");
			/*if (gobj.GetComponent<Enemy>() != null)
			{
				Debug.Log ("not null");
				gobj.GetComponent<Enemy>().Set_Speed(DropSpeed);
			}*/
        }
        return null;
    }
}