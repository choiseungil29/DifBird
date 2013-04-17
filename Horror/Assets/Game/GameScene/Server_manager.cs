using UnityEngine;
using System.Collections;

public class Server_manager : Photon.MonoBehaviour {
	
    private Vector2 scrollPos = Vector2.zero;
    private string roomName = "";
    private string playerName = "";
    // Use this for initialization
	
	private GameObject player;
	
    void Start ()
	{
		roomName = "room" + PhotonNetwork.countOfRooms + 1;
		playerName = "Guest" + Random.Range(1, 9999);
    }

    // Update is called once per frame
    void Update ()
	{
    }
	
    void OnGUI()
    {
        GUILayout.Space(0);
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.connectionState == ConnectionState.Disconnected)
        {
            if (GUILayout.Button("Connect"))
            {
                PhotonNetwork.ConnectUsingSettings("1.0");
				Debug.Log("Server Connect");
            }
        }
        else if(PhotonNetwork.connectionStateDetailed.ToString() == "JoinedLobby")
        {
            if (GUILayout.Button("Disconnect"))
            {
                PhotonNetwork.Disconnect();
				Debug.Log ("Server Disconnect");
            }

            GUILayout.Label("Enter Room Name :");
            roomName = GUILayout.TextField(roomName);
            GUILayout.Label("Set Player Name :");
            playerName = GUILayout.TextField(playerName);
			PhotonNetwork.player.name = playerName;
            if (GUILayout.Button("Create Room"))
            {
                PhotonNetwork.CreateRoom(roomName,true, true,2);
				Debug.Log("Create Room");
            }
			if (GUILayout.Button("Join Room"))
            {
                PhotonNetwork.JoinRoom(roomName);
				Debug.Log("Join Room");
            }
			if (GUILayout.Button("Join Random Room"))
            {
                PhotonNetwork.JoinRandomRoom();
				Debug.Log("Join Random Room");
            }
			GUILayout.Label(PhotonNetwork.countOfPlayers + " users are online in " + PhotonNetwork.countOfRooms + " rooms.");
        	
			if (PhotonNetwork.GetRoomList().Length == 0)
        	{
	            GUILayout.Label("Currently no games are available.");
	        }
			else
			{
				GUILayout.Label(PhotonNetwork.GetRoomList() + " currently available. Join either:");
		
		        this.scrollPos = GUILayout.BeginScrollView(this.scrollPos);
		        foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
		        {
					if(roomInfo.playerCount != roomInfo.maxPlayers)
					{
			            GUILayout.Label(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers);
			            if (GUILayout.Button("Join"))
			            {
			                PhotonNetwork.JoinRoom(roomInfo.name);
			            }
					}
		        }
		        GUILayout.EndScrollView();
			}
        }
		else if(PhotonNetwork.connectionStateDetailed.ToString() == "Joined")
        {
			if (GUILayout.Button("Left Room"))
            {
                PhotonNetwork.LeaveRoom();
				Debug.Log ("Leave Room");
            }
			int i=0;
			foreach(PhotonPlayer p in PhotonNetwork.playerList)
			{
				i++;
			}
			GUILayout.Label("Room Name : " + roomName);
			GUILayout.Label("Player Count : " + i);
			GUILayout.Label("Current Join Player");
			i=0;
			foreach(PhotonPlayer p in PhotonNetwork.playerList)
			{
				i++;
				GUILayout.Label(i + " user : " + p.name);
			}
			if (i == 2)
			{
				if(GUILayout.Button ("Game Start"))
				{
					string name = (PhotonNetwork.isMasterClient == true) ? "Player_1" : "Player_2";
					player = PhotonNetwork.Instantiate( name, new Vector3(31f,7f,62f), Quaternion.identity, 0 );
				}
			}
		}

    }
	
	/*void OnPhotonInstantiate(PhotonMessageInfo info)
	{
	}*/
	void OnJoinedLobby()
	{
		Debug.Log("Joined Lobby");
	}
	void OnConnectionFail(DisconnectCause cause)
	{
		Debug.Log("ConnectionFail : " + cause);
	}
	void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.Log("OnFailedToConnectToPhoton : " + cause);
	}
	void OnPhotonJoinRoomFailed()
	{
		Debug.Log ("Join Room Failed ! ! !");
	}
	void OnPhotonPlayerConnected(PhotonPlayer Player)
	{
		Debug.Log ("Player Connected : " + Player.name);
	}
	void OnMasterClientSwitched()
	{
		Debug.Log ("MasterClientSwitched");
	}
	void OnReceivedRoomListUpdate()
	{
		Debug.Log ("OnReceivedRoomListUpdate");
	}
}
