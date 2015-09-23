using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public class BlockQuestionSprite: Sprite
    {
      private  int containsItem;
      private int containsCoin;
      private float time;

        public BlockQuestionSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base (scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            containsItem = ContainsItem;
            containsCoin = ContainsCoin;
            this.IsBlock = true;
        }

        public override void HandleCollision(ISprite movSprite)
        {
            if (movSprite is MarioJumpingSprite && movSprite.CollisionBox.Velocity.Y < 0)
            {
                this.alive = false;
                scene.Sprites.Remove(this);
                Sprite used = blockFactory.MakeProduct(5);
                used.CollisionBox.Physics(new Vector2(this.Position.X, this.Position.Y), Vector2.Zero, Vector2.Zero);
                scene.Sprites.Add(used);
                if (this.ContainsItem > 0)
                {
                    tmp = itemFactory.MakeProduct(this.ContainsItem);
                    tmp.CollisionBox.Physics(new Vector2(this.Position.X, this.Position.Y - 33), Vector2.Zero, Vector2.Zero);
                    scene.Sprites.Add(tmp);
                }
                else if(this.ContainsCoin > 0)
                {
                    tmp = itemFactory.MakeProduct(4);
                    tmp.CollisionBox.Physics(new Vector2(this.Position.X, this.Position.Y - 33), Vector2.Zero, Vector2.Zero);
                    scene.Sprites.Add(tmp);

                    tmp.HandleCollision(movSprite);
                    sounds.PlayCoin();
                 //   scene.mario.Coins++;
                }
            }
        }
    }

    public class BlockUsedSprite: Sprite
    {
        public BlockUsedSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            this.IsBlock = true;
        }     
    }

    public class BlockFloorSprite : Sprite
    {
        public BlockFloorSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            this.IsBlock = true;
        }

    }

    public class BlockBrickSprite : Sprite
    {
        Sprite brokenSprite;
        float totalElapsed;
        public BlockBrickSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            this.IsBlock = true;
             brokenSprite = blockFactory.MakeProduct(7);
             
            
        }
        public override void HandleCollision(ISprite movSprite)
        {
            if (movSprite is MarioJumpingSprite && movSprite is MarioStandardSprite && movSprite.CollisionBox.Velocity.Y < 0)
            {
                this.bumped = true;
                sounds.PlayBump();
            }

            else if (movSprite is MarioJumpingSprite && movSprite.CollisionBox.Velocity.Y < 0)
            {
              //  Sprite brokenSprite = blockFactory.MakeProduct(7);
               // brokenSprite.Position = this.Position;
                scene.Sprites.Remove(this);
                scene.Sprites.Add(brokenSprite);
                
                
                sounds.PlayBrickSmash();
            }
        }
        public override void Update(float elapsed, int direction)
        {
            brokenSprite.Position = this.CollisionBox.Position;
            if (bumped)
            {
                totalElapsed += elapsed;
                this.CollisionBox.Y--;
                if (totalElapsed >= 5 * elapsed)
                {
                    bumped = false;
                    back = true;
                }
            }
            if (back)
            {
                totalElapsed -= elapsed;
                this.CollisionBox.Y++;
                if (totalElapsed <= 0)
                {
                    back = false;
                }
            }
        }
    }

    public class BlockBrokenSprite : Sprite
    {
        Vector2 velocity;
        Vector2 accel;
        float gravity;
        public BlockBrokenSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            velocity = new Vector2(0, 0);
            gravity = scene.Gravity;
            accel = new Vector2(0, gravity);
        }

        public override void Update(float elapsed, int direction)
        {
            velocity += accel * elapsed / 100;
            this.CollisionBox.Y += velocity.Y;
        }
    }

    public class BlockPyramidSprite : Sprite
    {
        public BlockPyramidSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            this.IsBlock = true;
        }
    }

    public class BlockHiddenSprite : Sprite
    {
        public BlockHiddenSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            this.IsBlock = true;
        }
    }

    public class FlagPoleSprite : Sprite
    {
        public FlagPoleSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
        }

        public override void HandleCollision(ISprite movSprite)
        {
            float finishHeight = 543f - movSprite.CollisionBox.Max_Y;

            if (finishHeight <= 17)
                scene.mario.Points += 100;
            else if (finishHeight <= 57)
                scene.mario.Points += 400;
            else if (finishHeight <= 81)
                scene.mario.Points += 800;
            else if (finishHeight <= 127)
                scene.mario.Points += 2000;
            else if (finishHeight <= 153)
                scene.mario.Points += 4000;
            else
                scene.mario.Lives++;

            scene.Victory = true;
        }
    }
    public class FlagBulbSprite : Sprite
    {
        public FlagBulbSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {

        }
    }
    public class FlagSprite : Sprite
    {
        public FlagSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {            
        }
    }
}
