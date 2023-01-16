public struct Player
{
    public PieceColor selectedColor;
    public int index;

    public Player(PieceColor _selectedColor, int _index)
    {
        selectedColor = _selectedColor;
        index = _index;
    }
}
