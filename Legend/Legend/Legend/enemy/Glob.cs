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
        static Rectangle[] sources = {
                                new Rectangle(0, 0, 30, 32),
                                new Rectangle(32, 0, 30, 30),
                                new Rectangle(0, 33, 32, 30),
                                new Rectangle(32, 33, 30, 28)
                              };
        public Glob(Texture2D txture, Vector2 position)
            : base(txture, position, sources)
        {

        }

        public override void Update(GameTime gameTime, Player p)
        {
            base.Update(gameTime, p);
        }
    }
}
