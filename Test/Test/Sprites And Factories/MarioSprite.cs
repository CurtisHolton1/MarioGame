using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    public abstract class MarioSprite : ISprite
    {
        public bool falling;
        private int containsItem;
        private Texture2D texture;
        protected Texture2D Texture
        {
            get { return texture; }
            //   set { texture = value; }
        }
        private int timePerFrame;
        protected int TimePerFrame
        {
            get { return timePerFrame; }
            //  set { timePerFrame = value; }
        }
        private Rectangle sourceRect;
        protected Rectangle SourceRect
        {
            get { return sourceRect; }
            // set { sourceRect = value; }
        }
        private int numberOfFrames;
        protected int NumberOfFrames
        {
            get { return numberOfFrames; }
        }
        private int direction;
        protected int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        protected Vector2 origin;

        private CollisionBox collisionBox;
        public CollisionBox CollisionBox
        {
            get { return collisionBox; }
            set { collisionBox = value; }
        }
        private Vector2 position;
        public Vector2 Position
        {
            get { return CollisionBox.Position; }
            set { CollisionBox.Position = value; }
        }
        private bool isBlock;
        public bool IsBlock { get { return isBlock; } }

        protected MarioSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
        {
            this.numberOfFrames = numberOfFrames;
            this.position = position;    
            this.texture = texture;
            this.timePerFrame = timePerFrame;
            this.sourceRect = sourceRect;
            Direction = 1;
            this.origin.X = 0;
           // this.origin.Y = sourceRect.Height;
            position.Y = position.Y - sourceRect.Height;
            collisionBox = new CollisionBox(position.X, position.Y , 32, SourceRect.Height*2);
            isBlock = false;
        }

        //public void DrawBox(Rectangle rectangleToDraw, SpriteBatch spriteBatch, int thicknessOfBorder, Color borderColor) {
        //    pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        //    pixel.SetData(new[] { Color.White }); 
        //    spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

        //    // Draw left line
        //    spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

        //    // Draw right line
        //    spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
        //                                    rectangleToDraw.Y,
        //                                    thicknessOfBorder,
        //                                    rectangleToDraw.Height), borderColor);
        //    // Draw bottom line
        //    spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
        //                                    rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
        //                                    rectangleToDraw.Width,
        //                                    thicknessOfBorder), borderColor);
        
        //}

        public virtual void Update(float elapsed, int direction)
        {
            this.Direction = direction;
        }
        public virtual void Draw(SpriteBatch spritebatch)
        {
           // System.Console.WriteLine(collisionBox.Position.Y);
            if (Direction == 0)
                spritebatch.Draw(Texture, Position, SourceRect, Color.White, 0, origin, 2f, SpriteEffects.FlipHorizontally, 0);
            else
                spritebatch.Draw(Texture, Position, SourceRect, Color.White, 0, origin, 2f, SpriteEffects.None, 0);
        }

        public virtual void HandleCollision(ISprite movSprite)
        {

        }
    }
    public interface MarioJumpingSprite : ISprite 
    {
    }

    public interface MarioStandardSprite : ISprite
    {
    }
    public interface MarioLargeSprite : ISprite
    {
    }
    public interface MarioFireSprite : ISprite
    {
    }
    public interface MarioStarSprite : ISprite
    {
    }
    public interface MarioWalkingSprite : ISprite
    {
    }
    public interface MarioFallingSprite : ISprite
    {
    }
    public interface MarioDeathSprite : ISprite
    {
    }
    public interface MarioIdlingSprite : ISprite
    {
    }
    public interface MarioCrouchingSprite : ISprite
    {
    }

    public class MarioDeadSprite : MarioSprite, MarioDeathSprite
    {
        public MarioDeadSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {
        }

        
    }
    public class MarioIdleSprite : MarioSprite, MarioIdlingSprite, MarioStandardSprite
    {

        public MarioIdleSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {
        }

    }
    public class MarioJumpSprite : MarioSprite, MarioJumpingSprite, MarioStandardSprite
    {
        public MarioJumpSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {
        }
    }

    public class MarioWalkSprite : MarioSprite, MarioWalkingSprite, MarioStandardSprite
    {
       private float totalElapsed = 0;
        private int frame = 0;
        private bool flag;
       private int offset;
       private int FrameWidth;
        private Rectangle drawRect;
        public MarioWalkSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {
            //SpriteBox_Width = 14;
        }
        public override void Update(float elapsed, int direction)
        {
            this.Direction = direction;
            totalElapsed += elapsed;         
            if (totalElapsed > TimePerFrame)
            {
                if (frame == NumberOfFrames - 1 || frame == 0)
                    flag = !flag;
                if (flag)
                    frame++;
                else
                    frame--;
                totalElapsed -= TimePerFrame;
            }

        }
        public override void Draw(SpriteBatch spritebatch)
        {
            //small mario has different widths at different sprites
            if (frame == 0)
            {
                FrameWidth = 14;
                offset = 0;
            }
            if (frame == 1)
            {
                FrameWidth = 12;
                offset = 31;
            }
            else
            {
                FrameWidth = 16;
                offset = 59;
            }
            drawRect = new Rectangle(SourceRect.X + offset, SourceRect.Y, FrameWidth, SourceRect.Height);
            if (Direction == 0)
                spritebatch.Draw(Texture, Position, drawRect, Color.White, 0, origin, 2f, SpriteEffects.FlipHorizontally, 0);
            else
                spritebatch.Draw(Texture, Position, drawRect, Color.White, 0, origin, 2f, SpriteEffects.None, 0);
        }
    }

    //super mario
    public class SuperMarioIdleSprite : MarioSprite, MarioIdlingSprite, MarioLargeSprite
    {

        public SuperMarioIdleSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {
        }
    }
        public class SuperMarioJumpSprite : MarioSprite, MarioJumpingSprite, MarioLargeSprite
        {

            public SuperMarioJumpSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
                : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
            {
            }         
        }

        public class SuperMarioWalkSprite : MarioSprite, MarioWalkingSprite, MarioLargeSprite
        {
            float totalElapsed = 0;
            int frame = 0;
            bool flag;
            Rectangle drawRect;
            int FrameWidth = 16;
            public SuperMarioWalkSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
                : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
            {
            }
            public override void Update(float elapsed, int direction)
            {
                this.Direction = direction;
                totalElapsed += elapsed;
                if (totalElapsed > TimePerFrame)
                {
                    if (frame == NumberOfFrames - 1 || frame == 0)
                        flag = !flag;
                    if (flag)
                        frame++;
                    else
                        frame--;
                    totalElapsed -= TimePerFrame;
                }
            }
            public override void Draw(SpriteBatch spritebatch)
            {
                drawRect = new Rectangle(SourceRect.X + (FrameWidth * frame) + 14 * frame, SourceRect.Y, FrameWidth, SourceRect.Height);
                if (Direction == 0)
                    spritebatch.Draw(Texture, Position, drawRect, Color.White, 0, origin, 2f, SpriteEffects.FlipHorizontally, 0);
                else
                    spritebatch.Draw(Texture, Position, drawRect, Color.White, 0, origin, 2f, SpriteEffects.None, 0);
            }
        }
        public class SuperMarioCrouchSprite : MarioSprite, MarioCrouchingSprite, MarioLargeSprite
        {
            public SuperMarioCrouchSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
                : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
            {
            }
        }
        //Fire mario

        public class FireMarioIdleSprite : MarioSprite, MarioIdlingSprite, MarioFireSprite
        {
            public FireMarioIdleSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
                : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
            {
            }
        }

        public class FireMarioJumpSprite : MarioSprite, MarioJumpingSprite, MarioFireSprite
        {
            public FireMarioJumpSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
                : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
            {
            }
        }

        public class FireMarioWalkSprite : MarioSprite, MarioWalkingSprite, MarioFireSprite
        {
            float totalElapsed = 0;
            int frame = 0;
            bool flag;
            int FrameWidth = 16;
            Rectangle drawRect;
            public FireMarioWalkSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
                : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
            {
            }
            public override void Update(float elapsed, int direction)
            {
                this.Direction = direction;
                totalElapsed += elapsed;
                if (totalElapsed > TimePerFrame)
                {
                    if (frame == NumberOfFrames - 1 || frame == 0)
                        flag = !flag;
                    if (flag)
                        frame++;
                    else
                        frame--;
                    totalElapsed -= TimePerFrame;
                }
            }
            public override void Draw(SpriteBatch spritebatch)
            {
                 drawRect = new Rectangle(SourceRect.X + (FrameWidth * frame) + 10 * frame, SourceRect.Y, FrameWidth, SourceRect.Height);
                if (Direction == 0)
                {
                    spritebatch.Draw(Texture, Position, drawRect, Color.White, 0, origin, 2f, SpriteEffects.FlipHorizontally, 0);
                }
                else
                    spritebatch.Draw(Texture, Position, drawRect, Color.White, 0, origin, 2f, SpriteEffects.None, 0);
            }
        }
        public class FireMarioCrouchSprite : MarioSprite, MarioCrouchingSprite, MarioFireSprite
        {
            public FireMarioCrouchSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
                : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
            {
            }
        }
    }
