using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiPlayerController : MonoBehaviour
{
    private Board board;
    private PhotonView photonView; 

    private void Start()
    {
        board = GameController.Instance.Board;
        photonView = GetComponent<PhotonView>();
        board.OnTileClicked += OnTileClicked;
    }

    private void OnTileClicked(Tile currentTile)
    {
        Debug.Log("OnTileClicked");
        photonView.RPC(nameof(RPC_TileClicked), RpcTarget.AllBuffered, new object[] { new Vector2(currentTile.xPos, currentTile.yPos) });
    }

    [PunRPC]
    private void RPC_TileClicked(Vector2 coords)
    {
        if(photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            if (GameController.Instance.currentPlayer.index != PhotonNetwork.LocalPlayer.ActorNumber-1)
            {
                Tile currentTile = board.GetTileAt(coords);
                board.CheckTileClicked(currentTile);
            }
        }
    }
}
