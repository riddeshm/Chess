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
        Debug.Log("RPC_TileClicked ");
        if(photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("LocalPlayer ");
            Debug.Log("GameController.Instance.currentPlayer1  " + GameController.Instance.currentPlayer1.CustomProperties["PieceColor"]);
            Debug.Log("PhotonNetwork.LocalPlayer  " + PhotonNetwork.LocalPlayer.CustomProperties["PieceColor"]);
            if (GameController.Instance.currentPlayer1 != PhotonNetwork.LocalPlayer)
            {
                Debug.Log("!currentPlayer1 ");
                Tile currentTile = board.GetTileAt(coords);
                board.CheckTileClicked(currentTile);
            }
        }
    }
}
