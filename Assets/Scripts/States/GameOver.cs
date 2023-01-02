using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : State
{
    Context context;
    public void Begin(Context _context)
    {
        string popupText = GameController.Instance.currentPlayer.selectedColor.ToString();
        context = _context;
        GameController.Instance.ShowPopup(popupText + " Wins!", "EXIT", true, OnExitClicked);
    }

    private void OnExitClicked()
    {
        context.ClearCurrentState();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
