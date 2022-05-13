using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace ChickenFarm
{
    public class GameplayScreen3 : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private SnakeSprite3[] squareSnakes;

        private SnakeSprite snake;
        private SnakeSprite2 snake2;
        private ChickenSprite chicken;
        private EggSprite[] eggs;
        private SpriteFont bangers;
        private int eggsLeft;
        private Texture2D backgroundTexture;
        private SoundEffect eggCollected;
        private SoundEffect collision;

        private bool _screenShake;
        private float _shakeLength;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        SuccessfulCaptureParticle _sucessCapture;

        public GameplayScreen3()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape },
                true
            );
        }

        public override void Activate()
        {
            if(_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            #region intialize values
            _sucessCapture = new SuccessfulCaptureParticle(ScreenManager.Game, 20);
            ScreenManager.Game.Components.Add(_sucessCapture);

            _gameFont = _content.Load<SpriteFont>("bangers");
            backgroundTexture = _content.Load<Texture2D>("Plains");
            eggCollected = _content.Load<SoundEffect>("EggPickup");
            collision = _content.Load<SoundEffect>("Collision");

            System.Random randPosition = new System.Random();

            chicken = new ChickenSprite();

            squareSnakes = new SnakeSprite3[]
            {
                new SnakeSprite3((new Vector2(250, 350)), SnakeDirection_3.Left),
                new SnakeSprite3((new Vector2(450, 150)), SnakeDirection_3.Down)
            };

            snake = new SnakeSprite((new Vector2(375, 290)), SnakeDirection.Left);
            snake2 = new SnakeSprite2((new Vector2(175, 350)), Snake2Direction.Up);

            Vector2 eggPosition;
            eggs = new EggSprite[5];
            for(int i = 0; i < eggs.Length; i++)
            {
                eggPosition = new Vector2((float)randPosition.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)randPosition.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height);

                if (eggPosition.X < 25)
                {
                    eggPosition.X += 40;
                }
                if (eggPosition.Y < 30)
                {
                    eggPosition.Y += 60;
                }

                eggs[i] = new EggSprite(eggPosition);
            }
            eggsLeft = eggs.Length;
            #endregion

            #region Load Content
            chicken.LoadContent(_content);
            foreach(var snake in squareSnakes)
            {
                snake.LoadContent(_content);
            }

            snake.LoadContent(_content);
            snake2.LoadContent(_content);
            
            foreach(var egg in eggs)
            {
                egg.LoadContent(_content);
            }
            #endregion

            Thread.Sleep(1000);
            ScreenManager.Game.ResetElapsedTime();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            
            _content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (coveredByOtherScreen)
            {
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            }
            else
            {
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);
            }

            if (IsActive)
            {
                #region update sprites 
                chicken.Update(gameTime);

                foreach(var snake in squareSnakes)
                {
                    snake.Update(gameTime);
                    if (chicken.Bounds.CollidesWith(snake.Bounds))
                    {
                        _screenShake = true;
                        _shakeLength = 0;

                        chicken.Reset();
                        foreach (var egg in eggs)
                        {
                            if (egg.Collected)
                            {
                                egg.Collected = false;
                                eggsLeft++;
                            }
                        }
                    }
                }

                snake.Update(gameTime);
                if (chicken.Bounds.CollidesWith(snake.Bounds))
                {
                    _screenShake = true;
                    _shakeLength = 0;

                    chicken.Reset();
                    foreach (var egg in eggs)
                    {
                        if (egg.Collected)
                        {
                            egg.Collected = false;
                            eggsLeft++;
                        }
                    }
                }

                snake2.Update(gameTime);
                if (chicken.Bounds.CollidesWith(snake.Bounds))
                {
                    _screenShake = true;
                    _shakeLength = 0;

                    chicken.Reset();
                    foreach(var egg in eggs)
                    {
                        if (egg.Collected)
                        {
                            egg.Collected = false;
                            eggsLeft++;
                        }
                    }
                }

                foreach (var egg in eggs)
                {
                    if (!egg.Collected && egg.Bounds.CollidesWith(chicken.Bounds))
                    {
                        egg.Collected = true;
                        eggsLeft--;
                        _sucessCapture.ShowSucessfulCapture(egg.Bounds.Center);
                        eggCollected.Play();
                    }
                }

                #endregion

                if(eggsLeft == 0)
                {
                    //eggsLeft = 4;
                    LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new BackgroundScreen(), new GameplayScreen4());
                }
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if(input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            int playerIndex = (int)ControllingPlayer.Value;
            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

            bool gamePadDisconnected = !gamePadState.IsConnected && input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if(_pauseAction.Occurred(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
        }


        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            var spriteBatch = ScreenManager.SpriteBatch;

            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix shakeTransform = Matrix.Identity;
            if (_screenShake)
            {
                _shakeLength += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Matrix shakeTranslation = Matrix.CreateTranslation(10 * MathF.Sin(_shakeLength), 10 * MathF.Cos(_shakeLength), 0);
                shakeTransform = shakeTranslation;
                if (_shakeLength > 250) _screenShake = false;
            }

            spriteBatch.Begin(transformMatrix: shakeTransform);

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

            chicken.Draw(gameTime, spriteBatch);

            foreach(var snake in squareSnakes)
            {
                snake.Draw(gameTime, spriteBatch);
            }

            snake.Draw(gameTime, spriteBatch);
            snake2.Draw(gameTime, spriteBatch);

            foreach(var egg in eggs)
            {
                egg.Draw(gameTime, spriteBatch);
            }

            spriteBatch.DrawString(_gameFont, $"Eggs Left: {eggsLeft}", new Vector2(15, 35), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);

            if(TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);
                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}
