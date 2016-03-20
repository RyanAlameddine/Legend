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
        public float health = 10;
        Texture2D hitparticle;
        Texture2D pixels;
        Vector2 position;
        List<int> noshow = new List<int>();
        public HealthManager(Texture2D hitparticle, Texture2D pixels)
        {
            this.hitparticle = hitparticle;
            this.pixels = pixels;
            position = new Vector2(10, 10);
            noshow.Add(2);
        }

        public void Update()
        {
            position = Game1.levellist[Game1.level - 1].player._position;
            position.Y -= 7;
            position.X -= 2;
            if (health <= 0)
            {
                Game1.rendColor = Color.Lerp(Game1.rendColor, Color.Black, .009f);
                Game1.deathspeed = MathHelper.Lerp(Game1.deathspeed, 0f, .02f);
                if (Game1.rendColor.R < 2 && Game1.rendColor.G < 2 && Game1.rendColor.B < 2)
                {
                    Game1.screen = Screens.GameOver;
                    Game1.ttle.Reset(false);
                    Game1.deathspeed = 1;
                    Reset();
                }
            }
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
                spriteBatch.Draw(pixels, position * Settings.Scale, null, Color.Red, 0f, Vector2.Zero, new Vector2(4, 1) * Settings.Scale, SpriteEffects.None, .55f);
                spriteBatch.Draw(pixels, position * Settings.Scale, null, Color.Lime, 0f, Vector2.Zero, new Vector2(.4f*health, 1) * Settings.Scale, SpriteEffects.None, .56f);
            }
        }

        void Reset()
        {
            health = 10;
        }
    }
}
