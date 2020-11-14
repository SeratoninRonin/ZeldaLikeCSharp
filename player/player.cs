using Godot;
using System;

public class player : entity
{
    [Export]
    string state = "default";
    public int keys = 0;
    public override void _Ready()
    {
        SPEED=70;
        TYPE = "PLAYER";
        MAXHEALTH=16;
        GD.Print("Player ready!");
        base._Ready();
    }
    
    public override void _PhysicsProcess(float delta)
    {
        switch(state)
        {
            case "default":
                state_default();
                break;
            case "swing":
                state_swing();
                break;
            default:
                state_default();
                break;
        }
        keys=Math.Min(keys,9);
        base._PhysicsProcess(delta);
    }

    public void state_default()
    {
        Controls_Loop();
        Movement_Loop();
        SpriteDir_Loop();
        Damage_Loop();
        if(IsOnWall() && movedir!=Vector2.Zero)
        {
            if(spritedir=="left" && TestMove(this.Transform,Vector2.Left))
                AnimSwitch("push");
            if(spritedir=="right" && TestMove(this.Transform,Vector2.Right))
                AnimSwitch("push");
            if(spritedir=="up" && TestMove(this.Transform,Vector2.Up))
                AnimSwitch("push");
            if(spritedir=="down" && TestMove(this.Transform,Vector2.Down))
                AnimSwitch("push");
        }
        else
        {
            if(movedir!=Vector2.Zero)
                AnimSwitch("walk");
            else
                AnimSwitch("idle");
        }
        if(Input.IsActionJustPressed("a"))
        {
            var item = (PackedScene)ResourceLoader.Load("res://items/sword.tscn");
            var inst = (Node2D)item.Instance();
            UseItem(inst);
        }
    }

    public void state_swing()
    {
        AnimSwitch("idle");
        Movement_Loop();
        Damage_Loop();
        movedir=Vector2.Zero;
    }

    public void Controls_Loop()
    {
        var LEFT = Input.IsActionPressed("ui_left");
        var RIGHT = Input.IsActionPressed("ui_right");
        var UP = Input.IsActionPressed("ui_up");
        var DOWN = Input.IsActionPressed("ui_down");

        movedir.x=0;
        movedir.y=0;
        if(LEFT)
            movedir.x-=1;
        if(RIGHT)
            movedir.x+=1;
        if(UP)
            movedir.y-=1;
        if(DOWN)
            movedir.y+=1;
    }

    
}
