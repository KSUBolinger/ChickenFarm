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
    public class GameplayScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private SnakeSprite[] snakes;
        //private SnakeSprite2 uNdSnake;
        private GoofySnake goofySnake;
        private ChickenSprite chicken;
        private EggSprite[] eggs;
        private SpriteFont bangers;
        private int eggsLeft;
        private Texture2D backgroundTexture;
        private Tilemap tilemap;
        private SoundEffect eggCollected;
        private SoundEffect collision;
        Diamond diamond;


        private bool _screenShake;
        private float _shakeLength;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        SuccessfulCaptureParticle _sucessCapture;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);
        }

        // Load graphics content for the game
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _sucessCapture = new SuccessfulCaptureParticle(ScreenManager.Game, 20);
            ScreenManager.Game.Components.Add(_sucessCapture);

            _gameFont = _content.Load<SpriteFont>("bangers");

            //old code for intialize
            backgroundTexture = _content.Load<Texture2D>("Plains");
            eggCollected = _content.Load<SoundEffect>("EggPickup");
            collision = _content.Load<SoundEffect>("Collision");
            tilemap = new Tilemap("map.txt");

            System.Random randPosition = new System.Random();
            // TODO: Add your initialization logic here
            chicken = new ChickenSprite();
            snakes = new SnakeSprite[]
            {
                new SnakeSprite((new Vector2(400, 375)), SnakeDirection.Right),
                new SnakeSprite((new Vector2(325, 100)), SnakeDirection.Right),
                new SnakeSprite((new Vector2(200, 300)), SnakeDirection.Left),
                new SnakeSprite((new Vector2(350, 200)), SnakeDirection.Left)
            };
            goofySnake = new GoofySnake((new Vector2(300, 400)), SnakeDirection.Left);
            //uNdSnake = new SnakeSprite2((new Vector2(100, 100)), Snake2Direction.Down);
            diamond = new Diamond(ScreenManager.Game);

            Vector2 eggPosition;
            eggs = new EggSprite[4];
            for (int i = 0; i < eggs.Length; i++)
            {
                eggPosition = new Vector2((float)randPosition.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)randPosition.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height);
                
                if(eggPosition.X < 25)
                {
                    eggPosition.X += 40;
                }
                if(eggPosition.Y < 40)
                {
                    eggPosition.Y += 60;
                }
                if ((eggPosition.X + 50) >= ScreenManager.GraphicsDevice.Viewport.Width)
                {
                    eggPosition.X -= 100;
                }
                if ((eggPosition.Y + 50) >= ScreenManager.GraphicsDevice.Viewport.Height)
                {
                    eggPosition.Y -= 100;
                }

                eggs[i] = new EggSprite(eggPosition);
            }
            eggsLeft = eggs.Length;



            chicken.LoadContent(_content);
            //uNdSnake.LoadContent(_content);
            foreach (var snake in snakes)
            {
                snake.LoadContent(_content);
            }
            foreach (var egg in eggs)
            {
                egg.LoadContent(_content);
            }
            goofySnake.LoadContent(_content);
            tilemap.LoadContent(_content);
            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
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

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {

                chicken.Update(gameTime);
                goofySnake.Update(gameTime);
                //uNdSnake.Update(gameTime);
                foreach (var snake in snakes)
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
                        collision.Play();
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

                if (eggsLeft == 0)
                {
                    LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new BackgroundScreen(), new GameplayScreen2());
                    
                    /*
                    chicken.Reset();
                    foreach (var egg in eggs)
                    {
                        egg.Collected = false;
                        eggsLeft++;
                    }*/
                }

                diamond.Update(gameTime);
            }
        }

        // Unlike the Update method, this will only be called when the gameplay screen is active.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected && input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (_pauseAction.Occurred(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            var spriteBatch = ScreenManager.SpriteBatch;




            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Matrix shakeTransform = Matrix.Identity;
            if (_screenShake)
            {
                _shakeLength += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                // Matrix shakeRotation = Matrix.CreateRotationZ(MathF.Cos(_shakeTime));
                Matrix shakeTranslation = Matrix.CreateTranslation(10 * MathF.Sin(_shakeLength), 10 * MathF.Cos(_shakeLength), 0);
                shakeTransform = shakeTranslation;
                if (_shakeLength > 250) _screenShake = false;
            }

            spriteBatch.Begin(transformMatrix: shakeTransform);

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            
            chicken.Draw(gameTime, spriteBatch);
            goofySnake.Draw(gameTime, spriteBatch);
            //uNdSnake.Draw(gameTime, spriteBatch);
            foreach (var snake in snakes)
            {
                snake.Draw(gameTime, spriteBatch);
            }

            foreach (var egg in eggs)
            {
                egg.Draw(gameTime, spriteBatch);
            }
            spriteBatch.DrawString(_gameFont, $"Eggs Left: {eggsLeft} ", new Vector2(15, 35), Color.Black);
            spriteBatch.DrawString(_gameFont, $"Use 'ESC' to exit game", new Vector2(15, 5), Color.Black);

            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(65, 415, 0));
            tilemap.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            //diamond.Draw();

            base.Draw(gameTime);


            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}
