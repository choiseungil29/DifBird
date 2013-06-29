using UnityEngine;
using System.Collections;

public class Room_server : Photon.MonoBehaviour {
	
	
	public bool Rready = true;
	public bool ready = true;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log("Room_server");
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
        {
            stream.SendNext(this.ready);
            stream.SendNext(this.Rready);
        }
        else
        {
            this.ready = (bool)stream.ReceiveNext();
            this.Rready = (bool)stream.ReceiveNext();
        }
	}
}
