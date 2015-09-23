using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    public class GoombaWalkingSprite : Sprite
    {
        public GoombaWalkingSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
            this.scene = scene;
            Velocity = new Vector2(-5, 0);
        }
        public override void Update(float elapsed, int direction)
        {
            Vector2 pos = Position;
            foreach (Sprite sprite in scene.SSprites)
            {
                if (!sprite.IsBlock)
                {
                    continue;
                }
                if (this.CollisionBox.Intersect(this.CollisionBox, sprite.CollisionBox))
                {
                    if (Velocity.X < 0) //Walking Left
                    {
                        pos.X = sprite.CollisionBox.Max_X + 1;
                    }
                    else if(Velocity.X > 0)
                    {
                        pos.X = sprite.CollisionBox.Min_X - this.CollisionBox.Width - 1;
                    }
                    Velocity *= -1;
                    break;
                }
            }
            Position = pos;
            Position += Velocity * elapsed / 100;
            this.CollisionBox.Physics(Position, Velocity, Vector2.Zero);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                Rectangle drawRect = new Rectangle(SourceRect.X + (30 * Frame), SourceRect.Y, 16, SourceRect.Height);
                spriteBatch.Draw(Texture, Position, drawRect, Color.White, 0, origin, 2f, SpriteEffects.None, 0);
            }
            else
            {
                //Rectangle drawRect = new Rectangle(SourceRect.X + (60), SourceRect.Y, 16, SourceRect.Height);
                //spriteBatch.Draw(Texture, Position, drawRect, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
                Sprite squishedSprite = enemyFactory.MakeProduct(2);
                squishedSprite.Position = this.Position;
                scene.Sprites.Add(squishedSprite);
                scene.Sprites.Remove(this);
                scene.MovSprites.Remove(this);
            }
        }
        public override void HandleCollision(ISprite movSprite)
        {
            if (scene.mario.CurrentState is MarioFallingState && movSprite.CollisionBox.Velocity.Y > 0 && this.alive)
            {
                this.alive = false;
                sounds.PlayStomp();
                scene.mario.Points += 100;
                scene.mario.ToJump();
            }
            else if (!scene.mario.invincible && this.alive)
            {
                //scene.mario.ToIdle();

                if (scene.mario.PowerupState is MarioFire)
                {
                    scene.mario.SuperCommand();
                    sounds.PlayPowerDown();
                    scene.mario.invincible = true;

                }

                else if (scene.mario.PowerupState is MarioSuper)
                {
                    scene.mario.StandardCommand();
                    sounds.PlayPowerDown();
                    scene.mario.invincible = true;

                }

                //Mario is standard
                else
                {
                    scene.Sprites.Remove(this);
                    scene.mario.toDead();
                    scene.mario.Lives -= 1;
                    scene.Mute();
                    sounds.PlayDie();
                    
                }

            }
        }
    }


    public class GoombaSquishedSprite : Sprite
    {
        public GoombaSquishedSprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
            : base(scene, position, texture, sourceRect, timePerFrame, numberOfFrames, isAnimated)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRect = new Rectangle(SourceRect.X, SourceRect.Y, 16, SourceRect.Height);
            spriteBatch.Draw(Texture, Position, drawRect, Color.White, 0, origin, 2f, SpriteEffects.None, 0);
        }
        public override void Update(float elapsed, int direction)
        {
            this.CollisionBox.Y++;
            base.Update(elapsed, direction);
        }
    }
}
