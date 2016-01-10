using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.enemy
{
    public class Enemy
    {
        public Vector2 pos;
        public Vector2 ori;
        protected Rectangle source;
        protected Texture2D txture;
        public Rectangle Hitbox;
        public Enemy(Texture2D txture, Vector2 pos, Rectangle source)
        {
            this.pos = pos;
            this.txture = txture;
            this.source = source;
            Hitbox = new Rectangle((int)pos.X, (int)pos.Y, txture.Width, txture.Height);
            ori = new Vector2(source.Width / 2, source.Height / 2);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txture, pos * Settings.Scale, source, Color.White, 0f, ori, .5f * Settings.Scale, SpriteEffects.None, .45f);
        }
    }
}
