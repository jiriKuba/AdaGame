using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AdaGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameStarter : Game
    {
        public SpriteBatch SpriteBatch { get; private set; }
        public OrthographicCamera Camera { get; private set; }
        public readonly GraphicsDeviceManager Graphics;
        private BoxingViewportAdapter _boxingViewport;
        public GameStarter()
        {
            Graphics = new GraphicsDeviceManager(this);
            Window.Title = "Ada game";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ToggleFullscreen()
        {
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; 
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Graphics.ToggleFullScreen();
            Graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Components.Add(new BackgroundColorer(this));
            Components.Add(new FireworkEmitter(this));
            Components.Add(new SparkEmitter(this));
            Components.Add(new MouseHandler(this));
            Components.Add(new KeyboardHandler(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            _boxingViewport = new BoxingViewportAdapter(Window, GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);
            Camera = new OrthographicCamera(_boxingViewport);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // any content not loaded with the content manager should be disposed
            _boxingViewport?.Dispose();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
