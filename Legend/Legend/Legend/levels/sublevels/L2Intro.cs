using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legend.levels.functions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Legend.inventory;
using Legend.characters;

namespace Legend.levels.sublevels
{
    public class L2Intro :  Level
    {
        Texture2D tshirt;
        SpriteFont font;
        Button button;
        int state = 1;

        public L2Intro(Texture2D playertxture, Texture2D playerattack, Texture2D portaltxture, Song song, SpriteFont font, Texture2D buttontx2d, Texture2D buttontx2dhover, Texture2D tshirt)
            : base(playertxture, portaltxture, song)
        {
            button = new Button(buttontx2d, buttontx2dhover, font, "Continue", new Vector2(120, 270));
            this.font = font;
            this.tshirt = tshirt;
            player = new Player(playermove, playerattack, new Vector2(0, 0));
        }

        public override void Update(GameTime gameTime)
        {
            if (state == 1)
            {
                if (button.buttonpressed())
                {
                    if (Game1.inventory.weapon.name == "Foam Sword")
                    {
                        state++;
                        Game1.inventory.AddItem(Items.GetItem("T-Shirt Armour"));
                    }
                }
            }
            else if (state == 2)
            {
                if (button.buttonpressed())
                {
                    if (Game1.inventory.armour.name == "T-Shirt Armour")
                    {
                        Game1.rendColor = new Color(0, 0, 0);
                        Game1.resetRend = true;
                        MediaPlayer.Volume = 0f;
                        Game1.level++;
                        save();
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == 1)
            {
                spriteBatch.DrawString(font, "Hold  the  L  key  and  click  on\nthe  foam  sword  to  equip  it", new Vector2(22, 100) * Settings.Scale, Color.White, 0f, Vector2.Zero, 1.5f * Settings.Scale, SpriteEffects.None, 0.6f);
                button.Draw(spriteBatch);
            }
            else if (state == 2)
            {
                spriteBatch.DrawString(font, "I  just  gave  you  tshirt  armour\nequip  it  and  then  continue", new Vector2(22, 100) * Settings.Scale, Color.White, 0f, Vector2.Zero, 1.5f * Settings.Scale, SpriteEffects.None, 0.6f);
                button.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }

    }
}
