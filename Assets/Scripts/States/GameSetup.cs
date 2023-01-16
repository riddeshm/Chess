using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : GameState
{
    public void Begin(Context context)
    {
        GameController.Instance.SetupBoard();
        context.SetState(new GamePlay());
    }
}
