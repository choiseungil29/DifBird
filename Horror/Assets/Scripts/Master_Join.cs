using UnityEngine;
using System.Collections;

public class Master_Join : Photon.MonoBehaviour
{
	public static int playerWhoIsIt = 0;
	private static PhotonView ScenePhotonView;
	// Use this for initialization
	void Start ()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		ScenePhotonView = this.GetComponent<PhotonView>();//이녀석은 씬을 관리하는 PhotonView입니다.
	}
	
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null, true, true,2);
	}
	void OnJoinedRoom()
	{
		if(PhotonNetwork.playerList.Length == 1) playerWhoIsIt = PhotonNetwork.player.ID;
		string player = (PhotonNetwork.isMasterClient == true) ? "Player_1" : "Player_2";
	}
	
	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		if(PhotonNetwork.isMasterClient) TagPlayer(playerWhoIsIt);
	}
	public static void TagPlayer(int PlayerID)
	{
		ScenePhotonView.RPC ("TaggedPlayer", PhotonTargets.All, PlayerID);
	}
	IEnumerator Gen(string player)
	{
		PhotonNetwork.Instantiate(player, new Vector3(0f, 0f, 0f), Quaternion.Euler(new Vector3(0f,0f,180f)), 1 );
		return null;
	}
	
	[RPC]
	void TaggedPlayer(int playerID)
	{
		playerWhoIsIt = playerID;
	}
	
	void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log ("Disconnected : " + player);
		
		if(PhotonNetwork.isMasterClient)
		{
			if(player.ID == playerWhoIsIt)
			{
				TagPlayer(PhotonNetwork.player.ID);
			}
		}
	}
	
	void OnMasterClientSwitched()
    {
        Debug.Log("OnMasterClientSwitched");
    }
}
