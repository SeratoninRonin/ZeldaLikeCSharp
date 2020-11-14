using Godot;
using System;

public class enemy_death : Node2D
{
    public override void _Ready()
    {
        var anim = (AnimationPlayer)GetNode("anim");
        anim.Play("default");
        anim.Connect("animation_finished",this,"destroy");
        base._Ready();
    }

    public void destroy(string animationName)
    {
        QueueFree();
    }
}
