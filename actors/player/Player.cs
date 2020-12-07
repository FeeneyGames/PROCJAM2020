using Godot;
using System;

public class Player : KinematicBody2D
{
    const float SPEED = 450;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 dir = Vector2.Zero;
        if(Godot.Input.IsActionPressed("ui_left"))
            dir += Vector2.Left;
        if(Godot.Input.IsActionPressed("ui_right"))
            dir += Vector2.Right;
        if(Godot.Input.IsActionPressed("ui_up"))
            dir += Vector2.Up;
        if(Godot.Input.IsActionPressed("ui_down"))
            dir += Vector2.Down;
        if(dir != Vector2.Zero)
            dir = dir.Normalized();
        MoveAndSlide(dir * SPEED);
    }
}
