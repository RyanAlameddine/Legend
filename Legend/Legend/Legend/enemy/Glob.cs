using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Legend.characters;
using Legend.particles;
using Legend.inventory;

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
        public Glob(Texture2D txture, Vector2 position, Texture2D slimeparticle, Dictionary<inventory.Item, Vector2> itemdrops)
            : base(txture, position, sources, 1, 15, new ParticleSystem(slimeparticle, 0f, 1f, Color.White, new Vector2(-2, 2), new Vector2(-2, 2), new TimeSpan(0, 0, 3), 1.1f, 1.1f, -3f, 2f, Vector2.Zero, new TimeSpan(10000, 0, 0, 0), true, 0.000005f), itemdrops, false)
        {

        }

        public override void Update(GameTime gameTime, Player p)
        {
            base.Update(gameTime, p);
        }
    }
}
