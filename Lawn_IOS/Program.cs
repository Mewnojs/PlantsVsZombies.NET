using Foundation;
using Sexy;
using UIKit;

namespace Lawn_IOS
{
    [Register("AppDelegate")]
    internal class Program : UIApplicationDelegate
    {
        private static Main game;

        internal static void RunGame()
        {
            game = new Main();
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, typeof(Program));
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}
