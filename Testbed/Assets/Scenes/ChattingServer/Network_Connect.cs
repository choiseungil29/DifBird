using UnityEngine;
using System.Collections;

public class Network_Connect : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        Debug.Log("Start");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnConnectedToPhoton()
    {
        Debug.Log("OnConnectedToPhoton");
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("OnJoinedLobby()");
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom("test", true, true, 2);
        Debug.Log("OnPhotonRandomJoinFailed()");
    }

    void OnJoinedRoom()
    {
        Debug.Log("OK!");
    }
}
