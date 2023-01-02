using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PiecePrefabType
{
    WhiteRook = 0,
    BlackRook = 1,
    WhiteKnight = 2,
    BlackKnight = 3,
    WhiteBishop = 4,
    BlackBishop = 5,
    WhiteKing = 6,
    BlackKing = 7,
    WhiteQueen = 8,
    BlackQueen = 9,
    WhitePawn = 10,
    BlackPawn = 11
}

public class Board : MonoBehaviour
{
    public System.Action<Piece> MoveCompleted;
    [SerializeField] private GameObject TilePrefab;
    [SerializeField] private GameObject[] piecePrefabs;
    Tile[,] tiles = new Tile[8, 8];
    Tile initialTile;

    private void Start()
    {
        //SpawnChessBoard();
        //SpawnPieces();
    }

    public void SpawnChessBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject obj = Instantiate(TilePrefab, transform);
                obj.name = "Tile" + i + "," + j;
                obj.transform.position = new Vector3(i, 0, j);
                if (i % 2 != 0)
                {
                    if (j % 2 != 0)
                    {
                        obj.GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }
                else
                {
                    if (j % 2 == 0)
                    {
                        obj.GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }
                Tile tempTile = obj.GetComponent<Tile>();
                tempTile.xPos = i;
                tempTile.yPos = j;
                tempTile.OnTileClicked += OnTileClicked;
                tiles[i, j] = tempTile;
            }
        }
    }

    public void SpawnPieces()
    {
        for(int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if(j == 1)
                {
                    SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhitePawn], "WhitePawn" + i + "," + "j", i, j, PieceColor.White);
                }
                if (j == tiles.GetLength(1) - 2)
                {
                    SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackPawn], "BlackPawn" + i + "," + "j", i, j, PieceColor.Black, 180);
                }
            }
        }
        //White pieces
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteRook], "WhiteRook7,0", 7, 0, PieceColor.White);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteRook], "WhiteRook0,0", 0, 0, PieceColor.White);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteKnight], "WhiteKnight6,0", 6, 0, PieceColor.White);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteKnight], "WhiteKnight1,0", 1, 0, PieceColor.White);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteBishop], "WhiteBishop5,0", 5, 0, PieceColor.White);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteBishop], "WhiteBishop2,0", 2, 0, PieceColor.White);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteQueen], "WhiteQueen3,0", 3, 0, PieceColor.White);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteKing], "WhiteKing4,0", 4, 0, PieceColor.White);

        //black pieces
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackRook], "BlackRook0,7", 0, 7, PieceColor.Black, 180);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackRook], "BlackRook7,7", 7, 7, PieceColor.Black, 180);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackKnight], "BlackKnight1,7", 1, 7, PieceColor.Black, 180);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackKnight], "BlackKnight6,7", 6, 7, PieceColor.Black, 180);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackBishop], "BlackBishop2,7", 2, 7, PieceColor.Black, 180);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackBishop], "BlackBishop5,7", 5, 7, PieceColor.Black, 180);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackQueen], "BlackQueen3,7", 3, 7, PieceColor.Black, 180);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackKing], "BlackKing4,7", 4, 7, PieceColor.Black, 180);
    }

    void SpawnPiece(GameObject prefab, string objName, int xPos, int yPos, PieceColor pieceColor, float rotation = 0f)
    {
        GameObject obj = Instantiate(prefab, transform);
        Piece piece = obj.GetComponent<Piece>();
        obj.name = objName;
        obj.transform.Rotate(0, rotation, 0);
        obj.transform.position = tiles[xPos, yPos].transform.position;
        piece.pieceColor = pieceColor;
        try
        {
            Pawn pawn = (Pawn)piece;
            pawn.spawnTile = tiles[xPos, yPos];
        }
        catch(System.Exception e)
        {
            
        }
        tiles[xPos, yPos].piece = piece;
    }

    void OnTileClicked(Tile currentTile)
    {
        if (initialTile != null)
        {
            if(initialTile == currentTile)
            {
                currentTile.DeselectTile();
            }
            else
            {
                //Check Move piece
                if(initialTile.piece.CanMove(initialTile, currentTile) && !IsPathBlocked(initialTile, currentTile))
                {
                    Piece capturedPiece = null;
                    if(currentTile.piece != null)
                    {
                        capturedPiece = currentTile.piece;
                        currentTile.piece.gameObject.SetActive(false);
                        currentTile.piece = null;
                    }
                    currentTile.piece = initialTile.piece;
                    initialTile.piece = null;
                    currentTile.piece.transform.position = currentTile.transform.position;
                    MoveCompleted?.Invoke(capturedPiece);
                }
                currentTile.DeselectTile();
                initialTile.DeselectTile();
            }
            initialTile = null;
        }
        else if(initialTile == null && currentTile.piece != null)
        {
            if(GameController.Instance.currentPlayer.selectedColor == currentTile.piece.pieceColor)
            {
                initialTile = currentTile;
                currentTile.SelectTile();
            }
        }
    }

    private bool IsPathBlocked(Tile startTile, Tile endTile)
    {
        int x = Mathf.Abs(startTile.xPos - endTile.xPos);
        int y = Mathf.Abs(startTile.yPos - endTile.yPos);
        if (x == y)
        {
            int dirX = endTile.xPos > startTile.xPos ? 1 : -1;
            int dirY = endTile.yPos > startTile.yPos ? 1 : -1;
            for (int i = 1; i < Mathf.Abs(endTile.xPos - startTile.xPos); ++i)
            {
                if (tiles[startTile.xPos + i * dirX, startTile.yPos + i * dirY].piece != null)
                {
                    return true;
                }
            }
        }
        else if((x == 0 && y > 0) || (x > 0 && y == 0))
        {
            int dirX = endTile.xPos > startTile.xPos ? 1 : -1;
            int dirY = endTile.yPos > startTile.yPos ? 1 : -1;

            if(x > y)
            {
                for (int i = 1; i < Mathf.Abs(endTile.xPos - startTile.xPos); ++i)
                {
                    if (tiles[startTile.xPos + i * dirX, startTile.yPos].piece != null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 1; i < Mathf.Abs(endTile.yPos - startTile.yPos); ++i)
                {
                    if (tiles[startTile.xPos, startTile.yPos + i * dirY].piece != null)
                    {
                        return true;
                    }
                }
            }
            
        }
        return false;
    }
}
