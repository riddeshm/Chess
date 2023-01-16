using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject multiPlayerObjectPrefab;
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
        PieceColor color = (PieceColor)index;
        string pieceColorText = "You are : " + color.ToString();
        GameController.Instance.SetCamera(index);
        GameController.Instance.AddPlayers(color, index);
        GameController.Instance.SetPieceText(pieceColorText, index == 0 ? Color.white : Color.black);
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            AddMultiPlayerController();
            GameController.Instance.stateContext.SetState(new GameSetup());
        }
        
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Photon.Pun.PhotonNetwork.Disconnect();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        int index = newPlayer.ActorNumber - 1;
        GameController.Instance.AddPlayers((PieceColor)index, index);
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            AddMultiPlayerController();
            GameController.Instance.stateContext.SetState(new GameSetup());
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        GameController.Instance.SetCurrentPlayer(PhotonNetwork.LocalPlayer.ActorNumber - 1);
        GameController.Instance.stateContext.SetState(new GameOver());
    }

    public void AddMultiPlayerController()
    {
        PhotonNetwork.Instantiate(multiPlayerObjectPrefab.name, Vector3.zero, Quaternion.identity);
    }
}
