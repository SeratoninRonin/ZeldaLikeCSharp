using Godot;
using System;

public class heart : pickup
{
    public override void body_entered(Node body)
    {
        if(body.Name=="player")
        {
            var p = body as player;
            p.health+=1;
            QueueFree();
        }
    }
    public override void _Ready()
    {
        base._Ready();
    }
}
