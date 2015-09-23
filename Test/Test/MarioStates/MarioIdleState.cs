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
    class MarioIdleState : MarioActionState
    {
    
        public MarioIdleState(Mario mario)
            :base(mario)
        { }
      
      
        public override void UpCommand()
        {
            
            mario.ToJump();
        }
        public override void DownCommand()
        {
           
            if (mario.PowerupState.Factor() >= 10 && mario.PowerupState.Factor() < 50)
            {
                mario.ToCrouch();
            }
       
        }
        public override void LeftCommand()
        {
            if (mario.Direction == 0)
                mario.ToWalk();
            else
            {
                mario.Direction = 0;              
            }
        }
        public override void RightCommand()
        {
            if(mario.Direction==1)
            mario.ToWalk();
            else
            {
                mario.Direction = 1;
            }
        }
       
        public override void Update(GraphicsDeviceManager graphics, float elapsed)
        {
          
           
        }
    }
}
