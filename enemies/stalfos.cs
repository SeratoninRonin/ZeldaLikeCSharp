using Godot;
using System;


public class stalfos : entity
{
    float movetimer_length=15;
    float movetimer=0;
    public const float DAMAGE = .5f;
    public stalfos()
    {
        SPEED=40;

    }

    public override void _Ready()
    {
        var anim = GetNode("anim") as AnimationPlayer;
        anim.Play("default");
        movedir = dir.rand();
        base._Ready();
    }

    public override void _PhysicsProcess(float delta)
    {
        Movement_Loop();
        Damage_Loop();

        if(movetimer>0)
            movetimer-=1;
        if(movetimer==0 || IsOnWall())
        {
            movedir=dir.rand();
            movetimer=movetimer_length;
        }
    }
}
