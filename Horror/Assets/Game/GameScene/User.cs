using UnityEngine;
using System.Collections;

public class User : Photon.MonoBehaviour {
	
	private Vector3 correctPlayerPos = new Vector3(31f,7f,62f);
    private Quaternion correctPlayerRot = Quaternion.identity;
	
	// Use this for initialization
	void Awake ()
	{
		if(GetComponent<GameCharacter_1>() != null) GetComponent<GameCharacter_1>().Set_isControl(photonView.isMine);
		if(GetComponent<GameCharacter_2>() != null) GetComponent<GameCharacter_2>().Set_isControl(photonView.isMine);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log(PhotonNetwork.GetPing());
		if (photonView.isMine == true) return;
		
        float distance = Vector3.Distance(transform.position, this.correctPlayerPos);
        if (distance > 0.01f)
        {
            Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
            Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
        }
        else
        {
            transform.position = this.correctPlayerPos;
            transform.rotation = this.correctPlayerRot;
        }
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
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
