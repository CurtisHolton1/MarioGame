using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public abstract class BlockSprite: ISprite
    {

        protected Vector2 position;
        protected Texture2D texture;
        protected int timePerFrame;
        protected Rectangle sourceRect;
        protected int numberOfFrames;
        protected Vector2 origin;

        public BlockSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
        {
            this.numberOfFrames = numberOfFrames;
            this.position = position;
            this.texture = texture;
            this.timePerFrame = timePerFrame;
            this.sourceRect = sourceRect;   
            this.origin.X = 0;
            this.origin.Y = 0;
        }
        public abstract void Update(float elapsed, int direction);
        public abstract void Draw(SpriteBatch spriteBatch);
     
    }

    public class BlockQuestionSprite: BlockSprite
    {
        float totalElapsed = 0;
        int frame = 0;

        public BlockQuestionSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base (position, texture, sourceRect, timePerFrame, numberOfFrames)
        {

        }
        public override void Update(float elapsed, int direction)
        {
            totalElapsed += elapsed;
            if (totalElapsed > timePerFrame)
            {
                frame++;
                if (frame == numberOfFrames)
                    frame = 0;
                totalElapsed -= timePerFrame;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            origin.Y = 0;
            int FrameWidth = 16;
            Rectangle drawRect = new Rectangle(sourceRect.X + (FrameWidth * frame), sourceRect.Y, FrameWidth, sourceRect.Height);

            spriteBatch.Draw(texture, position, drawRect, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
        }
    }

    public class BlockUsedSprite: BlockSprite
    {
        public BlockUsedSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base (position, texture, sourceRect, timePerFrame, numberOfFrames) 
        {

        }
        public override void Update(float elapsed, int direction)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
        }
    }

    public class BlockFloorSprite : BlockSprite
    {
        public BlockFloorSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {

        }
        public override void Update(float elapsed, int direction)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
        }
    }

    public class BlockBrickSprite : BlockSprite
    {
        public BlockBrickSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {

        }
        public override void Update(float elapsed, int direction)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
        }
    }

    public class BlockPyramidSprite : BlockSprite
    {
        public BlockPyramidSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {

        }
        public override void Update(float elapsed, int direction)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
        }
    }

    public class BlockHiddenSprite : BlockSprite
    {
        public BlockHiddenSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {

        }
        public override void Update(float elapsed, int direction)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
        }
    }

    public class BlockBumpSprite : BlockSprite
    {
        private Vector2 maxposition;
        private int dir;
        public BlockBumpSprite(Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames)
            : base(position, texture, sourceRect, timePerFrame, numberOfFrames)
        {
            maxposition = position;
            maxposition.Y = position.Y + 10;
            dir = 1;
        }
        public override void Update(float elapsed, int direction)
        {
           
            if (position.Y.Equals(maxposition.Y))
            {
                dir *= -1;
            }
            position.Y -= dir;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
