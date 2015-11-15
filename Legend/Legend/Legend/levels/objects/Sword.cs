using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Legend.levels.objects
{
    public class Sword
    {
        Texture2D txture;
        Vector2 position;
        float rotation;
        Rectangle Hitbox;
        Vector2 hilt;
        bool swinging = false;
        public Sword(Texture2D txture, Vector2 hilt)
        {
            this.txture = txture;
            this.hilt = hilt;
            Hitbox.Width = txture.Width;
            Hitbox.Height = txture.Height;
        }

        public void Update()
        {

        }

        public void swing(Vector2 position, float rotation){
            this.position = position;
            this.rotation = rotation;
            this.swinging = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txture, position, null, Color.White, rotation, hilt, 1f, SpriteEffects.None, 0.6f);
        }
    }
}
