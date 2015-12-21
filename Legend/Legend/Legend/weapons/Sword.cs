using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Legend.characters;
using Legend.inventory;
using Legend.enemy;

namespace Legend.weapons
{
    public class Sword
    {
        Texture2D txture;
        Vector2 position;
        float rotation;
        float endrotation;
        Rectangle Hitbox;
        Vector2 hilt;
        bool swinging = false;
        Player p;
        float layerDepth = .4f;
        Rectangle f;
        public Sword(Texture2D txture, Player p, Vector2 hilt)
        {
            this.hilt = hilt;
            this.txture = txture;
            Hitbox.Width = txture.Width;
            Hitbox.Height = txture.Height;
            this.p = p;
        }

        public void Update()
        {
            if (Game1.levellist[Game1.level - 1].enemies.Count > 0)
            {
                Matrix invRotationMatrix = Matrix.Invert(Matrix.CreateRotationZ(rotation));

                Vector2 translatedPosition = Vector2.Transform((Game1.levellist[Game1.level - 1].enemies[0].pos - position) * Settings.Scale, invRotationMatrix);

                Rectangle swordOriginalHitBox = new Rectangle((int)(-hilt.X), (int)(-hilt.Y), (int)(txture.Width * 0.6f * Settings.Scale), (int)(txture.Height * 0.6f * Settings.Scale));

                Rectangle globTranslatedHitBox = new Rectangle((int)(translatedPosition.X - Game1.levellist[Game1.level - 1].enemies[0].ori.X), (int)(translatedPosition.Y - Game1.levellist[Game1.level - 1].enemies[0].ori.Y),
                                                               (int)(Game1.levellist[Game1.level - 1].enemies[0].Hitbox.Width * Settings.Scale), (int)(Game1.levellist[Game1.level - 1].enemies[0].Hitbox.Height * Settings.Scale));

                if (swordOriginalHitBox.Intersects(globTranslatedHitBox))
                {
                    Game1.levellist[Game1.level - 1].enemyHit(0);
                }
            }

            if (swinging)
            {
                if (rotation > endrotation)
                {
                    rotation -= 0.15f; // .1
                }
                else
                {
                    p.State = PlayerState.Idle;
                    swinging = false;
                    p.texture = p.playermove;
                    p._frame = f;
                    position.X = 100000;
                }

            }
        }

        public void swing()
        {
            if (txture != GameContent.selectedinventory)
            {
                Hitbox.X = (int)position.X;
                Hitbox.Y = (int)position.Y;
                f = p._frame;
                swinging = true;
                p.State = PlayerState.Attacking;
                if (p.dir == Direction.Up)
                {
                    rotation = 0;
                    position.X = p._position.X + 10;
                    position.Y = p._position.Y + 3;
                    p._frame = p.attackFrames[0];
                }
                else if (p.dir == Direction.Down)
                {
                    rotation = (float)Math.PI;
                    position.X = p._position.X + 11;
                    position.Y = p._position.Y + 15;
                    p._frame = p.attackFrames[1];
                }
                else if (p.dir == Direction.Left)
                {
                    rotation = 3f * (float)Math.PI / 2f;
                    position.X = p._position.X + 1;
                    position.Y = p._position.Y + 11;
                    p._frame = p.attackFrames[2];
                }
                else if (p.dir == Direction.Right)
                {
                    rotation = (float)Math.PI / 2f;
                    position.X = p._position.X + 11;
                    position.Y = p._position.Y + 10;
                    p._frame = p.attackFrames[3];
                }
                endrotation = (float)(rotation - Math.PI / 6f);
                rotation += (float)Math.PI / 2f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (swinging)
            {
                spriteBatch.Draw(txture, position * Settings.Scale, null, Color.White, rotation, hilt, .6f * Settings.Scale, SpriteEffects.None, layerDepth);
            }
        }
    }
}
