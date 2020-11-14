using Godot;
using System;

public class entity : KinematicBody2D
{
    [Export]
    public float MAXHEALTH = 1;
    Texture texture_default = null;
    Texture texture_hurt=null;
    protected float SPEED = 0;
    public string TYPE = "ENEMY";
     public  Vector2 movedir = Vector2.Zero;
    public string spritedir = "down";

    protected float hitstun =0;
    protected Vector2 knockdir=Vector2.Zero;
    public float health;

    public override void _Ready()
    {
        
        if (TYPE == "ENEMY")
        {
            SetCollisionMaskBit(1,true);
            SetPhysicsProcess(false);
        }
        
        var s = (Sprite)GetNode("Sprite");
        texture_default=s.Texture;
        texture_hurt=(Texture)GD.Load(s.Texture.ResourcePath.Replace(".png","_hurt.png"));
        GD.Print("entity ready!");
        health=MAXHEALTH;
        base._Ready();
    }
    public void Movement_Loop()
    {
        Vector2 motion = Vector2.Zero;
        if(hitstun==0)
            motion = movedir.Normalized() * SPEED;
        else
            motion = knockdir.Normalized() * 125;
        MoveAndSlide(motion, Vector2.Zero);
    }

    public void SpriteDir_Loop()
    {
        if(movedir==Vector2.Left)
            spritedir="left";
        if(movedir==Vector2.Right)
            spritedir = "right";
        if(movedir==Vector2.Down)
            spritedir="down";
        if(movedir==Vector2.Up)
            spritedir="up";
    }

    public void AnimSwitch(string animation)
    {
        var anim = GetNode("anim") as AnimationPlayer;
        var newAnim = animation + spritedir;
        if (anim.CurrentAnimation != newAnim)
        {
            anim.Play(newAnim);
        }
    }

    public void Damage_Loop()
    {
        health = Mathf.Min(MAXHEALTH, health);
        if(hitstun>0)
        {
            hitstun-=1;
            var s = (Sprite)GetNode("Sprite");
            s.Texture=texture_hurt;
        }
        else
        {
            var s = (Sprite)GetNode("Sprite");
            s.Texture=texture_default;
            if(TYPE=="ENEMY" && health <=0)
            {
                /*var death = (PackedScene)GD.Load("res://enemies/enemy_death.tscn");
                var anim = death.Instance() as Node2D;
                anim.GlobalTransform = GlobalTransform;
                GetParent().AddChild(anim);*/
                var drop = GD.Randi() % 3;
                if(drop==0)
                {
                    InstanceScene((PackedScene)GD.Load("res://pickups/heart.tscn"));
                }
                InstanceScene((PackedScene)GD.Load("res://enemies/enemy_death.tscn"));
                QueueFree();
            }
        }
        var hitbox = GetNode("hitbox") as Area2D;
        foreach(Node2D area in hitbox.GetOverlappingAreas())
        {
            var body = area.GetParent() as Node2D;
            if(body!=null)
            {
                if(hitstun==0 && body.Get("DAMAGE") != null && 
                    body.Get("TYPE").ToString()!=TYPE)
                {
                    health -= (float)body.Get("DAMAGE");
                    hitstun=10;
                    knockdir = GlobalTransform.origin - body.GlobalTransform.origin;
                }
            }
        }
    }

    public void UseItem(Node2D item)
    {
        var newItem = item;
        newItem.AddToGroup(newItem.Name+GetInstanceId());
        AddChild(newItem);
        var tree = GetTree();
        var n = tree.GetNodesInGroup(newItem.Name+GetInstanceId());
        if(n.Count>(int)newItem.Get("maxamount"))
        {
            newItem.QueueFree();
        }
    }

    public void InstanceScene(PackedScene scene)
    {
        var new_scene = scene.Instance() as Node2D;
        new_scene.GlobalPosition = GlobalPosition;
        GetParent().AddChild(new_scene);
    }
}
