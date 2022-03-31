/*
    This screen is an adapted version of the one provided by Nathan Bean in the Game Architecture Tutorial to suit this game
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace ChickenFarm
{
    public class ScreenFactory : IScreenFactory
    {
        public GameScreen CreateScreen(Type screenType)
        {
            return Activator.CreateInstance(screenType) as GameScreen;
        }
    }
}
