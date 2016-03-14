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
        Texture2D fourpixels;

        public L2(Texture2D playertxture, Texture2D playerattack, Texture2D portaltxture, Song song, Texture2D fourpixels, Texture2D slimeparticle)
            : base(playertxture, portaltxture, song)
        {
            player = new Player(playertxture, playerattack, new Vector2(150, 245));
            this.fourpixels = fourpixels;
            background = new Background(fourpixels);
            exitportal = new ExitPortal(portaltxture, new Vector2(155, 250));
            player.State = PlayerState.Interacting;
            player._frame = player._downWalkingFrames[1];
            enemies.Add(new Glob(GameContent.glob, new Vector2(150, 30), slimeparticle));
            enemies.Add(new Glob(GameContent.glob, new Vector2(150, 150), slimeparticle));
            particleSystem.times = .00015f;
        }

        public override void Update(KeyboardState ks, MouseState ms, GameTime gameTime)
        {
            exitportal.Update();
            ExitPortal();
            base.Update(ks, ms, gameTime);
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
                        tile.color = Color.Lerp(tile.color, Color.White, 0.025f);
                        if (tile.color.R > 214 && tile.color.G > 214 && tile.color.B > 214)
                        {
                            tile.color = Color.White;
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            exitportal.Draw(spriteBatch);
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public override void enemyHit(int index)
        {
            base.enemyHit(index);
        }
    }
}
