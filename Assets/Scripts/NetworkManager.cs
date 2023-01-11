using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LocalPlayer.ActorNumber;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }
}
