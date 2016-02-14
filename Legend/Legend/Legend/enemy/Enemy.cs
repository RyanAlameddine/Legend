using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Legend.characters;

namespace Legend.enemy
{
    public class Enemy
    {
        public Vector2 pos;
        public Vector2 ori;
        protected Rectangle source;
        Rectangle[] sources;
        SpriteEffects effect;
        protected Texture2D txture;
        public Rectangle Hitbox;
        public Vector2 speed;
        Vector2 dir;
        Vector2 speedoffset;
        public Enemy(Texture2D txture, Vector2 pos, Rectangle[] sources)
        {
            this.pos = pos;
            this.txture = txture;
            this.source = sources[0];
            this.sources = sources;
            ori = new Vector2(source.Width / 2, source.Height / 2);
            Hitbox = new Rectangle((int)(pos.X), (int)(pos.Y), (int)(source.Width/2), (int)(source.Height/2));
            speedoffset.X = ((float)(Game1.rand.Next(-4, 0)) / 15) + 1;
            speedoffset.Y = speedoffset.X;
        }

        public virtual void Update(GameTime gameTime, Player p)
        {
            if (p._position.X - 2 > pos.X)
            {
                dir.X = 1;
            }
            else if (p._position.X - 2 < pos.X)
            {
                dir.X = -1;
            }
            if (p._position.Y + 5 > pos.Y)
            {
                dir.Y = 1;
            }
            else if (p._position.Y + 5 < pos.Y)
            {
                dir.Y = -1;
            }
            Vector2 distance = p._position - pos;
            float rotation = (float) Math.Atan2(distance.Y, distance.X);
            rotation -= (float) (90 * Math.PI / 180);
            rotation *= (float) (180 / Math.PI);
            while (rotation > 360)
            {
                rotation -= 360;
            }
            asdfasfdasfdasfasf FIX this;
            if (rotation <= 45 || rotation >= 315)
            {
                source = sources[2];
                effect = SpriteEffects.FlipVertically;
            }
            else if (rotation >= 45 && rotation <= 135)
            {
                source = sources[0];
                effect = SpriteEffects.None;
            }
            else if (rotation >= 135 && rotation <= 225)
            {
                source = sources[2];
                effect = SpriteEffects.None;
            }
            else if (rotation >= 225 && rotation <= 315)
            {
                source = sources[0];
                effect = SpriteEffects.FlipVertically;
            }

            speed = Vector2.Lerp(speed, dir, 0.05f);
            if (p.Hitbox.Intersects(Hitbox) && p.State != PlayerState.Interacting)
            {
                Vector2 temp = new Vector2(p.speedx * p.speed * 2f, p.speedy * p.speed * 2f);
                p.speedx = dir.X * 2f;
                p.speedy = dir.Y * 2f;
                speed = temp;
                p.resetspeed = false;
                p.State = PlayerState.Interacting;
            }
            Hitbox.X = (int)(pos.X - (ori.X / 2));
            Hitbox.Y = (int)(pos.Y - (ori.Y / 2));
            pos += speed * speedoffset;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(txture, pos * Settings.Scale, source, Color.White, 0f, ori, .5f * Settings.Scale, effect, .45f);
        }
    }
}
