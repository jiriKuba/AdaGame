using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaGame
{
    public class MouseHandler : DrawableGameComponent
    {
        private GameStarter _game;
        private SparkEmitter _sparkEmitter;

        public MouseHandler(GameStarter game)
            : base(game)
        {
            _game = game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void LoadContent()
        {
            _sparkEmitter = _game.Components.OfType<SparkEmitter>().FirstOrDefault();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();            

            // mouse trigger
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                var p = _game.Camera.ScreenToWorld(mouseState.X, mouseState.Y);
                _sparkEmitter.Trigger(p.X, p.Y);
            }

            base.Update(gameTime);
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
