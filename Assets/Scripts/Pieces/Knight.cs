using UnityEngine;

public class Knight : Piece
{
    public override bool CanMove(Tile startTile, Tile endTile)
    {
        if (base.CheckSamePieceColorAtEndTile(endTile))
        {
            return false;
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
