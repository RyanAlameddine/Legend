using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Legend.levels
{
    public class GameOver
    {
        Texture2D gameovertexture;
        Button yes;
        Button no;

        public GameOver(Texture2D gameovertexture, Texture2D button, Texture2D buttonhover, SpriteFont font)
        {
            this.gameovertexture = gameovertexture;
            yes = new Button(button, buttonhover, font, "Yes", new Vector2(120, 150));
            no = new Button(button, buttonhover, font, "No", new Vector2(120, 200));
        }

        public void Update()
        {
            if(yes.buttonpressed())
            {
                Game1.resetRend = true;
                Game1.toinitialize = true;
                Game1.screen = Screens.Home;
            }
            if (no.buttonpressed())
            {
                Game1.quitbool = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameovertexture, new Vector2(100, 50), Color.White);
            yes.Draw(spriteBatch);
            no.Draw(spriteBatch);
        }
    }
}
