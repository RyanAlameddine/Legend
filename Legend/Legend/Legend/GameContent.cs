using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Xml;

namespace Legend
{
    public static class GameContent
    {
        public static Texture2D button;
        public static Texture2D buttonhover;
        public static Texture2D mouse;
        public static Texture2D grass;
        public static Texture2D playermove;
        public static Texture2D grassbarrier;
        public static Texture2D foamsword;
        public static Texture2D portal;
        public static Texture2D invtxture;
        public static Texture2D selectedinventory;
        public static Texture2D tshirt;
        public static Texture2D fourpixels;
        public static Texture2D logo;

        public static Song cantina_theme;
        public static Song eightbit;

        public static SoundEffect typewriter;
        public static SoundEffect spacebar;

        public static SpriteFont normalfont;
        public static SpriteFont descriptionsfont;


        public static void LoadContent(ContentManager content){

            GameContent.mouse = content.Load<Texture2D>("mouse/mouse");
            GameContent.tshirt = content.Load<Texture2D>("armour/tshirt");
            GameContent.foamsword = content.Load<Texture2D>("weapons/foam sword");
            GameContent.normalfont = content.Load<SpriteFont>("fonts/normal");
            GameContent.descriptionsfont = content.Load<SpriteFont>("fonts/descriptions");
            GameContent.button = content.Load<Texture2D>("buttons/button");
            GameContent.buttonhover = content.Load<Texture2D>("buttons/button hover");
            GameContent.grass = content.Load<Texture2D>("background/grass");
            GameContent.playermove = content.Load<Texture2D>("player/playermove");
            GameContent.grassbarrier = content.Load<Texture2D>("obstacles/grassbarrier");
            GameContent.portal = content.Load<Texture2D>("objects/portal");
            GameContent.invtxture = content.Load<Texture2D>("guis/inventory");
            GameContent.selectedinventory = content.Load<Texture2D>("buttons/selectedinventory");
            GameContent.fourpixels = content.Load<Texture2D>("fourpixels");
            GameContent.logo = content.Load<Texture2D>("logo");

            GameContent.eightbit = content.Load<Song>("music/8bit");
            GameContent.cantina_theme = content.Load<Song>("music/cantina_theme");
            GameContent.typewriter = content.Load<SoundEffect>("sound effects/typewriter");
            GameContent.spacebar = content.Load<SoundEffect>("sound effects/typewriter-space");
        }

        public static void Loadxml(ContentManager Content)
        {

            Game1.xmlDoc = new XmlDocument();
            Game1.xmlDoc.Load(Game1.saveFile);            

            //foreach (XmlElement e in Game1.xmlDoc.GetElementsByTagName("user"))
            //{
            //    if (e.Attributes["name"].Value == "Dank Memes")
            //    {
            //        Game1.level = int.Parse(e.Attributes["level"].Value);
            //        e.SetAttribute("name", "Steel Beams");
            //        break;
            //    }

            //}
            //Game1.xmlDoc.Save(Game1.saveFile);
        }
    }
}
