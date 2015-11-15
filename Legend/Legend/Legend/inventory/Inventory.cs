﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legend.inventory;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Legend
{
    public class Inventory
    {
        bool draw;
        List<Item> items = new List<Item>();
        Texture2D invtxture;
        Texture2D selectedinventory;
        string description = "";
        bool testing = false;
        Armour armour;
        Weapon weapon;

        public Weapon equiptedWeapon
        {
            get { return weapon; }
        }

        public Armour equiptedArmour
        {
            get { return armour; }
        }

        public Inventory(Texture2D invtxture, Texture2D selectedinventory)
        {
            this.invtxture = invtxture;
            this.selectedinventory = selectedinventory;
            this.armour = new Armour("", selectedinventory, 0, 0);
            this.weapon = new Weapon("", selectedinventory, 0, WeaponPower.no, 0);
        }

        public void AddItem(Item add){
            items.Add(add);
        }

        public void Update(KeyboardState ks, MouseState ms)
        {
            foreach(Item i in items){
                i.Update();
            }
            if (ks.IsKeyDown(Keys.L))
            {
                this.draw = true;
            }
            else
            {
                this.draw = false;
            }
            this.description = "";
            foreach (Item item in items)
            {
                if (item.Hitbox.Contains((int)(ms.X / Settings.Scale), (int)(ms.Y / Settings.Scale)))
                {
                    this.description = item.getDescription();
                    if (ms.LeftButton == ButtonState.Pressed)
                    {
                        if (!testing)
                        {
                            if (item.type == ItemType.Armour)
                            {
                                if (armour.name == "")
                                {
                                    armour = (Armour) item;
                                    item.togglequpited();
                                    testing = true;
                                }
                                else
                                {
                                    armour = (Armour)item;
                                    item.togglequpited();
                                    testing = true;
                                    foreach (Item itemx in items)
                                    {
                                        if (itemx.name == armour.name)
                                        {
                                            itemx.togglequpited();
                                        }
                                    }
                                }
                            }
                            else if (item.type == ItemType.Weapon)
                            {
                                if (weapon.name == "")
                                {
                                    weapon = (Weapon) item;
                                    item.togglequpited();
                                    testing = true;
                                }
                                else
                                {
                                    item.togglequpited();
                                    testing = true;
                                    foreach (Item itemx in items)
                                    {
                                        if (itemx.name == weapon.name)
                                        {
                                            itemx.togglequpited();
                                        }
                                    }
                                    weapon = (Weapon)item;
                                }
                            }
                        }
                    }
                    else
                    {
                        testing = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (draw)
            {
                Vector2 pos = new Vector2(0, 0);
                spriteBatch.Draw(invtxture, pos * Settings.Scale, null, Color.White, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.7f);
                spriteBatch.DrawString(font, description, new Vector2(12, 111) * Settings.Scale, Color.White, 0f, Vector2.Zero,(float)(.11  * Settings.Scale), SpriteEffects.None, .8f);
                pos.X += 11;
                pos.Y += 28;

                int counter = 1;
                foreach (Item item in items)
                {

                    if (item.equiptstatus == "equipped.")
                    {
                        spriteBatch.Draw(selectedinventory, pos * Settings.Scale, null, Color.White, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.8f);
                    }

                    item.Draw(spriteBatch, pos);
                    pos.X += 16;


                    //moving to next line
                    if (counter >= 5)
                    {
                        counter = 0;
                        pos.X = 11;
                        pos.Y += 16;
                    }

                    counter++;
                }
            }
        }

    }
}
