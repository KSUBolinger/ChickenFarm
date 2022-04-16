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

            for(int i=0; i < vertices.Length; i++)
            {
                if((i % 3) == 0)
                {
                    vertices[i].Color = Color.DarkGreen;
                }
                else
                {
                    vertices[i].Color = Color.LightGreen;
                }
            }

            #region top vertix
            //top verticies
            vertices[0].Position = new Vector3(0, 1, 0);

            vertices[3] = vertices[0];
            vertices[6] = vertices[0];
            vertices[9] = vertices[0];
            #endregion

            #region bottom vertex
            //bottom verticies
            vertices[12].Position = new Vector3(0, -1, 0);

            vertices[15] = vertices[12];
            vertices[18] = vertices[12];
            vertices[21] = vertices[12];
            #endregion

            #region front face
            //front verticies
            vertices[1].Position = new Vector3(-1, 0, 1);
            vertices[14] = vertices[1];

            vertices[2].Position = new Vector3(1, 0, 1);
            vertices[13] = vertices[2];
            #endregion

            #region right face
            //right verticies 
            vertices[4].Position = vertices[2].Position;
            vertices[17] = vertices[4];

            vertices[5].Position = new Vector3(1, 0, -1);
            vertices[16] = vertices[5];
            #endregion

            #region back face
            //back verticies
            vertices[7].Position = vertices[5].Position;
            vertices[20] = vertices[7];

            vertices[8].Position = new Vector3(-1, 0, -1);
            vertices[19] = vertices[8];
            #endregion

            #region left face
            //left verticies
            vertices[10].Position = vertices[8].Position;
            vertices[23] = vertices[10];

            vertices[11].Position = vertices[1].Position;
            vertices[22] = vertices[11];
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
            /*
            RasterizerState oldState = game.GraphicsDevice.RasterizerState;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            game.GraphicsDevice.RasterizerState = rasterizerState;

            effect.CurrentTechnique.Passes[0].Apply();

            game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                vertices,
                0,
                4
            );

            game.GraphicsDevice.RasterizerState = oldState;
            */
            effect.CurrentTechnique.Passes[0].Apply();
            game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>
            (
                PrimitiveType.TriangleList,
                vertices,
                0,
                8
            );
        }
    }
}
