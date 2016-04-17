using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Legend.tooltip
{
    public class KeyAnimation
    {
        List<Key> keys;
        TimeSpan timer;
        bool wasdown = false;
        public KeyAnimation(List<Key> keys)
        {
            this.keys = keys;
            timer = new TimeSpan(0, 0, 1);
        }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime;
            if (timer <= TimeSpan.Zero)
            {
                foreach (Key key in keys)
                {
                    if (!key.down && !wasdown)
                    {
                        key.down = true;
                    }
                    if (key.down)
                    {
                        key.down = false;
                        wasdown = true;
                    }
                }
            }
        }
    }
}
