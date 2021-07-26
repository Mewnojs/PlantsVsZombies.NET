using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sexy;

namespace Lawn
{
	internal class GlobalContentManager
	{
		public GlobalContentManager(Main m)
		{
			this.main = m;
			this.content = this.main.Content;
			this.graphicsDevice = this.main.GraphicsDevice;
			this.content.RootDirectory = "Content";
		}

		public void cleanUp()
		{
		}

		public void LoadSplashScreen()
		{
			new ContentManager(this.main.Services);
		}

		public void LoadGameContent()
		{
			this.cursor_texture = this.content.Load<Texture2D>(".\\Cursor");
			this.LoadFonts();
		}

		public void LoadFonts()
		{
			this.DEFAULT_FONT = this.content.Load<SpriteFont>(".\\fonts\\Arial");
			this.LOCALIZED_FONT_ARIAL = this.content.Load<SpriteFont>(".\\fonts\\ArialLocalizedFont");
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
