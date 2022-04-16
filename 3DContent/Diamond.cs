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
            vertices = new VertexPositionColor[15];
            #region verticies 0-11: x - y face of diamond
            //vertex 0 left
            vertices[0].Position = new Vector3(-1, 0, 0);
            vertices[0].Color = Color.LightGreen;

            //vertex 1 center
            vertices[1].Position = new Vector3(0, 0, 0);
            vertices[1].Color = Color.DarkGreen;

            //vertex 2 bottom
            vertices[2].Position = new Vector3(0, -1, 0);
            vertices[2].Color = Color.LightGreen;

            //vertex 3 left
            vertices[3] = vertices[0];

            //vertex 4 center
            vertices[4] = vertices[1];

            //vertex 5 top
            vertices[5].Position = new Vector3(0, 1, 0);
            vertices[5].Color = Color.LightGreen;

            //vertex 6 center
            vertices[6] = vertices[1];

            //vertex 7 bottom
            vertices[7] = vertices[2];

            //vertex 8 right
            vertices[8].Position = new Vector3(1, 0, 0);
            vertices[8].Color = Color.LightGreen;

            //vertex 9 center
            vertices[9] = vertices[1];

            //vertex 10 top
            vertices[10] = vertices[5];

            //vertex 11 right
            vertices[11] = vertices[8];
            #endregion

            #region verticies 12 - 24 z face of diamond
            //vertex 12 back center
            vertices[12].Position = new Vector3(0, 0, -1);
            vertices[12].Color = Color.LightGreen;

            //vertex 13 center
            vertices[13].Position = new Vector3(0, 0, 0);
            vertices[13].Color = Color.DarkGreen;

            //vertex 14 back bottom
            vertices[14].Position = new Vector3(0, -1, 0);
            vertices[14].Color = Color.LightGreen;

            #endregion
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
                50.0f
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
                5
            );

            game.GraphicsDevice.RasterizerState = oldState;
        }
    }
}
