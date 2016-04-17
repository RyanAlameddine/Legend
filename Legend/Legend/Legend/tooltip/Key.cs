using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.tooltip
{
    public class Key : ToolTipObj
    {
        SpriteFont font;
        char key;
        public bool down;
        Texture2D keytxture;
        Texture2D keydown;

        public Key(float scale, Vector2 pos, float layerdepth, SpriteFont font, char key, Texture2D keytxture, Texture2D keydown)
            : base(scale, pos, layerdepth, ToolTipObjType.Key)
        {
            this.keytxture = keytxture;
            this.keydown = keydown;
            this.font = font;
            this.key = key;
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 toolTipPos)
        {
            if (!down)
            {
                spriteBatch.Draw(keytxture, (pos + toolTipPos) * Settings.Scale, null, Color.White, 0f, new Vector2(keytxture.Width/2, keytxture.Height/2), scale * Settings.Scale, SpriteEffects.None, layerdepth);
            }
            else
            {
                spriteBatch.Draw(keydown, (pos + toolTipPos) * Settings.Scale, null, Color.White, 0f, new Vector2(keydown.Width / 2, keydown.Height / 2), scale * Settings.Scale, SpriteEffects.None, layerdepth);
            }
            spriteBatch.DrawString(font, key.ToString(), (pos + toolTipPos) * Settings.Scale, Color.White, 0f, font.MeasureString(key.ToString())/2, scale * Settings.Scale, SpriteEffects.None, layerdepth + 0.001f);
            base.Draw(spriteBatch, toolTipPos);
        }
    }
}
