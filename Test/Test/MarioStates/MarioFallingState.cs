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
    class MarioFallingState: MarioActionState
    {
         public MarioFallingState(Mario mario)
            : base(mario)
        {

        }

        public override int Factor()
        {
            return 2;
        }

      public override void UpCommand(){
      }
      public override void DownCommand()
      {
          //mario.ToIdle();
      }

      public override void LeftCommand()
      {
          if (mario.Direction == 1)
          {
              //mario.ToIdle();
              mario.Direction = 0;
          }
          else
          {
              mario.MarioAccel = new Vector2(-7, 5);
          }
      }
      public override void RightCommand()
      {
          if (mario.Direction == 0)
          {
              //mario.ToIdle();
              mario.Direction = 1;
          }
          else
          {
              mario.MarioAccel = new Vector2(7, 5);
          }
      }
      public override void Update(GraphicsDeviceManager graphics, float elapsed)
      {
          //mario.MarioVel_X = 0;

          if (mario.MarioVel_Y >= 0 && mario.MarioVel_Y <= 0.05)
          {
              mario.ToIdle();
          }
          if (mario.IsGrounded)
              mario.ToIdle();
        
      }
     
    }
}
