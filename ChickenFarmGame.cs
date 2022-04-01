using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ChickenFarm
{
    public class ChickenFarmGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ChickenSprite chicken;
        private Texture2D backgroundTexture;
        private Tilemap tilemap;
        
        private SpriteFont bangers;

        private SoundEffect eggCollected;
        private SoundEffect collision;
        private Song backgroudMusic;

        private readonly ScreenManager screenManager;

        public ChickenFarmGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            IntialScreens();
        }

        private void IntialScreens()
        {
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            backgroundTexture = Content.Load<Texture2D>("Plains");
            bangers = Content.Load<SpriteFont>("bangers");
            eggCollected = Content.Load<SoundEffect>("EggPickup");
            collision = Content.Load<SoundEffect>("Collision");
            backgroudMusic = Content.Load<Song>("BackgroundCountry");
            //tilemap.LoadContent(Content);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroudMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
