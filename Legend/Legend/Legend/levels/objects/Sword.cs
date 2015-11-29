using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Legend.characters;

namespace Legend.levels.objects
{
    public class Sword
    {
        Texture2D txture;
        Vector2 position;
        float rotation;
        float endrotation;
        Rectangle Hitbox;
        Vector2 hilt;
        bool swinging = false;
        Player p;
        public Sword(Texture2D txture, Player p, Vector2 hilt)
        {
            this.hilt = hilt;
            this.txture = txture;
            Hitbox.Width = txture.Width;
            Hitbox.Height = txture.Height;
            this.p = p;
        }

        public void Update()
        {
            if (swinging)
            {
                if (rotation > endrotation)
                {
                    rotation -= 0.4f;
                }
                else
                {
                    p.State = PlayerState.Idle;
                    swinging = false;
                }
            }
        }

        public void swing(){
            position = p._position;
            swinging = true;
            p.State = PlayerState.Attacking;
            if (p.dir == Direction.Up)
            {
                rotation = 0;
            }
            else if (p.dir == Direction.Down)
            {
                rotation = (float)Math.PI;
            }
            else if (p.dir == Direction.Left)
            {
                rotation = 3f * (float)Math.PI / 2f;
            }
            else if (p.dir == Direction.Right)
            {
                rotation = (float)Math.PI / 2f;
            }
            endrotation = (float)(rotation - Math.PI / 4f);
            rotation += (float)Math.PI / 4f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txture, position * Settings.Scale, null, Color.White, rotation, hilt, .6f * Settings.Scale, SpriteEffects.None, 0.6f);
        }
    }
}
