using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : GameState
{
    Context context;
    public void Begin(Context _context)
    {
        context = _context;
        string popupText = GameController.Instance.currentPlayer.selectedColor.ToString();
        GameController.Instance.ShowPopup(popupText + " Wins!", "EXIT", true, OnExitClicked);
    }

    private void OnExitClicked()
    {
        Photon.Pun.PhotonNetwork.LeaveRoom();
        //GameController.Instance.HidePopup();
        context.ClearCurrentState();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
