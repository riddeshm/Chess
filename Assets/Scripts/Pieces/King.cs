using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override bool CanMove(Tile startTile, Tile endTile)
    {
        if (base.CheckSamePieceColorAtEndTile(endTile))
        {
            return false;
        }

        int x = Mathf.Abs(startTile.xPos - endTile.xPos);
        int y = Mathf.Abs(startTile.yPos - endTile.yPos);
        if (x + y == 1)
        {
            return true;
        }
        else if(x + y == 2 && x == y)
        {
            return true;
        }
        return false;
    }
}
