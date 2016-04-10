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
                spriteBatch.Draw(keytxture, (pos + toolTipPos) * Settings.Scale, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, layerdepth);
            }
            else
            {
                spriteBatch.Draw(keydown, (pos + toolTipPos) * Settings.Scale, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, layerdepth);
            }
            spriteBatch.DrawString(font, key.ToString(), (pos + toolTipPos + new Vector2(13, 13)) * Settings.Scale, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, layerdepth + 0.001f);
            base.Draw(spriteBatch, toolTipPos);
        }
    }
}
