using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChickenFarm
{
    public class Diamond
    {
        BasicEffect effect;

        Game game;

        VertexPositionColor[] vertices;


        public Diamond(Game game)
        {
            this.game = game;
            InitializeVerticies();
            InitializeEffect();
        }

        void InitializeVerticies()
        {
            vertices = new VertexPositionColor[3];

            //vertex 0
            vertices[0].Position = new Vector3(0, 1, 0);
            vertices[0].Color = Color.Red;

            //vertex 1
            vertices[1].Position = new Vector3(1, 1, 0);
            vertices[1].Color = Color.Red;

            //vertex 2
            vertices[2].Position = new Vector3(1, 0, 0);
            vertices[2].Color = Color.Red;
        }

        void InitializeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(
                new Vector3(0, 0, 4),
                new Vector3(0, 0, 0),
                Vector3.Up
            );
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                game.GraphicsDevice.Viewport.AspectRatio,
                0.1f,
                100.0f
            );
            effect.VertexColorEnabled = true;
        }


        public void Update(GameTime gameTime)
        {
            float angle = (float)gameTime.TotalGameTime.TotalSeconds;
            effect.World = Matrix.CreateRotationY(angle);
        }

        
        public void Draw()
        {
            RasterizerState oldState = game.GraphicsDevice.RasterizerState;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            game.GraphicsDevice.RasterizerState = rasterizerState;

            effect.CurrentTechnique.Passes[0].Apply();

            game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                vertices,
                0,
                1
            );

            game.GraphicsDevice.RasterizerState = oldState;
        }
    }
}
