using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legend.levels.functions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Legend.characters;
using Legend.levels.objects;
using Legend.functions;
using Legend.enemy;
using Legend.particles;

namespace Legend.levels.sublevels
{
    public class L2 : Level
    {
        List<Sprite> grassBarriers = new List<Sprite>();
        Texture2D fourpixels;
        ParticleSystem particleSystem;

        public L2(Texture2D playertxture, Texture2D playerattack, Texture2D portaltxture, Song song, Texture2D fourpixels)
            : base(playertxture, portaltxture, song)
        {
            player = new Player(playertxture, playerattack, new Vector2(150, 245));
            this.fourpixels = fourpixels;
            background = new Background(fourpixels);
            exitportal = new ExitPortal(portaltxture, new Vector2(155, 250));
            player.State = PlayerState.Interacting;
            player._frame = player._downWalkingFrames[1];
            enemies.Add(new Enemy(GameContent.selectedinventory, new Vector2(150, 30)));
            particleSystem = new ParticleSystem(GameContent.fourpixels, 0f, 1f, Color.Red, new Vector2(-2, 2), new Vector2(-2, 2), new TimeSpan(0, 0, 0, 0, 500), 1f, 2f, 1f, 1f, new Vector2(150, 30), new TimeSpan(1000, 0, 1, 0, 0), true);
        }

        public override void Update(KeyboardState ks, MouseState ms, GameTime gameTime)
        {
            player.Update(ks, grassBarriers, ms, gameTime);
            particleSystem.Update(gameTime);
            foreach (Tile tile in background.materials)
            {
                if (player.Hitbox.Intersects(tile.Hitbox))
                {
                    tile.color = new Color(Game1.rand.Next(0, 255), Game1.rand.Next(0, 255), Game1.rand.Next(0, 255));
                }
                else
                {
                    if (tile.color != Color.White)
                    {
                        tile.color = Color.Lerp(tile.color, Color.White, 0.07f);
                        if (tile.color.R > 240 && tile.color.G > 240 && tile.color.B > 240)
                        {
                            tile.color = Color.White;
                        }
                    }
                }
            }
            exitportal.Update();
            ExitPortal();
            base.Update(ks, ms, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            particleSystem.Draw(spriteBatch);
            exitportal.Draw(spriteBatch);
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemies[0].Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public override void enemyHit(int index)
        {
            for (int i = 0; i < 20; i++)
            {
                particleSystem.addParticle();
            }
            base.enemyHit(index);
        }
    }
}
