using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    public class FlowerSprite : Sprite
    {
        public FlowerSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {

        }

        public override void HandleCollision(ISprite movSprite)
        {
            if (movSprite is MarioLargeSprite)
            {
                scene.mario.FireCommand();
                scene.Sprites.Remove(this); // hopefully this works
                scene.mario.Points += 1000;
            }
        }
        
    }

    public class StarSprite : Sprite
    {
        public StarSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {

        }

        public override void HandleCollision(ISprite movSprite)
        {
            //Star Mario to be implemented later
            scene.mario.Points += 1000;
            scene.Sprites.Remove(this);
        }
    }

    public class MushroomSprite : Sprite
    {
        public MushroomSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            Velocity = new Vector2(-5, 0);
        }
        public override void Update(float elapsed, int direction)
        {            
            Position += Velocity*elapsed/100;
            this.CollisionBox.Physics(Position, Velocity, Vector2.Zero);
        }

        public override void HandleCollision(ISprite movSprite)
        {
            if (!(scene.mario.PowerupState is MarioFire))
            {
                scene.mario.SuperCommand();
                sounds.PlayPowerUp();
            }
            scene.mario.Points += 1000;
            scene.Sprites.Remove(this);
            scene.MovSprites.Remove(this);
        }
      
        
    }
    public class CoinSprite : Sprite
    {
        float time;
        public CoinSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
        
        }

        public override void HandleCollision(ISprite movSprite)
        {
            sounds.PlayCoin();
            scene.mario.Coins += 1;
            scene.mario.Points += 200;
           // scene.Sprites.Remove(this);
        }
        public override void Update(float elapsed, int direction)
        {
            time += elapsed;
            if (time > 500)
            {
                scene.Sprites.Remove(this);
            }
        }
        
    }
    public class OneUpSprite : Sprite
    {
        
               public OneUpSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
                   : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            Velocity = new Vector2(5, 0);
        }
        public override void Update(float elapsed, int direction)
        {          
            Position += Velocity*elapsed/100;
            this.CollisionBox.Physics(Position, Velocity, Vector2.Zero);
        }

        public override void HandleCollision(ISprite movSprite)
        {
            sounds.PlayOneUp();
            scene.mario.Lives += 1;
            scene.Sprites.Remove(this);
            scene.MovSprites.Remove(this);
        }

    }
}
