using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override bool CanMove(Tile startTile, Tile endTile)
    {
        if (base.CheckSamePieceColorAtEndTile(endTile))
        {
            return false;
        }

        int x = Mathf.Abs(startTile.xPos - endTile.xPos);
        int y = Mathf.Abs(startTile.yPos - endTile.yPos);
        if ((x == 0 && y > 0) || (x > 0 && y == 0))
        {

            return true;
        }
        return false;
    }
}
