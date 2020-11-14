using Godot;
using System;

public class dir : Node
{
    

    public static Vector2 rand()
    {
        var d = GD.Randi() % 4 + 1;
        switch(d)
        {
            case 1:
                return Vector2.Left;
            case 2:
                return Vector2.Right;
            case 3:
                return Vector2.Down;
            case 4:
                return Vector2.Up;
        }
        return Vector2.Zero;
    }
}
