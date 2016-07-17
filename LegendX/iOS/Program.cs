using System;
using Foundation;
using UIKit;
using Legend;

namespace iOS
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static GameApplication game;

        internal static void RunGame()
        {
           game = new GameApplication();
           game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}
