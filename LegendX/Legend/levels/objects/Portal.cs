using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Legend.characters;

namespace Legend.levels.objects
{
    public class Portal
    {
        Texture2D _portaltxt;
        public PortalState state = PortalState.Bigger;
        float size = 0.01f;
        float rotation = 0f;
        Rectangle hitbox;
        Vector2 _position;
        public Vector2 Position
        {
            get
            {
                return _position;
            }
        }
        public Rectangle Hitbox
        {
            get
            {
                return hitbox;
            }
        }
        public Color color = Color.White;
        public bool hidden = true;
        
        public Portal(Texture2D portaltxt, Vector2 position)
        {
            _portaltxt = portaltxt;
            _position = position;
            hitbox = new Rectangle((int)position.X, (int)position.Y, _portaltxt.Width, _portaltxt.Height);
        }

        public void Update()
        {
            if (!hidden)
            {
                rotation -= 0.2f;
                if (state == PortalState.Smaller)
                {
                    if (size > 0.007)
                    {
                        size -= 0.003f;
                    }
                    else
                    {
                        state = PortalState.Bigger;
                    }
                }
                if (state == PortalState.Bigger)
                {
                    if (size < 0.7)
                    {
                        size += 0.007f;
                        hitbox = new Rectangle((int)(_position.X - _portaltxt.Width / 2f * size), (int)(_position.Y - _portaltxt.Height / 2f * size), (int)(_portaltxt.Width * size), (int)(_portaltxt.Height * size));
                    }
                    else
                    {
                        state = PortalState.Spinning;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!hidden)
            {
                spriteBatch.Draw(_portaltxt, _position * Settings.Scale, null, color, rotation, new Vector2(_portaltxt.Width / 2, _portaltxt.Height / 2), size * Settings.Scale, SpriteEffects.None, 0.4f);
            }
        }
    }
}
