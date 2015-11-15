using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Legend.weapons
{
    public class WeaponOnFloor : Sprite
    {
        TimeSpan ts = new TimeSpan();
        public WeaponOnGroundState State = WeaponOnGroundState.OnGround;
        public WeaponOnFloor(Texture2D sword, Vector2 position, float scale)
            :base(sword, position, null, 0, new Vector2(sword.Width/2, sword.Height/2), scale, SpriteEffects.None, 0, Color.White, sword.Width/2, sword.Height/2)
        {
            _layerDepth = 0.4f;
        }

        public override void Update(GameTime gameTime)
        {
            if (State == WeaponOnGroundState.GettingPickedUp)
            {
                _layerDepth = 0.6f;
                if (_rotation < 12.5)
                {
                    Scale += .01f;
                    _rotation += .08f;
                }
                else
                {
                    ts += gameTime.ElapsedGameTime;
                    if (ts.Seconds >= 1)
                    {
                        State = WeaponOnGroundState.DoneAnimating;
                    }
                }
            }
            base.Update(gameTime);
        }

       
    }
}
