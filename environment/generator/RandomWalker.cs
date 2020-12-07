using Godot;
using System;

public class RandomWalker
{
    Vector2 Pos;
    Generator Gen;

    public RandomWalker(Vector2 startPos, Generator parent)
    {
        Pos = startPos;
        Gen = parent;
        Gen.SetCellv(Pos, 0);
    }

    public void Step()
    {
        switch(Gen.RNG.RandiRange(0, 3))
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
        Gen.SetCellv(Pos, 0);
    }
}