using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class MarioDead : MarioActionState
    {
        public MarioDead(Mario mario)
            : base(mario)
        {

        }
        public override int Factor()
        {
            return 50;
        }


        public override void Update(GraphicsDeviceManager graphics, float elapsed)
        {
            //if (collisionBox.position != ) {
            mario.MarioVel_X = 0;
            mario.MarioVel_Y += 5*elapsed/100;
            //}

        }
    }
}
