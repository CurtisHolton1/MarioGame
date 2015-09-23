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
    /*This class is a Context in the State Pattern. 
     */
    public class Block
    {
        BlockFactory BlockFactory;   

        //private CollisionBox collisionBox;
        public CollisionBox CollisionBox { get { return currentSprite.CollisionBox; } set { currentSprite.CollisionBox = value; } }


        public Block(Game1 game, int posX, int posY)
        {
            BlockFactory = new BlockFactory(game);                      
           
        }

        public void toFall()
        {
            previousState = currentState;
            currentState = fall;
            previousSprite = currentSprite;  
        }

        public void ToIdle()
        {

            previousState = currentState;
            currentState = idle;
            previousSprite = currentSprite;


        }
        public void ToJump()
        {
            previousState = currentState;
            currentState = jump;
            previousSprite = currentSprite;

        }

        public void ToWalk()
        {
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

        public void Update(float elapsed)
        {
           actionStateFactor = currentState.Factor();
           powerupStateFactor = powerupState.Factor();       
           currentSprite = (MarioSprite)MarioFactory.MakeProduct(powerupStateFactor + actionStateFactor);
           
           currentSprite.CollisionBox.Physics(new Vector2(MarioVec.X, MarioVec.Y-CollisionBox.Height), Vector2.Zero, Vector2.Zero);
           currentSprite.Update(elapsed, direction);
           currentState.Update();
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
            powerupState = dead;
        }
    }
}
