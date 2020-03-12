using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public class FireworkEmitter : DrawableGameComponent
    {
        private GameStarter _game;
        private ParticleEffect _particleEffect;
        private Texture2D _particleTexture;
        public event EventHandler<EventArgs> IsTriggered;

        public FireworkEmitter(GameStarter game)
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
            _particleTexture.Dispose();
            _particleEffect.Dispose();
            base.UnloadContent();
        }

        protected override void LoadContent()
        {
            _particleTexture = new Texture2D(GraphicsDevice, 1, 1);
            _particleTexture.SetData(new[] { Color.White });

            ParticleInit(new TextureRegion2D(_particleTexture));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _particleEffect.Update(deltaTime);

            base.Update(gameTime);
            
        }

        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin(blendState: BlendState.AlphaBlend, transformMatrix: _game.Camera.GetViewMatrix());
            _game.SpriteBatch.Draw(_particleEffect);
            _game.SpriteBatch.End();
            base.Draw(gameTime);
        }

        private void ParticleInit(TextureRegion2D textureRegion)
        {
            _particleEffect = new ParticleEffect(autoTrigger: false)
            {
                Position = new Vector2(400, 240),
                Emitters = new List<ParticleEmitter>
                {
                    new ParticleEmitter(textureRegion, 1000, TimeSpan.FromSeconds(2),
                        Profile.Point())
                    {
                        AutoTrigger = false,
                        Parameters = new ParticleReleaseParameters
                        {
                            Speed = new Range<float>(0f, 100f),
                            Quantity = 10,
                            Scale = new Range<float>(6f),
                            Opacity = 1f,
                            Color = new Range<HslColor>(new HslColor(50f, 0.8f, 0.5f)),
                            Mass = new Range<float>(8, 12),
                        },
                        Modifiers =
                        {
                            new AgeModifier
                            {
                                Interpolators =
                                {
                                    new ColorInterpolator
                                    {
                                        StartValue = HslColor.FromRgb(Color.Aqua),//new HslColor(0.33f, 0.5f, 0.5f), 
                                        EndValue = new HslColor(0.5f, 0.9f, 1.0f)
                                    }
                                }
                            },
                            //new RotationModifier {RotationRate = -2.1f},
                            //new RectangleContainerModifier {Width = 800, Height = 480},
                            //new LinearGravityModifier {Direction = Vector2.UnitY, Strength = 30f}
                        }
                    }
                }
            };

            //_particleEffect = new ParticleEffect(autoTrigger: false)
            //{
            //    Position = new Vector2(400, 240),
            //    Emitters = new List<ParticleEmitter>
            //    {
            //        new ParticleEmitter(textureRegion, 500, TimeSpan.FromSeconds(2.5),
            //            Profile.Ring(150f, Profile.CircleRadiation.In))
            //        {
            //            Parameters = new ParticleReleaseParameters
            //            {
            //                Speed = new Range<float>(0f, 50f),
            //                Quantity = 3,
            //                Rotation = new Range<float>(-1f, 1f),
            //                Scale = new Range<float>(3.0f, 4.0f)
            //            },
            //            Modifiers =
            //            {
            //                new AgeModifier
            //                {
            //                    Interpolators =
            //                    {
            //                        new ColorInterpolator
            //                        {
            //                            StartValue = new HslColor(0.33f, 0.5f, 0.5f),
            //                            EndValue = new HslColor(0.5f, 0.9f, 1.0f)
            //                        }
            //                    }
            //                },
            //                new RotationModifier {RotationRate = -2.1f},
            //                new RectangleContainerModifier {Width = 800, Height = 480},
            //                new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 30f}
            //            }
            //        }
            //    }
            //};
        }

        public void Trigger(float x, float y)
        {
            _particleEffect.Trigger(new Vector2(x, y));
            IsTriggered?.Invoke(this, EventArgs.Empty);
        }

        public void Trigger(Vector2 position)
        {
            _particleEffect.Trigger(position);
            IsTriggered?.Invoke(this, EventArgs.Empty);
        }
    }
}
