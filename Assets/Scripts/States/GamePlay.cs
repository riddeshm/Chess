using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : State
{
    private Context context;
    public void Begin(Context _context)
    {
        context = _context;
        GameController.Instance.SetCurrentPlayer(0);
        GameController.Instance.MoveCompleted += OnMoveCompleted;
    }

    private void OnMoveCompleted(bool isGameOver)
    {
        if(isGameOver)
        {
            context.SetState(new GameOver());
            return;
        }
        int currentPlayerIndex = GameController.Instance.currentPlayer.index;
        if(currentPlayerIndex < GameController.Instance.players.Length-1)
        {
            currentPlayerIndex++; 
        }
        else
        {
            currentPlayerIndex = 0;
        }
        GameController.Instance.SetCurrentPlayer(currentPlayerIndex);
    }
}
