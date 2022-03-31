using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ChickenFarm
{
    public interface IEmitter
    {
        public Vector2 Position { get; }

        public Vector2 Velocity { get; }
    }
}
