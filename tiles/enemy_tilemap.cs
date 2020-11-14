using Godot;
using System;

public class enemy_tilemap : TileMap
{
    public override void _Ready()
    {
        var size = CellSize;
        var offset = size/2;
        foreach(Vector2 tile in GetUsedCells())
        {
            var name = TileSet.TileGetName(GetCell((int)tile.x,(int)tile.y));
            var scene = (PackedScene)GD.Load("res://enemies/"+name+".tscn");
            var inst = scene.Instance() as Node2D;
            inst.GlobalPosition = tile * size + offset;
            GetParent().CallDeferred("add_child",inst);
        }
        QueueFree();
        base._Ready();
    }
}
