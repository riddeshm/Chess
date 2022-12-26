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
    [SerializeField] private GameObject TilePrefab;
    [SerializeField] private GameObject[] piecePrefabs;
    Tile[,] tiles = new Tile[8, 8];
    Tile initialTile;

    private void Start()
    {
        SpawnChessBoard();
        SpawnPieces();
    }

    private void SpawnChessBoard()
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
                tempTile.OnTileClicked += OnTileClicked;
                tiles[i, j] = tempTile;
            }
        }
    }

    private void SpawnPieces()
    {
        for(int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if(j == 1)
                {
                    SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhitePawn], "WhitePawn" + i + "," + "j", i, j);
                }
                if (j == tiles.GetLength(1) - 2)
                {
                    SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackPawn], "BlackPawn" + i + "," + "j", i, j);
                }
            }
        }
        //White pieces
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteRook], "WhiteRook7,0", 7, 0);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteRook], "WhiteRook0,0", 0, 0);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteKnight], "WhiteKnight6,0", 6, 0);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteKnight], "WhiteKnight1,0", 1, 0);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteBishop], "WhiteBishop5,0", 5, 0);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteBishop], "WhiteBishop2,0", 2, 0);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteQueen], "WhiteQueen3,0", 3, 0);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.WhiteKing], "WhiteKing4,0", 4, 0);

        //black pieces
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackRook], "WhiteRook0,7", 0, 7);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackRook], "WhiteRook7,7", 7, 7);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackKnight], "WhiteKnight1,7", 1, 7);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackKnight], "WhiteKnight6,7", 6, 7);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackBishop], "WhiteBishop2,7", 2, 7);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackBishop], "WhiteBishop5,7", 5, 7);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackQueen], "WhiteQueen4,7", 4, 7);
        SpawnPiece(piecePrefabs[(int)PiecePrefabType.BlackKing], "WhiteKing3,7", 3, 7);
    }

    void SpawnPiece(GameObject prefab, string objName, int xPos, int yPos)
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.name = objName;
        obj.transform.position = tiles[xPos, yPos].transform.position;
        tiles[xPos, yPos].piece = obj.GetComponent<Piece>();
    }

    void OnTileClicked(Tile currentTile)
    {
        if(initialTile != null)
        {
            if(initialTile == currentTile)
            {
                currentTile.DeselectTile();
            }
            else
            {
                //Check Move piece
                currentTile.DeselectTile();
                initialTile.DeselectTile();
            }
            initialTile = null;
        }
        else
        {
            initialTile = currentTile;
            currentTile.SelectTile();
        }
    }
}
