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
    public class KeyboardHandler : DrawableGameComponent
    {
        private GameStarter _game;
        private FireworkEmitter _fireworkEmitter;
        private Vector2? _randomVector;
        private bool _fullscreenToggled;

        public KeyboardHandler(GameStarter game)
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
            _fireworkEmitter = _game.Components.OfType<FireworkEmitter>().FirstOrDefault();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            // end game
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                _game.Exit();
            }

            // set fullscreen
            if ((keyboardState.IsKeyDown(Keys.LeftAlt) || keyboardState.IsKeyDown(Keys.RightAlt))
                && keyboardState.IsKeyDown(Keys.Enter) && !_fullscreenToggled)
            {
                _fullscreenToggled = true;
                _game.ToggleFullscreen();
            }

            if (keyboardState.GetPressedKeys().Length > 0)
            {
                // fire firework
                if (_randomVector == null)
                {
                    var r = new Random();
                    int randomX = r.Next(0, (int)_game.Camera.BoundingRectangle.Width);
                    int randomY = r.Next(0, (int)_game.Camera.BoundingRectangle.Height);
                    _randomVector = new Vector2(randomX, randomY);
                }
                _fireworkEmitter.Trigger(_randomVector.Value);                
            }
            else
            {
                _randomVector = null;
                _fullscreenToggled = false;
            }

            base.Update(gameTime);
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
