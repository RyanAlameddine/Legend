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
        protected Texture2D txture;
        public Rectangle Hitbox;
        Vector2 speed;
        Vector2 dir;
        public Enemy(Texture2D txture, Vector2 pos, Rectangle source)
        {
            this.pos = pos;
            this.txture = txture;
            this.source = source;
            ori = new Vector2(source.Width / 2, source.Height / 2);
            Hitbox = new Rectangle((int)(pos.X), (int)(pos.Y), (int)(source.Width/2), (int)(source.Height/2));
        }

        public virtual void Update(GameTime gameTime, Player p)
        {
            if (p._position.X - 2 > pos.X) dir.X = 1;
            else if (p._position.X - 2 < pos.X) dir.X = -1;
            if (p._position.Y + 5 > pos.Y) dir.Y = 1;
            else if (p._position.Y + 5 < pos.Y) dir.Y = -1;
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
            pos += speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txture, pos * Settings.Scale, source, Color.White, 0f, ori, .5f * Settings.Scale, SpriteEffects.None, .45f);
        }
    }
}
