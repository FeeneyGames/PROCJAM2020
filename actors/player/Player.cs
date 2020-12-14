using Godot;
using System;

public class Player : KinematicBody2D
{
    const float SPEED = 600;
    const float RETICLE_MIN = 180;
    const float RETICLE_MAX = 600;
    Node2D Reticle;
    Vector2 PrevReticleDir = Vector2.Right;
    Camera2D Camera;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Reticle = GetNode<Node2D>("./Reticle");
        Camera = GetNode<Camera2D>("./Camera2D");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        HandleRestart();
        MovePlayer();
        MoveReticle();
        MoveCamera();
    }

    void HandleRestart()
    {
        if(Godot.Input.IsActionJustPressed("restart"))
            GetTree().ReloadCurrentScene();
    }

    void MovePlayer()
    {
        Vector2 dir = Vector2.Zero;
        dir.x -= Godot.Input.GetActionStrength("ui_left");
        dir.x += Godot.Input.GetActionStrength("ui_right");
        dir.y -= Godot.Input.GetActionStrength("ui_up");
        dir.y += Godot.Input.GetActionStrength("ui_down");
        if(dir.Length() > 1)
            dir = dir.Normalized();
        MoveAndSlide(dir * SPEED);
    }

    void MoveReticle()
    {
        Vector2 dir = Vector2.Zero;
        dir.x -= Godot.Input.GetActionStrength("aim_left");
        dir.x += Godot.Input.GetActionStrength("aim_right");
        dir.y -= Godot.Input.GetActionStrength("aim_up");
        dir.y += Godot.Input.GetActionStrength("aim_down");
        if(dir.Length() > 1)
            dir = dir.Normalized();
        Vector2 pos = dir * RETICLE_MAX;
        float lerp_speed = 15;
        if(pos.Length() >= RETICLE_MIN)
        {
            PrevReticleDir = dir.Normalized();
        }
        else
        {
            lerp_speed = 5;
            pos = PrevReticleDir * RETICLE_MIN;
        }
        Reticle.Position = Reticle.Position.LinearInterpolate(pos, lerp_speed * GetProcessDeltaTime());
    }

    void MoveCamera()
    {
        Camera.Position = Reticle.Position/2;
    }
}
