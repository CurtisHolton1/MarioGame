
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    public abstract class MarioPowerupState
    {
        public Mario mario;
        protected MarioPowerupState(Mario mario)
        {
            this.mario = mario;
        }
        public abstract int Factor();

        public virtual void TakeDamage() { }

    }
    }

