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
    class MarioWalkingState : MarioActionState
    {
        float time;
        public MarioWalkingState(Mario mario)
            : base(mario)
        {
            time = 0;
        }

        public override int Factor()
        {
            return 3;
        }
        public override void UpCommand()
        {
         
            mario.ToJump();
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
                mario.MarioAccel = new Vector2(-20, 0);
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
                mario.MarioAccel = new Vector2(20, 0);
            }
        }

        public override void Update(GraphicsDeviceManager graphics, float elapsed)
        {
            time += elapsed;
            if (Math.Abs(mario.MarioVel.X) > 0.05)
            {
                if (mario.Direction == 1)
                {
                    mario.MarioAccel = new Vector2(-1, 0);
                }
                else
                {
                    mario.MarioAccel = new Vector2(1, 0);
                }

            }
            else if (Math.Abs(mario.MarioVel.X) >= 0 && time > 400)
            {
                time = 0;
                mario.ToIdle();
            }
            
        }
    }
}
