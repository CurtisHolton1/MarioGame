using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class MarioStandard: MarioPowerupState
    {
          public MarioStandard(Mario mario)
            : base(mario)
        {
            
        }
          public override int Factor()
          {
              return 0;
          }

          public override void TakeDamage()
          {
              mario.toDead();
          }
    }
}
