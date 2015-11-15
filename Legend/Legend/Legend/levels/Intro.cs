using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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
        string targetText = "One  day,  in  a  land  called  gliir,\noverrun  with  monsters,  there  was  a\nperson  who  must  bring  order\nand  stop  the  chaos  that  has\ncovered  the  land.  You  are  that  person.\nfirst,  enter  your  name.\nThen  press  k  to  recover  your  first  sword.....";
        //string targetText = "ham";
        public TimeSpan timer = new TimeSpan(0, 0, -1);
        SoundEffect typewriter;
        SoundEffect spacebar;
        bool soundPlayed;
        Random random;

        public Intro(SpriteFont font, Texture2D textbox, Texture2D buttontx2d, Texture2D buttontx2dhover, SoundEffect typewriter, SoundEffect spacebar)
        {
            random = new Random();
            this.typewriter = typewriter;
            this.spacebar = spacebar;
            _font = font;
            _textbox = textbox;
            button = new Button(buttontx2d, buttontx2dhover, _font, "Begin", new Vector2(118, 200));
        }

        public void Update(KeyboardState keyboard, MouseState ms, GameTime gameTime)
        {
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

            if (button.buttonpressed(ms))
            {
                if (word != "")
                {
                    Game1.screen = Screens.Level;
                    Game1.name = word;
                }

            }
            foreach (Keys key in keyboard.GetPressedKeys())
            {
                if (key == Keys.Enter)
                {
                    if (word != "")
                    {
                        Game1.screen = Screens.Level;
                        Game1.name = word;
                    }
                }
                if (word.Length <= 12 || key == Keys.Back)
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

                            if (keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift))
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, text, new Vector2(25, 20) * Settings.Scale, Color.Black, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.6f);
            spriteBatch.DrawString(_font, "Your  name  is:", new Vector2(110, 130) * Settings.Scale, Color.Black, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.6f);
            spriteBatch.DrawString(_font, word + "|", new Vector2(x, 155) * Settings.Scale, Color.Green, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.6f);
            spriteBatch.Draw(_textbox, new Vector2(-22, 150) * Settings.Scale, null, Color.White, 0f, Vector2.Zero, 1f * Settings.Scale, SpriteEffects.None, 0.8f);
            button.Draw(spriteBatch);

        }
    }
}
