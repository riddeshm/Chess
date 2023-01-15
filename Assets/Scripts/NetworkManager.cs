using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomOrCreateRoom(null, 2, MatchmakingMode.FillRoom, null, 
            null, null, new RoomOptions() { MaxPlayers = 2, BroadcastPropsChangeToAll = true});
        Debug.Log("OnConnectedToMaster");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom " + PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.SetPlayerCustomProperties(new ExitGames.Client.Photon.Hashtable() {
            { "PieceColor", PhotonNetwork.LocalPlayer.ActorNumber - 1 } });
        GameController.Instance.UpdatePlayersList(PhotonNetwork.CurrentRoom.Players);
        GameController.Instance.stateContext.SetState(new GameSetup());
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("player piece color " + targetPlayer.CustomProperties.ContainsKey("PieceColor"));
        Debug.Log("player piece color " + (int)targetPlayer.CustomProperties["PieceColor"]);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        GameController.Instance.UpdatePlayersList(PhotonNetwork.CurrentRoom.Players);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }
}
