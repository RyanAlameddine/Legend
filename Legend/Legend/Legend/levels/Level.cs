﻿using System;
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

        public Level(Texture2D playermove, Texture2D portal, Song music)
        {
            this.music = music;
            this.portal = portal;
            this.playermove = playermove;
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
                    }
                }
                if (angle > 360)
                {
                    angle -= 360;
                }
            }
        }

        public void ExitPortal()
        {
            if (player.Hitbox.Intersects(exitportal.Hitbox) && !spinning && !exitportal.hidden && !smaller)
            {
                spinning = true;
                playerToPortalCenter = new Vector2(player._position.X + player.Hitbox.Width / 2 - exitportal.Position.X + exitportal.Hitbox.Width / 2, player._position.Y + player.Hitbox.Height / 2 - exitportal.Position.Y + exitportal.Hitbox.Height / 2);
                spinRadius = playerToPortalCenter.Length();
                angle = MathHelper.ToDegrees((float)Math.Atan2(playerToPortalCenter.Y, playerToPortalCenter.X));
            }
            if (spinning)
            {

                angle -= .12f;

                Vector2 portalCenter = new Vector2(exitportal.Position.X - player.Hitbox.Width / 2, exitportal.Position.Y - player.Hitbox.Height / 2);
                player._position = new Vector2(portalCenter.X + spinRadius * (float)Math.Cos(angle), portalCenter.Y + spinRadius * (float)Math.Sin(angle));

                if (spinRadius < 40)
                {
                    player.scale += 0.002f;
                    spinRadius = spinRadius + .1f;
                }
                else
                {
                    smaller = true;
                    player.scale = 1;
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
            Game1.inventory.Update(ks, ms);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
