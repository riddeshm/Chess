using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : State
{
    public void Begin(Context context)
    {
        GameController.Instance.AddPlayers();
        GameController.Instance.SetupBoard();
        context.SetState(new GamePlay());
    }
}
