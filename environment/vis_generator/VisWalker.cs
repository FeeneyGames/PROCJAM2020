using Godot;
using System;

public class VisWalker : Sprite
{
    Vector2 Pos;
    Vector2 CellSize;

    public void Init(Vector2 startPos, Vector2 cellSize)
    {
        Pos = startPos;
        CellSize = cellSize;
        UpdatePos();
    }

    public Vector2 Step(RandomNumberGenerator RNG)
    {
        switch(RNG.RandiRange(0, 3))
        {
            case 0:
                Pos += Vector2.Left;
                break;
            case 1:
                Pos += Vector2.Right;
                break;
            case 2:
                Pos += Vector2.Up;
                break;
            case 3:
                Pos += Vector2.Down;
                break;
        }
        UpdatePos();
        return Pos;
    }

    void UpdatePos()
    {
        this.Position = Pos * CellSize;
    }
}
