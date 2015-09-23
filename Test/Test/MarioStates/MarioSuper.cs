using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class MarioSuper : MarioPowerupState
    {
        public MarioSuper(Mario mario)
            : base(mario)
        {
        }
        public override int Factor()
        {
            return 10;
        }
        public  override void TakeDamage() {
            mario.StandardCommand();
        }

    }
    }

