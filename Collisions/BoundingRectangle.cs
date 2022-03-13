using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ChickenFarm
{
    public class BoundingRectangle
    {
        //variables that represent the top left corner of the bounding rectangle 
        public float X;
        public float Y;
        //variables that repersent the width and height of the bounding rectangle from the top left corner
        public float Width;
        public float Height;

        //a different way to represent the previous values for a web platform 
        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;


        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }


        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// methond that handles when the bounding rectangle collides with another bound rectangle
        /// </summary>
        /// <param name="other">the other bounding rectangle </param>
        /// <returns>a true if collision, false if not</returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /// <summary>
        /// method that handles when the bound rectangle collides with a bounding circle
        /// </summary>
        /// <param name="other">the bounding cirlce</param>
        /// <returns>true if collision detected, false if not</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(other, this);
        }
    }
}
