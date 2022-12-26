using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Action<Tile> OnTileClicked;
    public Piece piece;
    Renderer rendr;

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
