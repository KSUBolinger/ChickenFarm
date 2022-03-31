/*
    This screen is an adapted version of the one provided by Nathan Bean in the Game Architecture Tutorial to suit this game
*/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ChickenFarm
{
   public class PlayerIndexEventArgs : EventArgs
   {
        public PlayerIndex PlayerIndex { get; }

        public PlayerIndexEventArgs(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
        }
   }
}
