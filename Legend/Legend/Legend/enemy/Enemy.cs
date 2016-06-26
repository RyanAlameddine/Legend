using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Legend.characters;
using Legend.particles;
using Legend.inventory;
using Legend.weapons;

namespace Legend.enemy
{
    public class Enemy
    {
        public Vector2 pos;
        public Vector2 ori;
        protected Rectangle source;
        Rectangle[] sources;
        SpriteEffects effect;
        protected Texture2D txture;
        public Rectangle Hitbox;
        public Vector2 speed;
        Vector2 dir;
        public Vector2 speedoffset;
        float rotation = 0f;
        public Direction direction = Direction.Up;
        public int damage;
        int health;
        ParticleSystem deadparticles;
        TimeSpan deathtimer;
        bool draw = true;
        Dictionary<inventory.Item, Vector2> itemdrops = new Dictionary<inventory.Item, Vector2>();
        bool isAngry;
        public Enemy(Texture2D txture, Vector2 pos, Rectangle[] sources, int damage, int health, ParticleSystem deadparticles, Dictionary<inventory.Item, Vector2> itemdrops, bool isAngry)
        {
            this.pos = pos;
            this.txture = txture;
            this.source = sources[0];
            this.sources = sources;
            this.damage = damage;
            this.health = health;
            this.deadparticles = deadparticles;
            this.isAngry = isAngry;
            deathtimer = deadparticles.lifetime - new TimeSpan(0, 0, 0, 0, 10);
            ori = new Vector2(source.Width / 2, source.Height / 2);
            Hitbox = new Rectangle((int)(pos.X), (int)(pos.Y), (int)(source.Width/2), (int)(source.Height/2));
            speedoffset.X = -6/15 + 1;
            speedoffset.Y = speedoffset.X;
            this.itemdrops = itemdrops;
        }

        public virtual void Update(GameTime gameTime, Player p)
        {
            if (speedoffset == Vector2.Zero)
            {
                deathtimer -= gameTime.ElapsedGameTime;
            }
            if (deathtimer <= TimeSpan.Zero)
            {
                if (draw)
                {
                    draw = false;
                    deathtimer = new TimeSpan(0, 0, 5);
                }
                else
                {
                    try
                    {
                        Game1.levellist[Game1.level - 1].enemies.Remove(this);
                    }
                    catch(NullReferenceException e)
                    {
                        e.ToString();
                    }
                }
            }
            //ADD NOT ANGRY MOVEMENT
            if (speedoffset != Vector2.Zero && isAngry)
            {
                if (p._position.X - 2 > pos.X)
                {
                    dir.X = 1;
                }
                else if (p._position.X - 2 < pos.X)
                {
                    dir.X = -1;
                }
                if (p._position.Y + 5 > pos.Y)
                {
                    dir.Y = 1;
                }
                else if (p._position.Y + 5 < pos.Y)
                {
                    dir.Y = -1;
                }
                Vector2 distance = p._position - pos;
                rotation = (float)Math.Atan2(distance.Y, distance.X);
                rotation *= (float)(180 / Math.PI);
                while (rotation > 360)
                {
                    rotation -= 360;
                }
                if (rotation <= 45 && rotation >= -45)
                {
                    direction = Direction.Right;
                    source = sources[0];
                    effect = SpriteEffects.None;
                }
                else if (rotation >= 45 && rotation <= 135)
                {
                    direction = Direction.Up;
                    source = sources[2];
                    effect = SpriteEffects.FlipVertically;
                }
                else if (rotation >= 135 || rotation <= -135)
                {
                    direction = Direction.Left;
                    source = sources[0];
                    effect = SpriteEffects.FlipHorizontally;
                }
                else if (rotation >= -135 && rotation <= -45)
                {
                    direction = Direction.Down;
                    source = sources[2];
                    effect = SpriteEffects.None;
                }

                speed = Vector2.Lerp(speed, dir, 0.05f);
                if (p.Hitbox.Intersects(Hitbox) && p.State != PlayerState.Interacting)
                {
                    Vector2 temp = new Vector2(p.speedx * p.speed * 2f, p.speedy * p.speed * 2f);
                    p.speedx = dir.X * 2f;
                    p.speedy = dir.Y * 2f;
                    speed = temp;
                    p.resetspeed = false;
                    p.State = PlayerState.Interacting;
                    Game1.healthManager.health -= damage / Game1.inventory.armour.defence;
                }
                Hitbox.X = (int)((pos.X - (ori.X / 2)));
                Hitbox.Y = (int)((pos.Y - (ori.Y / 2)));
                pos += speed * speedoffset * Game1.deathspeed;
            }
            deadparticles.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (draw)
            {
                spriteBatch.Draw(txture, pos * Settings.Scale, source, Color.White, 0f, ori, .5f * Settings.Scale, effect, .45f);
            }
            deadparticles.Draw(spriteBatch);
        }

        public void hurt(int damage)
        {
            health -= damage;
            isAngry = true;
            if (health <= 0 && speedoffset != Vector2.Zero)
            {
                deadparticles.position = pos;
                Game1.levellist[Game1.level - 1].deadenemyparticle(deadparticles, 100);
                speedoffset = Vector2.Zero;
                draw = false;
                Item add = new Weapon("", GameContent.fourpixels, 0, WeaponPower.no, 0);
                for (int i = 0; i < itemdrops.Count; i++)
                {
                    if (Game1.rand.Next((int)itemdrops[itemdrops.Keys.ToList()[i]].X, (int)itemdrops[itemdrops.Keys.ToList()[i]].Y) == 0)
                    {
                        add = itemdrops.Keys.ToList()[i];
                        break;
                    }
                }
                if(add.name != ""){
                    Game1.levellist[Game1.level - 1].mobdropsonfloor.Add(new ItemOnFloor(add, pos, .2f, .001f, 0.08f, 6.25f));
                }
            }
        }
    }
}
