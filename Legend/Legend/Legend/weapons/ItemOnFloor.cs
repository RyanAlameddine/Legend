using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Legend.inventory;

namespace Legend.weapons
{
    public class ItemOnFloor : Sprite
    {
        public Item item;
        TimeSpan ts = new TimeSpan();
        public ItemOnGroundState State = ItemOnGroundState.OnGround;
        public ItemOnFloor(Item item, Vector2 position, float scale)
            : base(item.texture, position, null, 0, new Vector2(item.texture.Width / 2, item.texture.Height / 2), scale, SpriteEffects.None, 0, Color.White, item.texture.Width / 2, item.texture.Height / 2)
        {
            this.item = item;
            _layerDepth = 0.4f;
        }

        public override void Update(GameTime gameTime)
        {
            if (State == ItemOnGroundState.GettingPickedUp)
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
                        State = ItemOnGroundState.DoneAnimating;
                        Game1.inventory.AddItem(item);
                    }
                }
            }
            base.Update(gameTime);
        }

       
    }
}
