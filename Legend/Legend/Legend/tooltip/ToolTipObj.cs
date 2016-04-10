using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Legend.tooltip
{
    public class ToolTipObj
    {
        protected float scale;
        protected Vector2 pos;
        protected float layerdepth;
        ToolTipObjType type;

        public ToolTipObj(float scale, Vector2 pos, float layerdepth, ToolTipObjType type)
        {
            this.scale = scale;
            this.pos = pos;
            this.layerdepth = layerdepth;
            this.type = type;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 toolTipPos)
        {
            
        }
    }
}
