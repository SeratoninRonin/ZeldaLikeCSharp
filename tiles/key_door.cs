using Godot;
using System;

public class key_door : StaticBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var area = GetNode("area") as Area2D;
        area.Connect("body_entered",this,"body_entered");
        base._Ready();
    }

    public void body_entered(Node body)
    {
        if(body.Name=="player" && (int)body.Get("keys")>0)
        {
            var p = (player)body;
            p.keys-=1;
            QueueFree();
        }
    }
}
