/*
    This state is an adapted version of the one provided by Nathan Bean in the Game Architecture Tutorial to suit this game
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace ChickenFarm
{
    /// <summary>
    /// Defines an object that can create a screen when given its type.
    /// </summary>
    public interface IScreenFactory
    {
        GameScreen CreateScreen(Type screenType);
    }
}
