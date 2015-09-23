using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
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
    /*This class is a Context in the State Pattern. 
     */
    public class Mario
    {
        MarioActionState idle;
        MarioActionState jump;
        MarioActionState walk;
        MarioActionState crouch;
        MarioActionState fall;
        MarioFactory MarioFactory;
        MarioPowerupState standard;
        MarioPowerupState super;
        MarioPowerupState fire;
        MarioActionState dead;
        SoundMachine sounds;
        Game1 game;
        public bool invincible = false;
        float time;
        float elapsed;
        float buffer;
        Scene scene;
        private float gravity;
        public float Gravity
        {
            get { return gravity; }
        }

        private Vector2 marioVec;
        public Vector2 MarioVec
        {
            get { return marioVec; }
            set { marioVec = value; }
        }
        private int points;
        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        private int coins;
        public int Coins
        {
            get { return coins; }
            set { coins = value; }
        }
        private int lives;
        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public float MarioVec_X
        {
            get { return marioVec.X; }
            set { marioVec.X = value; }
        }
        public float MarioVec_Y
        {
            get { return marioVec.Y; }
            set { marioVec.Y = value; }
        }

        private int powerupStateFactor;
        public int PowerupStateFactor
        {
            get { return powerupStateFactor; }
            set { powerupStateFactor = value; }
        }
        int actionStateFactor;
        //Mario keeps track of his current state at all times.
        private MarioActionState currentState;
        public MarioActionState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }
        private MarioSprite currentSprite;

        public MarioSprite CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }

        private MarioPowerupState powerupState;

        public MarioPowerupState PowerupState
        {
            get { return powerupState; }
            set { powerupState = value; }

        }
        private int direction;
        public int Direction{
            get { return direction; }
            set { direction = value; }
        }
        private MarioActionState previousState;
        public MarioActionState PreviousState
        {
            get { return previousState; }
            set { previousState = value; }
        }

        private ISprite previousSprite;
        public ISprite PreviousSprite
        {
            get { return previousSprite; }
            set { previousSprite = value; }
        }

        //private CollisionBox collisionBox;
        public CollisionBox CollisionBox { get { return currentSprite.CollisionBox; } set { currentSprite.CollisionBox = value; } }


        public Mario(Game1 game, Scene scene)
        {
            MarioFactory = new MarioFactory(game);
            currentSprite = (MarioSprite)MarioFactory.MakeProduct(1);
            powerupStateFactor = 0;
            actionStateFactor = 1;
            direction = 1;
            idle = new MarioIdleState(this);
            jump = new MarioJumpingState(this);
            walk = new MarioWalkingState(this);
            crouch = new MarioCrouchingState(this);
            fall = new MarioFallingState(this);
            standard = new MarioStandard(this);
            super = new MarioSuper(this);
            fire = new MarioFire(this);
            dead = new MarioDead(this);
            powerupState = standard;
            currentState = idle;
            previousState = currentState;
            previousSprite = currentSprite;
            this.game = game;
            this.scene = scene;
            gravity = scene.Gravity;

            isGrounded = false;

            points = 0;
            coins = 10;
            lives = 3;
            sounds = new SoundMachine(game);
            
           
        }

       
        public void ToFall()
        {
            previousState = currentState;
            currentState = fall;
            previousSprite = currentSprite;
            this.MarioAccel = new Vector2(0, 5);
        }


        public void ToIdle()
        {
            previousState = currentState;
            currentState = idle;
            previousSprite = currentSprite;
            this.MarioVel = new Vector2(0, 0);
            this.MarioAccel = new Vector2(0, 0);
            
            
        }
        public void ToJump()
        {
            this.MarioVel = new Vector2(marioVel_X, -36);
              
            previousState = currentState;
            currentState = jump;
            previousSprite = currentSprite;
            if (this.powerupState.Factor() >= 10 ) {
                sounds.PlayBigJump();
            }
            else
            {
                sounds.PlaySmallJump(); 
            }
        }

        public void ToWalk()
        {
            this.MarioAccel = new Vector2(marioAccel_X, 0);
          
            previousState = currentState;
            currentState = walk;
            previousSprite = currentSprite;
      
        }
        public void ToCrouch()
        {
            previousState = currentState;
            currentState = crouch;
            previousSprite = currentSprite;
        }
        public void toDead()
        {
            currentState = dead;
            //How do we make time stop before reseting?
             //   if (lives > 1) {                    
             //        Vector2 latestCheck = game.scene.FindLatestCheckpoint();
             //        int life = lives;
             //        game.ResetCommand();//Why isn't the theme music playing?
             //        game.scene.mario.MarioVec = latestCheck;
             //        game.scene.mario.Lives = life;
             //   }
             //else {
             // game.scene.Mute();
             // sounds.PlayGameOver();
             // game.GameoverCommand();
             //   }
            }
            

        private Vector2 marioVel;
        public Vector2 MarioVel
        {
            get { return marioVel; }
            set { marioVel = value; }
        }

        private Vector2 marioAccel;
        public Vector2 MarioAccel
        {
            get { return marioAccel; }
            set { marioAccel = value; }
        }
        private float marioAccel_X;
        public float MarioAccel_X
        {
            get { return marioAccel.X; }
            set { marioAccel.X = value; }
        }
        private float marioAccel_Y;
        public float MarioAccel_Y
        {
            get { return marioAccel.Y; }
            set { marioAccel.Y = value; }
        }
        private float marioVel_X;
        public float MarioVel_X
        {
            get { return marioVel.X; }
            set { marioVel.X = value; }
        }
        private float marioVel_Y;
        public float MarioVel_Y
        {
            get { return marioVel.Y; }
            set { marioVel.Y = value; }
        }
        private bool isGrounded;
        public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
     
         
        public void Update(float elapsed, GraphicsDeviceManager graphics, MarioCollision collision)
        {

            if (currentState == dead)
            {
                if (buffer < 1000)
                {
                    buffer += elapsed;
                }
                else
                {
                    buffer = 0;
                    if (lives > 0)
                    {
                        Vector2 latestCheck = game.scene.FindLatestCheckpoint();
                        int life = lives;
                        game.ResetCommand();//Why isn't the theme music playing?
                        game.scene.mario.MarioVec = latestCheck;
                        game.scene.mario.Lives = life;
                    }
                    else
                    {
                        game.scene.Mute();
                        sounds.PlayGameOver();
                        game.GameoverCommand();
                    }
                }

            }
            else
            {
                gravity = scene.Gravity;
                this.elapsed = elapsed;
                if (time > 1000)
                {
                    invincible = false;
                }
                if (invincible == true)
                {
                    time += elapsed;
                }


                marioVel += marioAccel * collision.CollisionTime / 5;
                marioVel = Vector2.Clamp(marioVel, new Vector2(-20, -40), new Vector2(20, 40));
                marioVec += marioVel * collision.CollisionTime / 5;

            }


                actionStateFactor = currentState.Factor();
                powerupStateFactor = powerupState.Factor();
                currentSprite = (MarioSprite)MarioFactory.MakeProduct(powerupStateFactor + actionStateFactor);
                currentSprite.CollisionBox.Physics(new Vector2(marioVec.X, marioVec.Y - CollisionBox.Height), marioVel, marioAccel);
                currentSprite.Update(elapsed, direction);
                currentState.Update(graphics, elapsed);

                //if (!isGrounded && currentState != jump)
                //{
                //    this.ToFall();
                //}
                Console.WriteLine(MarioVel);
                //////////////////////////////////////
                CheckBorders();
        }

        private void CheckBorders()
        {
            if(this.CollisionBox.Max_Y >= 600 && !(this.currentState is MarioDead)) {
                this.marioVec.Y = 400;
                lives--;
                currentState = dead;
                if (lives > 0)
                {
                    Vector2 latestCheck = game.scene.FindLatestCheckpoint();
                    int life = lives;
                    game.ResetCommand();//Why isn't the theme music playing?
                    game.scene.mario.MarioVec = latestCheck;
                    game.scene.mario.Lives = life;
                }
                else
                {
                    game.scene.Mute();
                    sounds.PlayGameOver();
                    game.GameoverCommand();
                }
            }

            else if(this.CollisionBox.Min_X < 0) {
                this.marioVec.X = 0;
                this.marioVel.X = 0;
                this.marioAccel.X = 0;
            }

            else if(this.CollisionBox.Max_X >= 4000) {
                this.marioVec.X = 4000 - this.CollisionBox.Width;
                this.marioVel.X = 0;
                this.marioAccel.X = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch);
        }
        public void FireballCommand()
        {
            currentState.FireballCommand();
        }
        public void UpCommand()
        {
            currentState.UpCommand();
        }

        public void DownCommand()
        {
            currentState.DownCommand();
        }

        public void LeftCommand()
        {
            currentState.LeftCommand();
        }

        public void RightCommand()
        {
            currentState.RightCommand();
        }
        public void UpLeftCommand()
        {
            currentState.UpLeftCommand();
        }
        public void StandardCommand()
        {
            powerupState = standard;
        }
        public void SuperCommand()
        {
            powerupState = super;
        }

        public void FireCommand()
        {
            powerupState = fire;
        }
        public void DeadCommand()
        {
            powerupState = standard;
            this.toDead();
        }
    }
}
