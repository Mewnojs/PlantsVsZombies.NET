using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sexy;

namespace Lawn
{
    public /*internal*/ class GlobalContentManager
    {
        public GlobalContentManager(Main m)
        {
            main = m;
            content = main.Content;
            graphicsDevice = main.GraphicsDevice;
            content.RootDirectory = "Content";
        }

        public void cleanUp()
        {
        }

        public void LoadSplashScreen()
        {
            new ContentManager(main.Services);
        }

        public void LoadGameContent()
        {
            cursor_texture = content.Load<Texture2D>(".\\Cursor");
            LoadFonts();
        }

        public void LoadFonts()
        {
            DEFAULT_FONT = content.Load<SpriteFont>(".\\fonts\\Arial");
            LOCALIZED_FONT_ARIAL = content.Load<SpriteFont>(".\\fonts\\ArialLocalizedFont");
        }

        public void LoadSounds()
        {
        }

        public virtual void LoadLevelBackdrops()
        {
        }

        public Main main;

        public ContentManager content;

        public GraphicsDevice graphicsDevice;

        public SpriteFont DEFAULT_FONT;

        public SpriteFont LOCALIZED_FONT_ARIAL;

        public Texture2D splashScreen_texture;

        public Texture2D splashScreen_ring;

        public Texture2D cursor_texture;
    }
}
