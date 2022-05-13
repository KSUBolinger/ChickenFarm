using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChickenFarm
{
    public enum SnakeDirection_3
    {
        Right = 0,
        Left = 1,
        Up = 2,
        Down = 3
    }
    public class SnakeSprite3
    {
        private Texture2D texture;
        private double directionTimer;
        private double animationTimer;
        private short animationFrame;
        private bool flipped;
        private BoundingRectangle bounds;
        private Vector2 position;
        private SnakeDirection_3 snakeDirection3;

        public BoundingRectangle Bounds => bounds;

        public SnakeSprite3(Vector2 position, SnakeDirection_3 direction)
        {
            this.snakeDirection3 = direction;
            this.position = position;
            this.bounds = new BoundingRectangle(position, 20, 29);
        }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Snake");
        }


        public void Update(GameTime gameTime)
        {
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if(directionTimer > 2.0)
            {
                if(snakeDirection3 == SnakeDirection_3.Left)
                {
                    snakeDirection3 = SnakeDirection_3.Up;
                    flipped = true;
                }
                else if(snakeDirection3 == SnakeDirection_3.Up)
                {
                    snakeDirection3 = SnakeDirection_3.Right;
                    flipped = false;
                }
                else if(snakeDirection3 == SnakeDirection_3.Right)
                {
                    snakeDirection3 = SnakeDirection_3.Down;
                    flipped = false;
                }
                else
                {
                    snakeDirection3 = SnakeDirection_3.Left;
                    flipped = true;
                }
                directionTimer -= 2.0;
            }

            if(snakeDirection3 == SnakeDirection_3.Left)
            {
                position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if(snakeDirection3 == SnakeDirection_3.Up)
            {
                position += new Vector2(0, -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if(snakeDirection3 == SnakeDirection_3.Right)
            {
                position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                position += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            bounds.X = position.X;
            bounds.Y = position.Y;
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(animationTimer > 0.5)
            {
                animationFrame++;
                if(animationFrame > 1)
                {
                    animationFrame = 0;
                }
                animationTimer -= 0.5;
            }
            var source = new Rectangle(animationFrame * 40, 0, 40, 29);
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, position, source, Color.Red, 0, new Vector2(0, 0), 1.25f, spriteEffects, 0);
        }
    }
}
