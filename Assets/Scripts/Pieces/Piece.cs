using UnityEngine;

public enum PieceColor
{
    White,
    Black
}
public abstract class Piece : MonoBehaviour
{
    public PieceColor pieceColor;
    public abstract bool CanMove(Tile startTile, Tile endTile);

    protected bool CheckSamePieceColorAtEndTile(Tile endTile)
    {
        if (endTile.Piece != null)
        {
            if (endTile.Piece.pieceColor == pieceColor)
            {
                return true;
            }
        } 
        return false;
    }
}
