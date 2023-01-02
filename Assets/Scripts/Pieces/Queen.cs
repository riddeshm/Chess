using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override bool CanMove(Tile startTile, Tile endTile)
    {
        if (endTile.piece != null)
        {
            if (endTile.piece.pieceColor == pieceColor)
            {
                return false;
            }
        }

        int x = Mathf.Abs(startTile.xPos - endTile.xPos);
        int y = Mathf.Abs(startTile.yPos - endTile.yPos);
        if ((x == y) || //diagonal
            ((x == 0 && y > 0) || (x > 0 && y == 0))) // horizontal
        {

            return true;
        }
        return false;
    }
}
