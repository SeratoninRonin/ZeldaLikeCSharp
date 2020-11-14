using Godot;
using System;

public class pickup : Area2D
{
    [Export]
    bool disappears = false;
    public override void _Ready()
    {
        Connect("body_entered",this,"body_entered");
        Connect("area_entered",this,"area_entered");
        base._Ready();
    }

    public virtual void area_entered(Area area)
    {
        var parent = area.GetParent();
        if(parent.Name=="sword")
        {
            body_entered(parent.GetParent());
        }
    }

    public virtual void body_entered(Node body)
    {
        
    }
}
