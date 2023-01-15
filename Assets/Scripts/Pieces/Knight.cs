using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override bool CanMove(Tile startTile, Tile endTile)
    {
        if (endTile.Piece != null)
        {
            if (endTile.Piece.pieceColor == pieceColor)
            {
                return false;
            }
        }

        int x = Mathf.Abs(startTile.xPos - endTile.xPos);
        int y = Mathf.Abs(startTile.yPos - endTile.yPos);
        if (x * y == 2)
        {
            return true;
        }
        return false;
    }
}
