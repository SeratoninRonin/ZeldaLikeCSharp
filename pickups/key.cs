using Godot;
using System;

public class key : pickup
{
    public override void body_entered(Node body)
    {
        if(body.Name=="player" && (int)body.Get("keys")<9)
        {
            var p = body as player;
            p.keys +=1;
            QueueFree();
        }
    }
}
