using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Action<Tile> OnTileClicked;
    public int xPos;
    public int yPos;

    private Piece piece;
    private Renderer rendr;

    public Piece Piece
    {
        get { return piece; }
        set { piece = value; }
    }

    private void Start()
    {
        rendr = GetComponent<MeshRenderer>();
    }

    private void OnMouseUpAsButton()
    {
        OnTileClicked?.Invoke(this);
    }

    public void SelectTile()
    {
        Color color = rendr.material.color;
        color.a = 0.5f;
        rendr.material.color = color;
    }

    public void DeselectTile()
    {
        Color color = rendr.material.color;
        color.a = 1f;
        rendr.material.color = color;
    }
}
