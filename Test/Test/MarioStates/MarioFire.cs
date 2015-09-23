using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class MarioFire : MarioPowerupState
    {
        public MarioFire(Mario mario)
            : base(mario)
        {

        }
        public override int Factor()
        {
            return 20;
        }
        public override  void TakeDamage() { 
        mario.SuperCommand();
        }
        
    }
    }

