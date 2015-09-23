using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    public class MarioFactory 
    {
        Dictionary<int, MarioSprite> sprites;
        Vector2 position;
        Texture2D texture;
        Rectangle sourceRect;
        int numberOfFrames;
        int timePerFrame;
        enum spriteType
        {
            idle = 1, jump = 2, walk = 3, superIdle = 11, superJump = 12, superWalk = 13, superCrouch = 14, fireIdle = 21, fireJump = 22, fireWalk = 23, fireCrouch = 24, dead = 52
        };

        spriteType sprite = spriteType.idle;
        public MarioFactory(Game1 game)
        {
            this.position.X = 0;
            this.position.Y = game.GraphicsDevice.Viewport.Height;
            this.texture = game.Content.Load<Texture2D>("mario_2");
            sprites = new Dictionary<int,MarioSprite>();
        }
        public MarioSprite MakeProduct(int x)
        {
            sprite = (spriteType)x;
            if (sprites.ContainsKey(x))
                return sprites[x];
            else
            {
                sourceRect.Width = 16;
                sourceRect.X = 8;
                sourceRect.Y = 0;
                sourceRect.Height = 16;
                timePerFrame =0;
                numberOfFrames = 1;
               switch(sprite)
                {
                                
                    case spriteType.idle:
                        {
                            sourceRect.Width = 13;
                            MarioSprite marioIdle = new MarioIdleSprite(position, texture, sourceRect, 0, numberOfFrames);
                            sprites.Add(x, marioIdle);
                            return marioIdle;
                        }
                    case spriteType.jump:
                        {
                            sourceRect.X = 158;
                            MarioSprite marioJump = new MarioJumpSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, marioJump);
                            return marioJump;
                        }
                    case spriteType.walk:
                        {
                            numberOfFrames = 3;
                            timePerFrame = 250;
                            sourceRect.X = 38;
                            sourceRect.Width = 75;
                            MarioSprite marioWalk = new MarioWalkSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, marioWalk);
                            return marioWalk;
                        }

                    case spriteType.superIdle:
                        {
                            sourceRect.X = 6;
                            sourceRect.Y = 52;
                            sourceRect.Height = 32;
                            MarioSprite superMarioIdle = new SuperMarioIdleSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, superMarioIdle);
                            return superMarioIdle;
                        }
                    case spriteType.superJump:
                        {
                            sourceRect.X = 156;
                            sourceRect.Y = 52;
                            sourceRect.Width = 16;
                            sourceRect.Height = 32;
                            MarioSprite superMarioJump = new SuperMarioJumpSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, superMarioJump);
                            return superMarioJump;
                        }
                    case spriteType.superWalk:
                        {
                            numberOfFrames = 3;
                            timePerFrame = 250;
                            sourceRect.X = 36;
                            sourceRect.Y = 52;
                            sourceRect.Width = 78;
                            sourceRect.Height = 32;
                            MarioSprite superMarioWalk = new SuperMarioWalkSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, superMarioWalk);
                            return superMarioWalk;
                        }
                    case spriteType.superCrouch:
                        {
                            sourceRect.X = 186;
                            sourceRect.Y = 57;
                            sourceRect.Height = 22;
                            MarioSprite superMarioCrouch = new SuperMarioCrouchSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, superMarioCrouch);
                            return superMarioCrouch;
                        }
                    case spriteType.fireIdle:
                        {
                            sourceRect.X = 6;
                            sourceRect.Height = 32;
                            sourceRect.Y = 122;
                            MarioSprite fireMarioIdle = new FireMarioIdleSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, fireMarioIdle);
                            return fireMarioIdle;
                        }
                    case spriteType.fireJump:
                        {
                            sourceRect.X = 159;
                            sourceRect.Height = 32;
                            sourceRect.Y = 122;
                            MarioSprite fireMarioJump = new FireMarioJumpSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, fireMarioJump);
                            return fireMarioJump;
                        }
                    case spriteType.fireWalk:
                        {
                            numberOfFrames = 3;
                            timePerFrame = 250;
                            sourceRect.X = 34;
                            sourceRect.Y = 122;
                            sourceRect.Width = 67;
                            sourceRect.Height = 32;
                            MarioSprite fireMarioWalk = new FireMarioWalkSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, fireMarioWalk);
                            return fireMarioWalk;
                        }
                    case spriteType.fireCrouch:
                        {
                            sourceRect.X = 186;
                            sourceRect.Y = 127;
                            sourceRect.Height = 22;
                            MarioSprite fireMarioCrouch = new FireMarioCrouchSprite(position, texture, sourceRect, timePerFrame, numberOfFrames);
                            sprites.Add(x, fireMarioCrouch);
                            return fireMarioCrouch;
                        }
                    default:
                        {
                            sourceRect.X = 186;
                            sourceRect.Y = 15;
                            MarioSprite marioDead = new MarioDeadSprite(position, texture, sourceRect, 0, numberOfFrames);
                            sprites.Add(x, marioDead);
                            return marioDead;
                        }
                }
            }
        }
    }
}
