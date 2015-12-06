using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Legend.characters;
using Legend.inventory;

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
        float layerDepth = .4f;
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
                    rotation -= 0.25f;
                }
                else
                {
                    p.State = PlayerState.Idle;
                    swinging = false;
                }

            }
        }

        public void swing()
        {
            if (txture != GameContent.selectedinventory)
            {
                swinging = true;
                p.State = PlayerState.Attacking;
                if (p.dir == Direction.Up)
                {
                    rotation = 0;
                    position.X = p._position.X + 9;
                    position.Y = p._position.Y + 3;
                    layerDepth = .4f;
                }
                else if (p.dir == Direction.Down)
                {
                    rotation = (float)Math.PI;
                    position.X = p._position.X + 5;
                    position.Y = p._position.Y + 10;
                    layerDepth = .6f;
                }
                else if (p.dir == Direction.Left)
                {
                    rotation = 3f * (float)Math.PI / 2f;
                    position.X = p._position.X;
                    position.Y = p._position.Y + 11;
                    layerDepth = .4f;
                }
                else if (p.dir == Direction.Right)
                {
                    rotation = (float)Math.PI / 2f;
                    position.X = p._position.X + 9;
                    position.Y = p._position.Y + 10;
                    layerDepth = .4f;
                }
                endrotation = (float)(rotation - Math.PI / 6f);
                rotation += (float)Math.PI / 2f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (swinging)
            {
                spriteBatch.Draw(txture, position * Settings.Scale, null, Color.White, rotation, hilt, .6f * Settings.Scale, SpriteEffects.None, layerDepth);
            }
        }
    }
}
