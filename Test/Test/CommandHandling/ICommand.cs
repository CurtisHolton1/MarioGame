using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    interface ICommand
    {
        bool Toggled { get; }
        void Execute();
    }
}
