using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override bool CanMove(Tile startTile, Tile endTile)
    {
        if(base.CheckSamePieceColorAtEndTile(endTile))
        {
            return false;
        }

        int x = Mathf.Abs(startTile.xPos - endTile.xPos);
        int y = Mathf.Abs(startTile.yPos - endTile.yPos);
        if (x == y)
        {
            
            return true;
        }
        return false;
    }
}
