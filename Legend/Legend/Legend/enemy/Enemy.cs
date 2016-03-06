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
        float rotation = 0f;
        public Direction direction = Direction.Up;
        public int damage;
        public Enemy(Texture2D txture, Vector2 pos, Rectangle[] sources, int damage)
        {
            this.pos = pos;
            this.txture = txture;
            this.source = sources[0];
            this.sources = sources;
            this.damage = damage;
            ori = new Vector2(source.Width / 2, source.Height / 2);
            Hitbox = new Rectangle((int)(pos.X), (int)(pos.Y), (int)(source.Width/2), (int)(source.Height/2));
            speedoffset.X = -6/15 + 1;
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
            rotation = (float) Math.Atan2(distance.Y, distance.X);
            rotation *= (float) (180 / Math.PI);
            while (rotation > 360)
            {
                rotation -= 360;
            }
            if (rotation <= 45 && rotation >= -45)
            {
                direction = Direction.Right;
                source = sources[0];
                effect = SpriteEffects.None;
            }
            else if (rotation >= 45 && rotation <= 135)
            {
                direction = Direction.Up;
                source = sources[2];
                effect = SpriteEffects.FlipVertically;
            }
            else if (rotation >= 135 || rotation <= -135)
            {
                direction = Direction.Left;
                source = sources[0];
                effect = SpriteEffects.FlipHorizontally;
            }
            else if (rotation >= -135 && rotation <= -45)
            {
                direction = Direction.Down;
                source = sources[2];
                effect = SpriteEffects.None;
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
                Game1.healthManager.health -= damage / Game1.inventory.armour.defence;
            }
            Hitbox.X = (int)((pos.X - (ori.X / 2)));
            Hitbox.Y = (int)((pos.Y - (ori.Y / 2)));
            pos += speed * speedoffset * Game1.deathspeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txture, pos * Settings.Scale, source, Color.White, 0f, ori, .5f * Settings.Scale, effect, .45f);
        }
    }
}
