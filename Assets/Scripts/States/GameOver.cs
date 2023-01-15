using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : State
{
    Context context;
    public void Begin(Context _context)
    {
        context = _context;
        //string popupText = GameController.Instance.currentPlayer.selectedColor.ToString();
        int colorIndex = (int)GameController.Instance.currentPlayer1.CustomProperties["PieceColor"];
        PieceColor color = (PieceColor)colorIndex;
        string popupText = color.ToString();
        GameController.Instance.ShowPopup(popupText + " Wins!", "EXIT", true, OnExitClicked);
    }

    private void OnExitClicked()
    {
        //GameController.Instance.HidePopup();
        context.ClearCurrentState();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
