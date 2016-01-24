using Legend.levels.functions;
using Legend.levels.sublevels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Legend.levels
{
    public static class SubLevels
    {
        public static L1 l1;
        public static L2Intro l2intro;
        public static L2 l2;


        public static List<Level> registerLevels(Texture2D playermove, Texture2D playerattack, Texture2D grass, Texture2D grassbarrier, Texture2D foamsword, Texture2D portal, Song eightbit, Song cantina_theme, Song arcade, SpriteFont normalfont, Texture2D button, Texture2D buttonhover, Texture2D tshirt, Texture2D pixel)
        {
            List<Level> levellist = new List<Level>();
            l1 = new L1(playermove, playerattack, grass, grassbarrier, foamsword, portal, eightbit);
            levellist.Add(l1);
            l2intro = new L2Intro(playermove, playerattack, portal, cantina_theme, normalfont, button, buttonhover, tshirt);
            levellist.Add(l2intro);
            l2 = new L2(playermove, playerattack, portal, arcade, pixel);
            levellist.Add(l2);

            return levellist;
        }
    }
}
