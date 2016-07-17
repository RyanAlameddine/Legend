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
            position = GameApplication.levellist[GameApplication.level - 1].player._position;
            position.Y -= 7;
            position.X -= 2;
            if (health <= 0)
            {
                GameApplication.rendColor = Color.Lerp(GameApplication.rendColor, Color.Black, .009f);
                GameApplication.deathspeed = MathHelper.Lerp(GameApplication.deathspeed, 0f, .02f);
                if (GameApplication.rendColor.R < 2 && GameApplication.rendColor.G < 2 && GameApplication.rendColor.B < 2 || GameApplication.screen == Screens.Home)
                {
                    GameApplication.screen = Screens.GameOver;
                    GameApplication.ttle.Reset(false);
                    GameApplication.deathspeed = 1;
                    Reset();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bool show = true;
            if (GameApplication.screen != Screens.Level)
            {
                show = false;
            }
            foreach (int i in noshow)
            {
                if (i == GameApplication.level)
                {
                    show = false;
                }
            }
            if (show)
            {
                float scalemultiplier = (float) (GameApplication.levellist[GameApplication.level - 1].player.scale * 5.5);
                spriteBatch.Draw(pixels, position * Settings.Scale, null, Color.Red, 0f, Vector2.Zero, new Vector2(4, 1) * scalemultiplier * Settings.Scale, SpriteEffects.None, .55f);
                spriteBatch.Draw(pixels, position * Settings.Scale, null, Color.Lime, 0f, Vector2.Zero, new Vector2(.4f * health, 1) * scalemultiplier * Settings.Scale, SpriteEffects.None, .56f);
            }
        }

        void Reset()
        {
            health = 10;
        }
    }
}
