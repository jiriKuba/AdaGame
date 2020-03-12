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
    public class BackgroundColorer : DrawableGameComponent
    {
        private GameStarter _game;
        private Color _backgroundColor;
        private readonly Color _backgroundInitColor;

        public BackgroundColorer(GameStarter game)
            : base(game)
        {
            _game = game;
            _backgroundInitColor = new Color(11, 11, 11);
        }

        public override void Initialize()
        {
            _backgroundColor = _backgroundInitColor;
            base.Initialize();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void LoadContent()
        {
            var fireworkEmitter = _game.Components.OfType<FireworkEmitter>().FirstOrDefault();
            var sparkEmitter = _game.Components.OfType<SparkEmitter>().FirstOrDefault();

            fireworkEmitter.IsTriggered += emitter_IsTriggered;
            sparkEmitter.IsTriggered += emitter_IsTriggered;
            base.LoadContent();
        }        

        public override void Update(GameTime gameTime)
        {
            if (_backgroundColor != _backgroundInitColor && ((int)gameTime.TotalGameTime.TotalSeconds % 3) == 0)
            {
                _backgroundColor = new Color(_backgroundColor.R - 1, _backgroundColor.G - 1, _backgroundColor.B - 1);
            }

            base.Update(gameTime);            
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);
            base.Draw(gameTime);
        }

        private void emitter_IsTriggered(object sender, EventArgs e)
        {
            _backgroundColor = new Color(44, 44, 44);
        }
    }
}
