using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Legend
{
    public class HealthManager
    {
        public float health = 3;
        Texture2D heart;
        Vector2 position;
        List<int> noshow = new List<int>();
        public HealthManager(Texture2D heart)
        {
            this.heart = heart;
            position = new Vector2(10, 10);
            noshow.Add(2);
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bool show = true;
            if (Game1.screen != Screens.Level)
            {
                show = false;
            }
            foreach (int i in noshow)
            {
                if (i == Game1.level)
                {
                    show = false;
                }
            }
            if (show)
            {
                for (int i = 0; i < health; i++)
                {
                    Vector2 temp = position;
                    temp.X += i * 30;
                    spriteBatch.Draw(heart, temp, null, Color.White, 0f, Vector2.Zero, .03f, SpriteEffects.None, 0.8f);
                }
            }
        }
    }
}
