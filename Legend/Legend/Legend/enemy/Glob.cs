using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Legend.characters;

namespace Legend.enemy
{
    public class Glob : Enemy
    {
        Rectangle[] sources = {
                                new Rectangle(0, 0, 30, 32),
                                new Rectangle(32, 0, 30, 30)
                              };
        public Glob(Texture2D txture, Vector2 position)
            : base(txture, position, new Rectangle(0, 0, 30, 32))
        {
        }

        public override void Update(GameTime gameTime, Player p)
        {
            Hitbox.X = (int)(pos.X - (ori.X/2));
            Hitbox.Y = (int)(pos.Y - (ori.Y/2));
            base.Update(gameTime, p);
        }
    }
}
