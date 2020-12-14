using Godot;
using System;

public class VisGenerator : TileMap
{
    // Bounds for level generation
    Vector2 MinTileBound = new Vector2(0, 0);
    Vector2 MaxTileBound = new Vector2(16, 12);
    // Start tile for walkers
    Vector2 StartTile;
    // Number of cells to create
    int MaxCells = 500;
    // Initial number of walkers
    int InitWalkers = 3;
    // RandomNumberGenerator for walkers
    public RandomNumberGenerator RNG = new RandomNumberGenerator();
    // Scene to instance for the walkers
    PackedScene WalkerScene = ResourceLoader.Load<PackedScene>("environment/vis_generator/VisWalker.tscn");
    // Node to add walkers to
    Node WalkerParent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Set the starting tile to middle of bounds
        Vector2 StartTile = new Vector2((MinTileBound + MaxTileBound)/2).Floor();
        // Randomize seed
        RNG.Randomize();
        // Create random walkers
        WalkerParent = GetNode("./WalkerParent");
        for(int i = 0; i < InitWalkers; i++)
        {
            VisWalker walker = (VisWalker) WalkerScene.Instance();
            walker.Init(StartTile, this.CellSize);
            FillCells(StartTile);
            WalkerParent.AddChild(walker);
        }
        // Play animation to run the simulation
        GetNode<AnimationPlayer>("./AnimationPlayer").Play("step");
    }

    public void Step()
    {
        for(int i = 0; i < WalkerParent.GetChildCount(); i++)
        {
            if(this.GetUsedCells().Count < MaxCells)
            {
                Vector2 walkerPos = WalkerParent.GetChild<VisWalker>(i).Step(RNG);
                FillCells(walkerPos);
            }
            else
                break;
        }
    }

    void FillCells(Vector2 floorPos)
    {
        SetCellv(floorPos, 0);
        Vector2 padTiles = new Vector2(1, 1);
        for(float i = floorPos[0] - padTiles[0]; i <= floorPos[0] + padTiles[0]; i++)
            for(float j = floorPos[1] - padTiles[1]; j <= floorPos[1] + padTiles[1]; j++)
            {
                Vector2 curPos = new Vector2(i, j);
                if(this.GetCellv(curPos) == InvalidCell)
                    this.SetCellv(curPos, 1);
            }
    }
}
