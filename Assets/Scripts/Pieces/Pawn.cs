using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public Tile spawnTile;
    public override bool CanMove(Tile startTile, Tile endTile)
    {
        if (endTile.Piece != null)
        {
            if (endTile.Piece.pieceColor == pieceColor)
            {
                return false;
            }
        }

        if(!IsForwardDirection(transform.position, endTile.transform.position))
        {
            return false;
        }
        int x = Mathf.Abs(startTile.xPos - endTile.xPos);
        int y = Mathf.Abs(startTile.yPos - endTile.yPos);
        if (x + y == 1)
        {
            return true;
        }
        else if((y == 2 && x == 0) && startTile.yPos == spawnTile.yPos)
        {
            return true;
        }
        else if (x + y == 2 && x == y)
        {
            if(endTile.Piece != null)
            {
                if(endTile.Piece.pieceColor != pieceColor)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsForwardDirection(Vector3 currentPosition, Vector3 finalPosition)
    {
        Vector3 direction = (finalPosition - currentPosition).normalized;
        float forwardDirectionAngle = Vector3.Angle(direction, transform.forward);
        return forwardDirectionAngle <= 45;
    }
}
