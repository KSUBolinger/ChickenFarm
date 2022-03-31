using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChickenFarm
{
    /// <summary>
    /// class that handles the egg sprites
    /// </summary>
    public class EggSprite
    {
        //attributes that contribute to the creation and updating of the sprites
        private Vector2 position;
        private Texture2D texture;
        private BoundingCircle bounds;

        /// <summary>
        /// boolen representing if the egg has been collected 
        /// </summary>
        public bool Collected { get; set; } = false;

        /// <summary>
        /// public accessor for the egg sprite's bounds
        /// </summary>
        public BoundingCircle Bounds => bounds;

        public EggSprite(Vector2 position)
        {
            this.position = position;
            this.bounds = new BoundingCircle(position - new Vector2(-8, -8), 8);
        }

        /// <summary>
        /// loads the content for an egg sprite
        /// </summary>
        /// <param name="content">contentmanager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Egg");
        }

        /// <summary>
        /// draws the egg sprite 
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="spriteBatch">the spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Collected) return;

            var source = new Rectangle(0, 0, 16, 16);
            spriteBatch.Draw(texture, position, source, Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
        }
    }
}
