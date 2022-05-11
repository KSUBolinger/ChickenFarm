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
        private bool flipped;

    }
}
