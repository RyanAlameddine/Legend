using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.enemy
{
    public class Glob
    {
        public Vector2 pos;
        public Vector2 ori;
        Texture2D txture;
        public Rectangle Hitbox;
        public Glob(Texture2D txture, Vector2 pos)
        {
            this.pos = pos;
            this.txture = txture;
            Hitbox = new Rectangle((int)pos.X, (int)pos.Y, txture.Width, txture.Height);
            ori = new Vector2(txture.Width / 2, txture.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txture, pos * Settings.Scale, null, Color.Black, 0f, ori, 1f * Settings.Scale, SpriteEffects.None, .45f);
        }
    }
}
