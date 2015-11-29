using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Legend.levels;
using Legend.weapons;
using Legend.levels.functions;
using Legend.levels.sublevels;
using Legend.inventory;
using Legend.particles;
using System.Xml;

namespace Legend
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static XmlDocument xmlDoc;
        public static String saveFile = "save.xml";
        public static Random rand = new Random();
        RenderTarget2D rend;
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Home home;
        Intro intro;
        Intro continueintro;
        KeyboardState ks;
        KeyboardState lastks;
        MouseState ms;
        public static bool transitioneffect = false;
        public static Vector2 rendpos = Vector2.Zero;
        public static Color rendColor = Color.White;
        public static int level = 1;
        public static List<Level> levellist = new List<Level>();
        float rendscale = 1f;
        public static string name = "";
        public static Screens screen = Screens.Home;
        public static Inventory inventory;
        public static int rendOffset = 0;
        public static bool resetRend = false;

        List<int> Size = new List<int>();
        int currentSize = 0;

        public static string currentSong;

        public static TransitionToLevelEffect ttle;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            MediaPlayer.IsVisualizationEnabled = true;
            base.Initialize();
            DisplayMode display = GraphicsDevice.DisplayMode;
            this.Window.AllowUserResizing = true;
            if (display.Width > display.Height)
            {
                int num = display.Height - 100;
                graphics.PreferredBackBufferWidth = num;
                graphics.PreferredBackBufferHeight = num;
                Settings.Scale = num / 310;
                while (num % 310 != 0)
                {
                    num--;
                    graphics.PreferredBackBufferWidth = num;
                    graphics.PreferredBackBufferHeight = num;
                    Settings.Scale = num / 310;
                    graphics.ApplyChanges();
                }
            }
            else
            {
                int num = display.Width - 100;
                graphics.PreferredBackBufferWidth = num;
                graphics.PreferredBackBufferHeight = num;
                Settings.Scale = num / 310;
                while (num % 310 != 0)
                {
                    num--;
                    graphics.PreferredBackBufferWidth = num;
                    graphics.PreferredBackBufferHeight = num;
                    Settings.Scale = num / 310;
                    graphics.ApplyChanges();
                }
            }
            graphics.ApplyChanges();
            for (int i = 620; i < display.Height; i += 310)
            {
                Size.Add(i);
            }

            rend = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        }

        protected override void LoadContent()
        {
            ttle = new TransitionToLevelEffect();

            GameContent.LoadContent(Content);
            GameContent.Loadxml(Content);

            currentSong = "8bit";
            MediaPlayer.Play(GameContent.eightbit);
            MediaPlayer.IsRepeating = true;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            home = new Home(GameContent.normalfont, GameContent.button, GameContent.buttonhover, GameContent.logo);
            intro = new Intro(GameContent.normalfont, Content.Load<Texture2D>("guis/text box"), GameContent.button, GameContent.buttonhover, GameContent.typewriter, GameContent.spacebar, false);
            continueintro = new Intro(GameContent.normalfont, Content.Load<Texture2D>("guis/text box"), GameContent.button, GameContent.buttonhover, GameContent.typewriter, GameContent.spacebar, true);
            levellist = SubLevels.registerLevels(GameContent.playermove, GameContent.grass, GameContent.grassbarrier, GameContent.foamsword, GameContent.portal, GameContent.eightbit, GameContent.cantina_theme, GameContent.arcade, GameContent.normalfont, GameContent.button, GameContent.buttonhover, GameContent.tshirt, GameContent.fourpixels);
            inventory = new Inventory(GameContent.invtxture, GameContent.selectedinventory);
        }
        protected override void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.LeftControl) && lastks.IsKeyUp(Keys.LeftControl))
            {
                currentSize++;
                currentSize %= Size.Count;

                graphics.PreferredBackBufferWidth = Size[currentSize];
                graphics.PreferredBackBufferHeight = Size[currentSize];
                Settings.ScreenSize = Size[currentSize];
                graphics.ApplyChanges();

                Settings.Scale = currentSize + 2;
                rend = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            }
            if (screen == Screens.Home) home.Update(ms);
            if (screen == Screens.Intro) intro.Update(ks, ms, gameTime);
            if (screen == Screens.Continue) continueintro.Update(ks, ms, gameTime);
            if (screen == Screens.Level)
            {
                levellist[level - 1].Update(ks, ms, gameTime);
            }
            lastks = ks;

            if (resetRend)
                resetRendInfo();

            ttle.Update();

            if (ks.IsKeyDown(Keys.F3))
            {
                level = 3;
            }
            if (ks.IsKeyDown(Keys.F2))
            {
                level = 2;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(rend);

            GraphicsDevice.Clear(new Color(208, 159, 81));

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            inventory.Draw(spriteBatch, GameContent.descriptionsfont);
            if (screen == Screens.Home) home.Draw(spriteBatch);
            if (screen == Screens.Intro) intro.Draw(spriteBatch);
            if (screen == Screens.Continue) continueintro.Draw(spriteBatch);
            if (screen == Screens.Level)
            {
                levellist[level - 1].Draw(spriteBatch);
            }
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(rendColor);

            spriteBatch.Begin();
            spriteBatch.Draw(rend, rendpos, null, rendColor, 0f, Vector2.Zero, (float)rendscale, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(GameContent.mouse, new Vector2(ms.X, ms.Y), null, Color.White, 0f, Vector2.Zero, GraphicsDevice.Viewport.Width / 300, SpriteEffects.None, 1);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        void resetRendInfo()
        {
            if (rendColor != Color.White)
            {
                MediaPlayer.Volume += 1f / 255f;
                rendColor.R++;
                rendColor.G++;
                rendColor.B++;
            }
            else
            {
                resetRend = false;
            }
        }
    }
}
