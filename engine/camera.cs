using Godot;
using System;
using System.Collections.Generic;

public class camera : Camera2D
{
    public Vector2 SCREEN_SIZE = new Vector2(160, 128);
    public float HUD_THICKNESS = 16;
    public Vector2 grid_pos = Vector2.Zero;

    public override void _Ready()
    {
        var area = GetNode("area") as Area2D;
        area.Connect("body_entered", this, "body_entered");
        area.Connect("body_exited", this, "body_exited");
        area.Connect("area_exited", this, "area_exited");
        base._Ready();
    }
    public override void _Process(float delta)
    {
        var player = GetNode("../player") as Node2D;
        var player_grid_pos = get_grid_pos(player.GlobalPosition);
        GlobalPosition = player_grid_pos * SCREEN_SIZE;
        grid_pos = player_grid_pos;
        base._Process(delta);
    }

    public int get_enemies()
    {
        List<Node> enemies = new List<Node>();
        var area = GetNode("area") as Area2D;
        var bodies = area.GetOverlappingBodies();
        foreach(Node body in bodies)
        {
            var t = body.Get("TYPE");
            if(t!=null)
            {
                if(t.ToString()=="ENEMY" && !enemies.Contains(body))
                {
                    enemies.Add(body);
                }
            }
        }
        return enemies.Count;
    }

    public Vector2 get_grid_pos(Vector2 pos)
    {
        pos.y -= HUD_THICKNESS;
        var x = Mathf.Floor(pos.x / SCREEN_SIZE.x);
        var y = Mathf.Floor(pos.y / SCREEN_SIZE.y);
        return new Vector2(x, y);
    }

    public void body_entered(Node body)
    {
        if (body != null)
        {
            var e = body as entity;
            if (e != null)
            {
                var s = e.Get("TYPE") as string;
                if (!string.IsNullOrEmpty(s) && s == "ENEMY")
                {
                    body.SetPhysicsProcess(true);
                }
            }
        }
    }

    public void body_exited(Node body)
    {
        if (body != null)
        {
            var e = body as entity;
            if (e != null)
            {
                var s = e.Get("TYPE") as string;
                if (!string.IsNullOrEmpty(s) && s == "ENEMY")
                {
                    body.SetPhysicsProcess(false);
                }
            }
        }
    }

    public void area_exited(Area area)
    {
        var b = area.Get("disappears") as bool?;
        if (b != null && b == true)
        {
            area.QueueFree();
        }
    }
}
