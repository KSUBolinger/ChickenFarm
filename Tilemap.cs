/*
 * This tilemap class was based on the file created in the Tilemap Exercise given by Nathan Bean
 */
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace ChickenFarm
{
    public class Tilemap
    {
        /// <summary>
        /// set of variables representing the tile map and tiles
        /// </summary>
        int tileWidth;
        int tileHeight;
        int mapWidth;
        int mapHeight;

        /// <summary>
        /// Texture representing the tilemap image
        /// </summary>
        Texture2D texture;


        Rectangle[] tiles;

        int[] map;

        string _filename;

        public Tilemap(string filename)
        {
            _filename = filename;
        }

        /// <summary>
        /// loads the content for the tilemap
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText(Path.Join(content.RootDirectory, _filename));
            var lines = data.Split('\n');


            var tilesetFilename = lines[0].Trim();
            texture = content.Load<Texture2D>(tilesetFilename);

            var secondLine = lines[1].Split(',');
            tileWidth = int.Parse(secondLine[0]);
            tileHeight = int.Parse(secondLine[1]);

            int tilesetColumns = texture.Width / tileWidth;
            int tilesetRows = texture.Height / tileHeight;
            tiles = new Rectangle[tilesetColumns * tilesetRows];

            for(int y = 0; y < tilesetColumns; y++)
            {
                for(int x = 0; x < tilesetColumns; x++)
                {
                    int index = y * tilesetColumns + x;
                    tiles[index] = new Rectangle(
                        x * tileWidth,
                        y * tileHeight,
                        tileWidth,
                        tileHeight
                    );
                }
            }

            var thirdLine = lines[2].Split(',');
            mapWidth = int.Parse(thirdLine[0]);
            mapHeight = int.Parse(thirdLine[1]);

            var fourthLine = lines[3].Split(',');
            map = new int[mapWidth * mapHeight];
            for(int i = 0; i < mapWidth * mapHeight; i++)
            {
                map[i] = int.Parse(fourthLine[i]);
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                for(int x = 0; x < mapWidth; x++)
                {
                    int index = map[y * mapWidth + x] - 1;
                    if(index == -1)
                    {
                        continue;
                    }

                    spriteBatch.Draw(
                        texture,
                        new Vector2(
                            x * tileWidth,
                            y * tileHeight
                        ),
                        tiles[index],
                        Color.White
                    );
                }
            }
        }
    }
}
