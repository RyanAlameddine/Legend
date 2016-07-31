using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Legend.functions
{
    public class Tile
    {
        public Texture2D texture;
        public Color color;
        public Rectangle Hitbox;

        public Tile(Texture2D texture, Color color)
        {
            this.texture = texture;
            this.color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(Hitbox.X, Hitbox.Y) * Settings.Scale, null,color, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.1f);
        }
    }
}
