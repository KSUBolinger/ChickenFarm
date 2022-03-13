using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ChickenFarm
{
    public class BoundingCircle
    {
        /// <summary>
        /// Vector2 representing the center of the circle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// float representing the radius of the circle
        /// </summary>
        public float Radius;

        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// tests for a collision between this another bounding circle
        /// </summary>
        /// <param name="other">the other boundig circle</param>
        /// <returns>true for collision, false otherwise</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}
