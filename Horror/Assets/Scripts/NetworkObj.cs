using UnityEngine;
using System.Collections;

public class NetworkObj : Photon.MonoBehaviour
{
	private Vector3 correctPlayerPos = Vector3.zero;
	private Quaternion correctPlayerRot = Quaternion.identity;

	// Use this for initialization
	void Awake ()
	{
		if(GetComponent<GameCharacter>()!=null) GetComponent<GameCharacter>().Set_isControl(photonView.isMine);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//여기서 중요한건 내 Object가 아니라 Network상의 다른 
		// Object의 Position과 rotation을 업데이트 시키는 겁니다.
		if(photonView.isMine == true) return;
		Debug.Log("isMine Success");
		//원래 위치와 업데이트된 위치가 2f이상 차이가 나면 즉시 반영하고
		//아니라면 부드럽게 움직이도록 설정합니다.
		float distance = Vector3.Distance(transform.position, this.correctPlayerPos);
		if(distance < 2f)
		{
			transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
			
		}
		else
		{
			transform.position = this.correctPlayerPos;
			transform.rotation = this.correctPlayerRot;
		}
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonSerializeView Success");
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}
}
