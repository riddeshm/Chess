using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : State
{
    private Context context;
    public void Begin(Context _context)
    {
        context = _context;
        GameController.Instance.ShowPopup("Press Start To Play", "START", true, OnStartClicked);
    }

    private void OnStartClicked()
    {
        GameController.Instance.HidePopup();
        GameController.Instance.ConnectToNetwork();
        GameController.Instance.ShowPopup("Connecting...", "", false, null);
        context.SetState(new GameSetup());
    }
}
