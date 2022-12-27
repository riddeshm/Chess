using System.Collections;
using System.Collections.Generic;
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
}
