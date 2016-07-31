using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace Legend
{
    public class Home
    {
        SpriteFont _font;
        Texture2D _button;
        Texture2D _buttonhover;
        Texture2D _logo;
        Button thebutton;
        Button continuebutton;
        public Home(SpriteFont font, Texture2D button, Texture2D buttonhover, Texture2D logo)
        {
            _font = font;
            _button = button;
            _buttonhover = buttonhover;
            _logo = logo;
            thebutton = new Button(_button,_buttonhover, _font, "New Game", new Vector2(118, 175));
            continuebutton = new Button(_button, _buttonhover, font, "Continue", new Vector2(118, 200));
        }

        public void Update()
        {
            if (thebutton.buttonpressed())
            {
                Game1.screen = Screens.Intro;
            }
            if (continuebutton.buttonpressed() && Game1.xmlDoc.GetElementsByTagName("user")[0] != null)
            {
                Game1.screen = Screens.Continue;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_logo, new Vector2(60, 80) * Settings.Scale, null, Color.White, 0f, Vector2.Zero, .4f * Settings.Scale, SpriteEffects.None, 0.5f);
            if (Game1.xmlDoc.GetElementsByTagName("user")[0] != null)
            {
                continuebutton.Draw(spriteBatch);
            }
            thebutton.Draw(spriteBatch);
        }

    }
}
