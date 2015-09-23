
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public abstract class MarioActionState
    {

        protected Mario mario;

        protected MarioActionState(Mario mario)
        {
            this.mario = mario;
        }
        public virtual void UpCommand() { }
        public virtual void UpLeftCommand() { }

        public virtual void DownCommand() { }
        public virtual void LeftCommand() { }
        public virtual void RightCommand() { }
        public virtual void FireballCommand() { }

        public virtual void Update(GraphicsDeviceManager graphics, float elapsed)
        {

        }


        public virtual int Factor()
        {
            return 1;
        }
        public virtual void TakeDamage(){}

    }
}
