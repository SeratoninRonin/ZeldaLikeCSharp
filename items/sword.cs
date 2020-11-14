using Godot;
using System;

public class sword : Node2D
{
    protected string TYPE = null;
    public float DAMAGE = .5f;
    public int maxamount = 1;
    public override void _Ready()
    {
        var entity = GetParent() as entity;
        TYPE = entity.Get("TYPE").ToString();
        var anim = this.GetNode<AnimationPlayer>("anim");
        anim.Connect("animation_finished", this, nameof(_destroy));
        anim.Play("swing" + entity.spritedir);
        if(entity.HasMethod("state_swing"))
        {
            entity.Set("state","swing");
        }
    }
    
    public void _destroy(string animName)
    {
        var entity = GetParent() as entity;
        if(entity.HasMethod("state_swing"))
            entity.Set("state","default");
        this.QueueFree();
    }
}
