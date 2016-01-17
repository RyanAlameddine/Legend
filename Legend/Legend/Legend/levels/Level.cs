using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legend.characters;
using Legend.levels.objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Legend.levels.functions;
using System.Xml;
using Legend.inventory;
using Legend.enemy;
using Legend.particles;

namespace Legend.levels
{
    public class Level
    {
        public Texture2D playermove;
        public Player player;
        public Background background;
        public bool spinning;
        public Vector2 playerToPortalCenter;
        public float spinRadius, angle;
        public Portal portalobj;
        public Texture2D portal;
        bool starting = true;
        bool smaller = false;
        Song music;
        TimeSpan timer;
        TimeSpan timeUntilNextLevel = new TimeSpan(0, 0, 0, 5, 0);
        public ExitPortal exitportal;
        public List<Enemy> enemies = new List<Enemy>();
        ParticleSystem particleSystem;

        public Level(Texture2D playermove, Texture2D portal, Song music)
        {
            this.music = music;
            this.portal = portal;
            this.playermove = playermove;
            particleSystem = new ParticleSystem(GameContent.fourpixels, 0f, 1f, Color.Red, new Vector2(-2, 2), new Vector2(-2, 2), new TimeSpan(0, 0, 0, 0, 500), 1f, 2f, 1f, 1f, new Vector2(150, 30), new TimeSpan(1000, 0, 1, 0, 0), true, .00015f);
        }

        public void Portal(GameTime gameTime, Color portalcolor)
        {
            if (player.Hitbox.Intersects(portalobj.Hitbox) && !spinning && !portalobj.hidden)
            {
                player.State = PlayerState.Interacting;
                spinning = true;
                playerToPortalCenter = new Vector2(player._position.X + player.Hitbox.Width / 2 - portalobj.Position.X + portalobj.Hitbox.Width / 2, player._position.Y + player.Hitbox.Height / 2 - portalobj.Position.Y + portalobj.Hitbox.Height / 2);
                spinRadius = playerToPortalCenter.Length();
                angle = MathHelper.ToDegrees((float)Math.Atan2(playerToPortalCenter.Y, playerToPortalCenter.X));
                angle = MathHelper.Clamp(angle, -10, 10);
            }
            if (spinning)
            {

                angle -= .12f;

                Vector2 portalCenter = new Vector2(portalobj.Position.X - player.Hitbox.Width / 2, portalobj.Position.Y - player.Hitbox.Height / 2);
                player._position = new Vector2(portalCenter.X + spinRadius * (float)Math.Cos(angle), portalCenter.Y + spinRadius * (float)Math.Sin(angle));

                if (spinRadius > 0)
                {
                    player.scale -= 0.001f;
                    spinRadius = spinRadius - .17f;
                }
                else
                {
                    Game1.transitioneffect = true;
                    player.scale = 0;
                    portalobj.state = PortalState.Smaller;
                    timer += gameTime.ElapsedGameTime;
                    Game1.rendOffset = timer.Seconds * 55;
                    if (timer > timeUntilNextLevel)
                    {
                        Game1.transitioneffect = false;
                        Game1.ttle.Reset();
                        Game1.level++;
                        save();
                    }
                }
                if (angle > 360)
                {
                    angle -= 360;
                }
            }
        }

        protected void save()
        {
            foreach (XmlElement userElement in Game1.xmlDoc.GetElementsByTagName("user"))
            {
                if (userElement.GetAttribute("name") == Game1.name)
                {
                    userElement.SetAttribute("level", Game1.level.ToString());
                    XmlElement inventoryElement = (XmlElement)userElement.GetElementsByTagName("inventory")[0];
                    inventoryElement.RemoveAll();
                    foreach (Item item in Game1.inventory.items)
                    {
                        XmlElement itemElement = Game1.xmlDoc.CreateElement("item");
                        itemElement.SetAttribute("name", item.name);
                        itemElement.SetAttribute("type", item.type.ToString());
                        itemElement.SetAttribute("cost", item.cost.ToString());
                        switch (item.type)
                        {
                            case ItemType.Armour:
                                Armour armour = (Armour)item;
                                itemElement.SetAttribute("defence", armour.defence.ToString());
                                itemElement.SetAttribute("equiptstatus", armour.equiptstatus);
                                break;
                            case ItemType.Consumable:
                                Consumable consumable = (Consumable)item;
                                itemElement.SetAttribute("health", consumable.health.ToString());
                                break;
                            case ItemType.Weapon:
                                Weapon weapon = (Weapon)item;
                                itemElement.SetAttribute("damage", weapon.damage.ToString());
                                itemElement.SetAttribute("power", weapon.power.ToString());
                                itemElement.SetAttribute("equiptstatus", weapon.equiptstatus);
                                break;
                            case ItemType.Misc:
                                break;
                            default:
                                break;
                        }
                        inventoryElement.AppendChild(itemElement);
                    }
                    Game1.xmlDoc.Save(Game1.saveFile);
                    break;
                }
            }
            Game1.inventory.setsword();
        }

        public void ExitPortal()
        {
            if (player.Hitbox.Intersects(exitportal.Hitbox) && !spinning && !exitportal.hidden && !smaller)
            {
                spinning = true;
                playerToPortalCenter = new Vector2(player._position.X + player.Hitbox.Width / 2 - exitportal.Position.X + exitportal.Hitbox.Width / 2, player._position.Y + player.Hitbox.Height / 2 - exitportal.Position.Y + exitportal.Hitbox.Height / 2);
                spinRadius = playerToPortalCenter.Length();
                angle = MathHelper.ToDegrees((float)Math.Atan2(playerToPortalCenter.Y, playerToPortalCenter.X));
                player.scale = 0f;
            }
            if (spinning)
            {

                angle -= .12f;

                Vector2 portalCenter = new Vector2(exitportal.Position.X - player.Hitbox.Width / 2, exitportal.Position.Y - player.Hitbox.Height / 2);
                player._position = new Vector2(portalCenter.X + spinRadius * (float)Math.Cos(angle), portalCenter.Y + spinRadius * (float)Math.Sin(angle));

                if (spinRadius < 40)
                {
                    player.scale += 0.0005f;
                    spinRadius = spinRadius + .1f;
                }
                else
                {
                    smaller = true;
                    player.scale = 1/5.5f;
                    player.State = PlayerState.Idle;
                    exitportal.state = PortalState.Smaller;
                    spinning = false;
                }
                if (angle > 360)
                {
                    angle -= 360;
                }
            }
        }

        public virtual void Update(KeyboardState ks, MouseState ms, GameTime gameTime)
        {
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, player);
            }
            if (starting)
            {
                string[] thing = music.Name.Split('\\');
                string currSong = thing[1];
                if (Game1.currentSong != currSong)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(music);
                    Game1.currentSong = currSong;
                }
                starting = false;
            }
            Game1.inventory.Update(ks, ms, gameTime);
            particleSystem.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            particleSystem.Draw(spriteBatch);
            foreach (Enemy e in enemies)
            {
                e.Draw(spriteBatch);
            }
        }

        public virtual void enemyHit(int index)
        {
            particleSystem.position = enemies[index].pos;
            for (int i = 0; i < 5; i++)
            {
                particleSystem.addParticle();
            }
        }
    }
}
