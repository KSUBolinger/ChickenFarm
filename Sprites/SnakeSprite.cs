using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ChickenFarm
{
    public enum SnakeDirection
    {
        Right = 0,
        Left = 1,
    }

    public class SnakeSprite
    {
        //various attributes that make up the snake sprites
        private Texture2D texture;
        private double directionTimer;
        private double animationTimer;
        private short animationFrame;
        private bool flipped;
        private BoundingRectangle bounds;
        private Vector2 position;
        private SnakeDirection snakeDirection;


        /// <summary>
        /// public accesor for the bounds of a snake sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        public SnakeSprite(Vector2 position, SnakeDirection direction)
        {
            this.snakeDirection = direction;
            this.position = position;
            this.bounds = new BoundingRectangle(position, 20, 29);
        }

        /// <summary>
        /// loads the content for the snake sprite
        /// </summary>
        /// <param name="content">the contentmanager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Snake");

        }

        /// <summary>
        /// updates the snake sprites animation
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime)
        {
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //switch the direction every two seconds
            if (directionTimer > 2.0)
            {
                if (snakeDirection == SnakeDirection.Left)
                {
                    snakeDirection = SnakeDirection.Right;
                    flipped = false;
                }
                else
                {
                    snakeDirection = SnakeDirection.Left;
                    flipped = true;
                }
                directionTimer -= 2.0;
            }

            if (snakeDirection == SnakeDirection.Left)
            {
                position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }


            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        /// <summary>
        /// draws the animated snake sprite 
        /// </summary>
        /// <param name="gametime">the game time</param>
        /// <param name="spriteBatch">the spritebatch to render with</param>
        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            animationTimer += gametime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > 0.5)
            {
                animationFrame++;
                if (animationFrame > 1)
                {
                    animationFrame = 0;
                }
                animationTimer -= 0.5;
            }
            var source = new Rectangle(animationFrame * 40, (int)snakeDirection * 29, 40, 29);
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, position, source, Color.White, 0, new Vector2(0, 0), 1.25f, spriteEffects, 0);
        }
    }
}
