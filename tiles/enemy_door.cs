using Godot;
using System;

public class enemy_door : StaticBody2D
{
    camera camera;
    player player;
    AnimationPlayer anim;
    Area2D area;
    public override void _Ready()
    {
        camera=(camera)GetNode("../camera");
        player=(player)GetNode("../player");
        anim = (AnimationPlayer)GetNode("anim");
        area = (Area2D)GetNode("area");
        anim.Play("open");
    }

    public override void _Process(float delta)
    {
        if(camera.grid_pos==camera.get_grid_pos(GlobalPosition))
        {
            if(camera.get_enemies()==0)
            {
                if(anim.AssignedAnimation!="open")
                {
                    anim.Play("open");
                }
            }
            else if(!area.GetOverlappingBodies().Contains(player))
            {
                if(anim.AssignedAnimation!="close")
                {
                    anim.Play("close");
                }
            }
        }
        else
        {
            if(anim.AssignedAnimation!="open")
                {
                    anim.Play("open");
                }
        }
    }
}
