using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChickenFarm
{
    public enum ChickenDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
        Idle = 4,
    }

    public class ChickenSprite
    {
        private KeyboardState keyboardState;
        private Texture2D texture;
        private bool inMotion;
        private Vector2 position = new Vector2(100, 100);
        private ChickenDirection chickenDirection;
        private InputManager inputManager = new InputManager();
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(200 - 16, 200 - 16), 32, 32);
        public Color color;

        public BoundingRectangle Bounds => bounds;
    }
}
