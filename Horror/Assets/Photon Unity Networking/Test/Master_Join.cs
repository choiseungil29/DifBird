using UnityEngine;
using System.Collections;

public class Master_Join : Photon.MonoBehaviour
{
	public static int playerWhoIsIt = 0;
	private static PhotonView ScenePhotonView;
	// Use this for initialization
	
	void Start ()
	{
		PhotonNetwork.ConnectUsingSettings("1.0");
		ScenePhotonView = this.GetComponent<PhotonView>();
	}
	void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null,true,true,3);
	}
	void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		if(PhotonNetwork.playerList.Length == 1) playerWhoIsIt = PhotonNetwork.player.ID;
		string player = (PhotonNetwork.isMasterClient == true) ? "Player_1" : "Player_2";
		Debug.Log("Player : " + player);
		if (Gen(player) != null)
		{
			Debug.Log("Gen(player) != null");
			StartCoroutine(Gen (player));
		}
	}
	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected");
		if(PhotonNetwork.isMasterClient) TagPlayer(playerWhoIsIt);
	}
	public static void TagPlayer(int playerID)
	{
		Debug.Log("TagPlayer");
		ScenePhotonView.RPC ("TaggedPlayer", PhotonTargets.All, playerID);
	}
	IEnumerator Gen(string player)
	{
		Debug.Log("Gen");
		PhotonNetwork.Instantiate(player, new Vector3(0f,0f,0f), Quaternion.Euler(new Vector3(0f,0f,180f)),1);
		return null;
	}
	[RPC]
	void TaggedPlayer(int playerID)
	{
		Debug.Log("TaggedPlayer");
		playerWhoIsIt = playerID;
	}
	
	void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("Disconnected: " + player);
		
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
		Debug.Log ("OnMasterClientSwitched");
	}
}
