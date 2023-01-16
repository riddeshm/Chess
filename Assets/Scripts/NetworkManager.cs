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
        int index = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        GameController.Instance.SetCamera(index);
        GameController.Instance.AddPlayers((PieceColor)index, index);
        GameController.Instance.stateContext.SetState(new GameSetup());
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        int index = newPlayer.ActorNumber - 1;
        GameController.Instance.AddPlayers((PieceColor)index, index);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }
}
