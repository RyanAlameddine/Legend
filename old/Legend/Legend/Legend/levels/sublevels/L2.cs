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
using Legend.inventory;
using Legend.tooltip;

namespace Legend.levels.sublevels
{
    public class L2 : Level
    {
        Texture2D fourpixels;
        ToolTip attacktip;
        KeyAnimation attacktipkeyanim;

        public L2(Texture2D playertxture, Texture2D playerattack, Texture2D portaltxture, Song song, Texture2D fourpixels, Texture2D slimeparticle, Texture2D skyportal, Texture2D tooltiptxture, SpriteFont font, Texture2D keytxture, Texture2D keydown)
            : base(playertxture, portaltxture, song)
        {
            player = new Player(playertxture, playerattack, new Vector2(150, 245));
            this.fourpixels = fourpixels;
            background = new Background(fourpixels);
            exitportal = new ExitPortal(portaltxture, new Vector2(155, 250));
            player.State = PlayerState.Interacting;
            player._frame = player._downWalkingFrames[1];
            Dictionary<inventory.Item, Vector2> globdrops = new Dictionary<inventory.Item, Vector2>();
            globdrops.Add(Items.GetItem("Gold Nugget"), new Vector2(0, 2));

            enemies.Add(new Glob(GameContent.glob, new Vector2(150, 30), slimeparticle, globdrops));
            enemies.Add(new Glob(GameContent.glob, new Vector2(150, 150), slimeparticle, globdrops));

            //attacktip\\
            List<ToolTipObj> objects = new List<ToolTipObj>();
            objects.Add(new Text(.3f, new Vector2(110, 17), .91f, font, "Press J to attack"));
            List<Key> keys = new List<Key>();
            keys.Add(new Key(.3f, new Vector2(50, 25), .911f, font, 'J', keytxture, keydown));
            keys.Add(new Key(.3f, new Vector2(50, 25), .91f, font, 'J', keytxture, keydown));
            ToolTipPlayer tooltipPlayer = new ToolTipPlayer(playermove, playerattack, new Vector2(80, 25), .91f, 1f / 5.5f, Direction.Down);
            objects.Add(tooltipPlayer);
            attacktipkeyanim = new KeyAnimation(keys, tooltipPlayer);
            foreach (Key thekey in keys)
            {
                objects.Add(thekey);
            }
            attacktip = new ToolTip(tooltiptxture, objects);
            attacktip.enabled = true;

            particleSystem.times = .00015f;
            portalobj = new Portal(skyportal, new Vector2(155, 100));
        }

        public override void Update(GameTime gameTime)
        {
            portalobj.Update();
            attacktip.Update(gameTime);
            attacktipkeyanim.Update(gameTime);
            Portal(gameTime, Color.DarkBlue);
            exitportal.Update();
            ExitPortal();
            if (enemies.Count == 0)
            {
                portalobj.hidden = false;
            }
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
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            attacktip.Draw(spriteBatch);
            portalobj.Draw(spriteBatch);
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
