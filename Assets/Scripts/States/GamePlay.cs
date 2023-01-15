using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : State
{
    private Context context;
    public void Begin(Context _context)
    {
        context = _context;
        GameController.Instance.SetCurrentPlayer(GameController.Instance.players1.FindIndex(x => x.ActorNumber-1 == 0));
        GameController.Instance.MoveCompleted += OnMoveCompleted;
    }

    private void OnMoveCompleted(bool isGameOver)
    {
        Debug.Log("OnMoveCompleted");
        if(isGameOver)
        {
            context.SetState(new GameOver());
            return;
        }
        //int currentPlayerIndex = GameController.Instance.currentPlayer.index;
        int currentPlayerIndex = GameController.Instance.players1.IndexOf(GameController.Instance.currentPlayer1);
        if (currentPlayerIndex < GameController.Instance.players1.Count-1)
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
