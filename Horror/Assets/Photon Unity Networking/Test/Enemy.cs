using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	private float DropSpeed = 5f;
	
	void Update ()
	{
		Debug.Log ("Enemy Update");
		Dropit();
		CheckAutoDie();
	}
	void Dropit()
	{
		transform.Translate(new Vector3(0f,-1f,0f) * DropSpeed * Time.deltaTime);
	}
	void CheckAutoDie()
	{
		if (transform.position.y > -5f) return;
		StartCoroutine("Die");
	}
	public void Set_Speed(float x)
	{
		DropSpeed = x;
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag!="Beam") return;
		StartCoroutine("Die");
	}
	IEnumerator Die()
	{
		if(PhotonNetwork.isMasterClient) PhotonNetwork.Destroy(gameObject);
		return null;
	}
}
