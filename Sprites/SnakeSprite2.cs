using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace ChickenFarm
{
    public enum Snake2Direction
    {
        Up = 0,
        Down = 1,
    }
    public class SnakeSprite2
    {
        private Texture2D texture;
        private double directionTimer;
        private double animationTimer;
        private short animationFrame;
        private BoundingRectangle bounds;
        private Vector2 position;
        private Snake2Direction snake2Direction;


        public BoundingRectangle Bounds => bounds;


        public SnakeSprite2(Vector2 position, Snake2Direction direction)
        {
            this.snake2Direction = direction;
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
                if(snake2Direction == Snake2Direction.Up)
                {
                    snake2Direction = Snake2Direction.Down;
                }
                else
                {
                    snake2Direction = Snake2Direction.Up;
                }
                directionTimer -= 2.0;
            }

            if(snake2Direction == Snake2Direction.Up)
            {
                position += new Vector2(0, -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            spriteBatch.Draw(texture, position, source, Color.Blue, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0);
        }
    }
}
