using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Legend.levels;
using Legend.levels.functions;
using System.Xml;

namespace Legend
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        int width;
        int height;

        BasicEffect effect;
        Camera camera;

        public static XmlDocument xmlDoc;
        public static String saveFile = "save.xml";
        public static Random rand = new Random();
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Home home;
        Intro intro;
        Intro continueintro;
        GameOver gameover;
        KeyboardState ks;
        KeyboardState lastks;
        MouseState ms;
        public static bool transitioneffect = false;
        public static Color rendColor = Color.White;
        public static int level = 1;
        public static List<Level> levellist = new List<Level>();
        public static string name = "";
        public static Screens screen = Screens.Home;
        public static Inventory inventory;
        public static int rendOffset = 0;
        public static bool resetRend = false;
        public static HealthManager healthManager;
        public static float deathspeed = 1;
        public static bool toinitialize = false;
        public static bool quitbool = false;
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

            camera = new Camera(GraphicsDevice, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
            
            effect = new BasicEffect(GraphicsDevice);
            effect.VertexColorEnabled = true;
            effect.TextureEnabled = true;
            effect.World = Matrix.Identity;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.DiffuseColor = rendColor.ToVector3();
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
            levellist = SubLevels.registerLevels(GameContent.playermove, GameContent.playerattack, GameContent.grass, GameContent.grassbarrier, GameContent.foamsword, GameContent.portal, GameContent.eightbit, GameContent.cantina_theme, GameContent.arcade, GameContent.normalfont, GameContent.button, GameContent.buttonhover, GameContent.tshirt, GameContent.fourpixels, GameContent.slimeparticle, GameContent.skyportal, GameContent.tooltip, GameContent.descriptionsfont, GameContent.keytxture, GameContent.keydown);
            gameover = new GameOver(GameContent.gameovertexture, GameContent.button, GameContent.buttonhover, GameContent.normalfont);
            inventory = new Inventory(GameContent.invtxture, GameContent.selectedinventory);
            healthManager = new HealthManager(GameContent.hitparticle, GameContent.fourpixels);
            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;
        }
        protected override void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            ks = Keyboard.GetState();
            InputManager.Update(ms, ks);
            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;
            if (ks.IsKeyDown(Keys.LeftControl) && lastks.IsKeyUp(Keys.LeftControl))
            {
                currentSize++;
                currentSize %= Size.Count;

                graphics.PreferredBackBufferWidth = Size[currentSize];
                graphics.PreferredBackBufferHeight = Size[currentSize];
                Settings.ScreenSize = Size[currentSize];
                graphics.ApplyChanges();

                Settings.Scale = currentSize + 2;
                camera.UpdateProjection();
                camera.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            }
            if (screen == Screens.Home) home.Update();
            if (screen == Screens.Intro) intro.Update(gameTime);
            if (screen == Screens.Continue) continueintro.Update(gameTime);
            if (screen == Screens.Level)
            {
                levellist[level - 1].Update(gameTime);
            }
            if (screen == Screens.GameOver) gameover.Update();
            lastks = ks;

            if (resetRend)
                resetRendInfo();

            if (quitbool)
            {
                quit();
            }

            if (ks.IsKeyDown(Keys.F11))
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }

            ttle.Update();
            healthManager.Update();

            effect.DiffuseColor = rendColor.ToVector3();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            effect.View = camera.View;
            effect.Projection = camera.Projection;

            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, effect);
            spriteBatch.Draw(GameContent.fourpixels, new Rectangle(-200, -200, GraphicsDevice.Viewport.Width + 400, GraphicsDevice.Viewport.Height + 400), new Color(208, 159, 81));
            inventory.Draw(spriteBatch, GameContent.descriptionsfont);
            if (screen == Screens.Home) home.Draw(spriteBatch);
            if (screen == Screens.Intro) intro.Draw(spriteBatch);
            if (screen == Screens.Continue) continueintro.Draw(spriteBatch);
            if (screen == Screens.Level)
            {
                levellist[level - 1].Draw(spriteBatch);
            }
            if (screen == Screens.GameOver) gameover.Draw(spriteBatch);
            healthManager.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.Draw(GameContent.mouse, new Vector2(ms.X, ms.Y), null, Color.White, 0f, Vector2.Zero, GraphicsDevice.Viewport.Width / 300, SpriteEffects.None, 1);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void resetRendInfo()
        {
            if (rendColor != Color.White)
            {
                MediaPlayer.Volume += 1f / 255f;
                rendColor = new Color(rendColor.R + 1, rendColor.G + 1, rendColor.B + 1);
            }
            else
            {
                resetRend = false;
            }

            if (toinitialize)
            {
                transitioneffect = false;
                camera.Offset = Vector2.Zero;
                rendColor = Color.White;
                level = 1;
                levellist = new List<Level>();
                name = "";
                screen = Screens.Home;
                rendOffset = 0;
                resetRend = false;
                deathspeed = 1;
                toinitialize = false;
                Size = new List<int>();
                currentSize = 0;
                Initialize();
                resetRend = false;
                camera.Rotation = (float)Math.PI;
                camera.Scale = Vector2.One;
            }
        }

        public void quit()
        {
            Exit();
        }
    }
}
