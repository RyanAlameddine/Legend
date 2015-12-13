using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Legend.characters;
using Microsoft.Xna.Framework;
using Legend.levels.functions;
using Legend.weapons;
using Legend.levels.objects;
using Legend.inventory;
using Microsoft.Xna.Framework.Media;
using Legend.particles;

namespace Legend.levels.sublevels
{
    public class L1 : Level
    {
        Texture2D _grass;
        Texture2D _grassbarrier;
        Texture2D _foamsword;
        ItemOnFloor sword;
        List<Sprite> grassBarriers = new List<Sprite>();
        ParticleSystem particleSystem;

        public L1(Texture2D playermove, Texture2D playerattack, Texture2D grass, Texture2D grassbarrier, Texture2D foamsword, Texture2D portal, Song song)
            :base(playermove, portal, song)
        {
            _grassbarrier = grassbarrier;
            _grass = grass;
            _foamsword = foamsword;
            sword = new ItemOnFloor(Items.GetItem("Foam Sword"), new Vector2(150, 30), 0.5f);
            particleSystem = new ParticleSystem(sword.item.texture, 0f, 1f, Color.White, new Vector2(-1, 1), new Vector2(-1, 1), new TimeSpan(0, 0, 3), 1f, 2f, 1f, 1f, new Vector2(150, 30), new TimeSpan(1000, 0, 1, 0, 0), true);
            int dy = 50;
            float layerDepth = .2f;
            for (int i = 0; i < 8; i++)
            {
                grassBarriers.Add(new Sprite(_grassbarrier, new Vector2(75, dy), null, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, layerDepth, Color.White, _grassbarrier.Width/2, _grassbarrier.Height/2));
                dy += 25;
                layerDepth += .001f;
            }

            dy = 50;
            layerDepth = .2f;
            for (int i = 0; i < 8; i++)
            {
                grassBarriers.Add(new Sprite(_grassbarrier, new Vector2(200, dy), null, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, layerDepth, Color.White, _grassbarrier.Width/2, _grassbarrier.Height/2));
                dy += 25;
                layerDepth += .001f;
            }

            background = new Background(_grass);
            player = new Player(playermove, playerattack, new Vector2(150, 300));
            portalobj = new Portal(portal, new Vector2(155, 250));
            spinning = false;
        }

        public override void Update(KeyboardState ks, MouseState ms, GameTime gameTime)
        {
            player.Update(ks, grassBarriers, ms, gameTime);
            sword.Update(gameTime);
            portalobj.Update();
            Portal(gameTime, Color.OrangeRed);
            if (player.DidPickUpWeapon(ks, sword) && sword.State != ItemOnGroundState.GettingPickedUp)
            {
                player.State = PlayerState.Interacting;
                sword.State = ItemOnGroundState.GettingPickedUp;
            }
            if (sword.State == ItemOnGroundState.DoneAnimating)
            {
                sword.State = ItemOnGroundState.OnGround;
                player.State = PlayerState.Moving;
                portalobj.hidden = false;
                for (int i = 0; i < 50; i++)
                {
                    particleSystem.addParticle();
                }
            }
            particleSystem.Update(gameTime);
            base.Update(ks, ms, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            if (portalobj.hidden)
            {
                sword.Draw(spriteBatch);
            }
            particleSystem.Draw(spriteBatch);
            player.Draw(spriteBatch);
            portalobj.Draw(spriteBatch);

            for (int i = 0; i < grassBarriers.Count; i++)
            {
                grassBarriers[i].Draw(spriteBatch);
            }
        }

    }
}
