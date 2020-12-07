using Godot;
using System;

public class Generator : TileMap
{
    // Bounds for level generation
    Vector2 MinTileBound = new Vector2(0, 0);
    Vector2 MaxTileBound = new Vector2(16, 12);
    // Number of cells to create
    int MaxCells = 110;
    // Max number of steps to ensure no infinite loops
    int MaxSteps = 1000;
    // Initial number of RandomWalker
    int InitWalkers = 1;
    // RandomNumberGenerator for RandomWalkers
    public RandomNumberGenerator RNG = new RandomNumberGenerator();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Randomize seed
        RNG.Randomize();
        // Set the starting tile to middle of bounds
        Vector2 startTile = new Vector2((MinTileBound + MaxTileBound)/2);
        startTile = startTile.Floor();
        // Create random walkers
        RandomWalker randomWalker = new RandomWalker(startTile, this);
        // Run simulation
        int numSteps = 0;
        while(this.GetUsedCells().Count < MaxCells && numSteps < MaxSteps)
        {
            randomWalker.Step();
            numSteps++;
        }
        // Fill in unused areas with walls
        Vector2 padTiles = (Godot.OS.WindowSize / this.CellSize).Ceil();
        Rect2 floorRect = this.GetUsedRect();
        for(float i = floorRect.Position[0] - padTiles[0]; i < floorRect.End[0] + padTiles[0]; i++)
            for(float j = floorRect.Position[1] - padTiles[1]; j < floorRect.End[1] + padTiles[1]; j++)
            {
                Vector2 curPos = new Vector2(i, j);
                if(this.GetCellv(curPos) == InvalidCell)
                    this.SetCellv(curPos, 1);
            }
    }
}
