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
        //local MultiPlayerController instance
        if(photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            //Current player is not local player
            if (GameController.Instance.currentPlayer.index != PhotonNetwork.LocalPlayer.ActorNumber-1)
            {
                Tile currentTile = board.GetTileAt(coords);
                board.CheckTileClicked(currentTile);
            }
        }
    }
}
