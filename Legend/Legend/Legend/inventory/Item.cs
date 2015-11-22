using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.inventory
{
    public class Item
    {
        public Texture2D texture;
        public Rectangle Hitbox;
        public ItemType type;
        public string equiptstatus = "not equipped.";
        public string description = "";
        public string name = "";
        public int cost = 0;

        public Item(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width/2, texture.Height/2);
            spriteBatch.Draw(texture, position * Settings.Scale, null, Color.White, 0f, Vector2.Zero, .5f * Settings.Scale, SpriteEffects.None, 0.9f);
        }

        public virtual string getDescription()
        {
            return "";
        }

        public void togglequpited()
        {
            if (equiptstatus == "not equipped.")
            {
                equiptstatus = "equipped.";
            }
            else
            {
                equiptstatus = "not equipped.";
            }
        }

        public virtual Item Copy()
        {
            return new Item(texture) { name = this.name, type = this.type };
        }
    }
}
