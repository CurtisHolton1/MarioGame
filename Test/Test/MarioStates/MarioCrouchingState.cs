using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MarioGame;

namespace MarioGame
{
    class MarioCrouchingState : MarioActionState
    {

        public MarioCrouchingState(Mario mario)
            : base(mario)
        {

        }

        public override int Factor()
        {
            return 4;
        }

        public override void UpCommand()
        {
            mario.ToIdle();
        }
        public override void DownCommand()
        {
            //if (mario is not touching the floor) {
            mario.ToFall();
            //}
        }
        public override void LeftCommand()
        {
            if (mario.Direction == 1)
                mario.Direction = 0;
        }
        public override void RightCommand()
        {
            if (mario.Direction == 0)
                mario.Direction = 1;
          
        }

        public override void Update(GraphicsDeviceManager graphics, float elapsed)
        {
           
        }

        

    }
}
