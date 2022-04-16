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
            vertices = new VertexPositionColor[24];
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
            vertices[13] = vertices[1];

            //vertex 14 bottom 
            vertices[14] = vertices[2];

            //vertex 15 back center
            vertices[15] = vertices[12];

            //vertex 16 center
            vertices[16] = vertices[1];

            //vertex 17 top 
            vertices[17] = vertices[5];

            //vertex 18 front center
            vertices[18].Position = new Vector3(0, 0, 1);
            vertices[18].Color = Color.LightGreen;

            //vertex 19 center
            vertices[19] = vertices[1];

            //vertex 20 top 
            vertices[20] = vertices[5];

            //vertex 21 front center
            vertices[21] = vertices[18];

            //vertex 22 center
            vertices[22] = vertices[1];

            //vertex 23 bottom center
            vertices[23] = vertices[14];
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
            effect.View = Matrix.CreateRotationY(angle) * Matrix.CreateLookAt(
                new Vector3(0,0,-10),
                Vector3.Zero,
                Vector3.Up
            );
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
                8
            );

            game.GraphicsDevice.RasterizerState = oldState;
        }
    }
}
