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
    public class GameplayScreen2 : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private SnakeSprite[] snakes;
        private SnakeSprite2[] snakes2;
        private ChickenSprite chicken;
        private EggSprite[] eggs;
        private SpriteFont bangers;
        private int eggsLeft;
        private Texture2D backgroundTexture;
        private SoundEffect eggCollected;
        private SoundEffect collision;

        private bool _screenShake;
        private float _shakeLength;

        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }


        SuccessfulCaptureParticle _sucessCapture;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        public GameplayScreen2()
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

            _sucessCapture = new SuccessfulCaptureParticle(ScreenManager.Game, 20);
            ScreenManager.Game.Components.Add(_sucessCapture);

            _gameFont = _content.Load<SpriteFont>("bangers");

            backgroundTexture = _content.Load<Texture2D>("Plains");
            eggCollected = _content.Load<SoundEffect>("EggPickup");
            collision = _content.Load<SoundEffect>("Collision");


            System.Random randPosition = new System.Random();

            chicken = new ChickenSprite();
            chicken.LoadContent(_content);

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
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 31, 0);
            }

            if (IsActive)
            {
                chicken.Update(gameTime);
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


            spriteBatch.DrawString(_gameFont, $"Eggs Left: {eggsLeft} ", new Vector2(15, 35), Color.Black);
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
