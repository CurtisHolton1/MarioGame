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
using System.Diagnostics;
using MarioGame;

namespace MarioGame
{

    class MarioJumpingState : MarioActionState
    {
        float time;
        private float gravity;
        public MarioJumpingState(Mario mario)
            : base(mario)
        {
            gravity = mario.Gravity;
        }

        public override int Factor()
        {
            return 2;
        }
        public override void UpCommand()
        {

        }
        public override void DownCommand()
        {
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
                mario.MarioAccel = new Vector2(-7, 0);
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
                mario.MarioAccel = new Vector2(7, 0);
            }
        }

        public override void Update(GraphicsDeviceManager graphics, float elapsed)
        {
            gravity = mario.Gravity;
            mario.MarioAccel = new Vector2(0, gravity);
            time += elapsed;
            //if (mario.MarioVel.Y >= 0 && time > 50)
            //{
            //    time = 0;
            //    mario.MarioAccel = new Vector2(0, -5f);
            //}
            if (mario.MarioVel.Y > 0)
            {
                mario.ToFall();
            }
        }


    }
}
