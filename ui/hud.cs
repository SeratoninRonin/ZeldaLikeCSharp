using Godot;
using System;

public class hud : CanvasLayer
{
    const int HEART_ROW_SIZE = 8;
    const int HEART_OFFSET=8;
    player player;
    Sprite hearts;
    public override void _Ready()
    {
        
        player = (player)GetNode("../player");
        hearts = (Sprite)GetNode("hearts");
        for(int i =0;i<player.MAXHEALTH;i++)
        {
            var new_heart = new Sprite();
            new_heart.Texture=hearts.Texture;
            new_heart.Hframes = hearts.Hframes;
            hearts.AddChild(new_heart);
        }
        base._Ready();
    }

    public override void _Process(float delta)
    {
        var list = hearts.GetChildren();
        foreach(Sprite heart in hearts.GetChildren())
        {
            var index = heart.GetIndex();
            var x = (index % HEART_ROW_SIZE) * HEART_OFFSET;
            var y = (index/HEART_ROW_SIZE) * HEART_OFFSET;
            heart.Position=new Vector2(x,y);

            var last_heart = Mathf.Floor(player.health);
            if(index>last_heart)
            {
                heart.Frame=0;
            }
            if(index==last_heart)
            {
                heart.Frame=(int)((player.health-last_heart)*4);
            }
            if(index<last_heart)
            {
                heart.Frame=4;
            }
        }
        var frame = (int)GetNode("../player").Get("keys");
        var keys = (Sprite)GetNode("keys");
        keys.Frame=frame;
    }
}
