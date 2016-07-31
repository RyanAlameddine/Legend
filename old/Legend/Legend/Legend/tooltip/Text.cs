using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.tooltip
{
    public class Text : ToolTipObj
    {
        SpriteFont font;
        string text;

        public Text(float scale, Vector2 pos, float layerdepth, SpriteFont font, string text)
            :base(scale, pos, layerdepth, ToolTipObjType.Text)
        {
            this.font = font;
            this.text = text;
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 toolTipPos)
        {
            spriteBatch.DrawString(font, text, (pos + toolTipPos) * Settings.Scale, Color.White, 0f, Vector2.Zero, scale * Settings.Scale, SpriteEffects.None, layerdepth);
            base.Draw(spriteBatch, toolTipPos);
        }
    }
}
