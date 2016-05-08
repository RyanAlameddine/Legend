using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using Legend.inventory;

namespace Legend.levels
{
    public class Intro
    {
        SpriteFont _font;
        Texture2D _textbox;
        string word = "";
        Keys? lk;
        int wait = 0;
        int x = 105;
        Button button;
        string text = "";
        string targetText = "One  day,  in  a  land  ovverrun\nwith  monsters  called  gliir,  there  was\na  person  who  must  bring  order\nand  stop  the  chaos  that  has\ncovered  the  land.  You  are  that  person.\nfirst,  enter  your  name.\nThen  press  k  to  recover  your  first  sword.....";
        public TimeSpan timer = new TimeSpan(0, 0, -1);
        SoundEffect typewriter;
        SoundEffect spacebar;
        bool soundPlayed;
        Random random;
        bool error = false;
        Color errorcolor = Color.Red;
        bool continuebool;
        Vector2 pos = new Vector2(25, 20);
        String errortext = "This name is already taken!";

        public Intro(SpriteFont font, Texture2D textbox, Texture2D buttontx2d, Texture2D buttontx2dhover, SoundEffect typewriter, SoundEffect spacebar, bool continuebool)
        {
            random = new Random();
            this.typewriter = typewriter;
            this.spacebar = spacebar;
            _font = font;
            _textbox = textbox;
            button = new Button(buttontx2d, buttontx2dhover, _font, "Begin", new Vector2(118, 200));
            this.continuebool = continuebool;
            if (continuebool)
            {
                targetText = "Welcome  Back!";
                pos = new Vector2(108, 75);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (InputManager.ks.IsKeyDown(Keys.Escape))
            {
                Game1.screen = Screens.Home;
            }
            timer += gameTime.ElapsedGameTime;
            if (!soundPlayed && targetText.Length > text.Length && timer.Milliseconds > 85)
            {
                if (targetText[text.Length].ToString() == " ")
                {
                    SoundEffectInstance space = spacebar.CreateInstance();
                    space.Pitch = random.Next(9, 11) / 10;
                    space.Volume = 0.35f;
                    space.Play();
                }
                else
                {
                    SoundEffectInstance presskey = typewriter.CreateInstance();
                    presskey.Pitch = random.Next(9, 11) / 10;
                    presskey.Volume = 0.5f;
                    presskey.Play();
                }
                soundPlayed = true;
            }
            if (targetText.Length > text.Length && timer.Milliseconds > 90)
            {
                timer = TimeSpan.Zero;
                if (targetText[text.Length].ToString() == " ")
                {
                    text += "  ";
                }
                else
                {
                    text += targetText[text.Length];

                }
                soundPlayed = false;
            }

            if (button.buttonpressed())
            {
                levelplus();

            }
            foreach (Keys key in InputManager.ks.GetPressedKeys())
            {
                if(key != Keys.None){
                    if (key == Keys.Enter)
                    {
                        levelplus();
                    }
                    int l = word.Length;
                    int x = 0;
                    foreach (char c in word)
                    {
                        if (c == ' ')
                        {
                            x++;
                        }
                        if (x == 5)
                        {
                            l -= 4;
                        }
                    }
                    if (l <= 12 || key == Keys.Back)
                    {
                        if (key != lk)
                        {
                            if (key == Keys.Space)
                            {
                                word += "  ";
                            }
                            else if (key == Keys.Back)
                            {
                                if (word.Count() > 0)
                                {
                                    if (word[word.Count() - 1] == ' ')
                                    {
                                        word = word.Substring(0, word.Count() - 1);
                                    }
                                    word = word.Substring(0, word.Count() - 1);
                                }
                            }
                            else if (key == Keys.OemPeriod)
                            {
                                word += ".";
                            }
                            else if (key == Keys.OemComma)
                            {
                                word += ",";
                            }
                            else if ((int)key >= 0 && (int)key <= 47)
                            {
                                //ignoring characters
                            }
                            else if ((int)key >= 91 && (int)key <= 254)
                            {
                                //ignoring characters
                            }
                            else if ((int)key >= (int)Keys.D0 && (int)key <= (int)Keys.D9)
                            {
                                word += (int)key - 48;
                            }
                            else
                            {

                                if (InputManager.ks.IsKeyDown(Keys.LeftShift) || InputManager.ks.IsKeyDown(Keys.RightShift))
                                {
                                    if (!System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))
                                    {
                                        word += key.ToString();
                                    }
                                    else
                                    {
                                        word += key.ToString().ToLower();
                                    }
                                }
                                else if (System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))
                                {
                                    word += key.ToString();
                                }
                                else
                                {
                                    word += key.ToString().ToLower();
                                }
                            }
                            wait = 0;
                            lk = key;
                        }
                        else
                        {
                            wait++;
                            if (wait >= 10)
                            {
                                lk = null;
                                wait = 0;
                            }

                        }
                    }
                    break;
                }
            }
        }

        void levelplus()
        {
            if (continuebool == false)
            {
                if (word != "")
                {
                    foreach (XmlElement e in Game1.xmlDoc.GetElementsByTagName("user"))
                    {

                        //XmlElement name = ((XmlElement)e.GetElementsByTagName("name")[0]);
                        if (e.Attributes["name"].Value.ToLower() == word.ToLower())
                        {
                            error = true;
                            errorcolor = Color.Red;
                            break;
                        }
                    }
                    if (error == false)
                    {
                        XmlElement characterElement = Game1.xmlDoc.CreateElement("user");
                        characterElement.SetAttribute("name", word);
                        characterElement.SetAttribute("level", "1");

                        XmlElement inventoryElement = Game1.xmlDoc.CreateElement("inventory");

                        characterElement.AppendChild(inventoryElement);

                        Game1.xmlDoc.DocumentElement.AppendChild(characterElement);
                        Game1.xmlDoc.Save(Game1.saveFile);

                        Game1.screen = Screens.Level;
                        Game1.name = word;
                    }
                }
            }
            else
            {
                if (word != "")
                {
                    foreach (XmlElement e in Game1.xmlDoc.GetElementsByTagName("user"))
                    {
                        error = true;
                        if (e.Attributes["name"].Value.ToLower() == word.ToLower())
                        {
                            foreach (XmlElement elem in ((XmlElement)e.GetElementsByTagName("inventory")[0]).GetElementsByTagName("item"))
                            {
                                Item tempitem = Items.GetItem(elem.GetAttribute("name"));
                                if (elem.GetAttribute("equiptstatus") == "equipped.")
                                {
                                    tempitem.togglequpited();
                                    if(elem.GetAttribute("type") == "Weapon"){
                                        Game1.inventory.weapon = (Weapon) tempitem;
                                    }else{
                                        Game1.inventory.armour = (Armour) tempitem;
                                    }
                                }
                                Game1.inventory.AddItem(tempitem);
                            }
                            Game1.level = int.Parse(e.GetAttribute("level"));
                            Game1.screen = Screens.Level;
                            Game1.name = word;
                            Game1.inventory.setsword();
                        }
                    }
                    if (error)
                    {
                        errorcolor = Color.Red;
                        errortext = " This user does not exist!";
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, text, pos * Settings.Scale, Color.Black, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.6f);
            spriteBatch.DrawString(_font, "Your  name  is:", new Vector2(110, 130) * Settings.Scale, Color.Black, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.6f);
            spriteBatch.DrawString(_font, word + "|", new Vector2(x, 155) * Settings.Scale, Color.Green, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.6f);
            spriteBatch.Draw(_textbox, new Vector2(-22, 150) * Settings.Scale, null, Color.White, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.8f);
            button.Draw(spriteBatch);
            if (error)
            {
                spriteBatch.DrawString(_font, errortext, new Vector2(75, 240) * Settings.Scale, errorcolor, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.6f);
                errorcolor = Color.Lerp(errorcolor, Color.Transparent, 0.015f);
                if (errorcolor.R < 10 && errorcolor.G < 10 && errorcolor.B < 10)
                {
                    errorcolor = Color.Transparent;
                    error = false;
                }
            }
        }
    }
}
