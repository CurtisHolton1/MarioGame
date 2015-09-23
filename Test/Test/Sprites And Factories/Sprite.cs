using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarioGame;

namespace MarioGame
{
   public abstract class Sprite: ISprite
    {
       protected Scene scene;
       protected SoundMachine sounds;
       private Texture2D texture;
        public Texture2D Texture
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
        public bool alive = true;
        public bool bumped = false;
        public bool back = false;
        public Sprite tmp;
        protected BlockFactory blockFactory;
        protected ItemFactory itemFactory;
        protected EnemyFactory enemyFactory;
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

        public float spritepos_X
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public float spritepos_Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
        private Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
      
       
       private bool isAnimated;
       private float totalElapsed = 0;
       private int frame = 0;
       //comment
       public int Frame { get { return frame; } }
       int frameWidth;
       private int containsItem;
       public int ContainsItem
       {
           get { return containsItem; }
           set { containsItem = value; }
       }

       private int containsCoin;
       public int ContainsCoin
       {
           get { return containsCoin; }
           set { containsCoin = value; }
       }

       private bool isBlock;
       public bool IsBlock { get { return isBlock; } set { isBlock = value; } }
 
           public Sprite(Scene scene, Vector2 position, Texture2D texture, Rectangle sourceRect, int timePerFrame, int numberOfFrames, bool isAnimated)
           {
               this.scene = scene;
               this.isAnimated = isAnimated;
               this.numberOfFrames = numberOfFrames;
               this.position = position;
               this.texture = texture;
               this.timePerFrame = timePerFrame;
               this.sourceRect = sourceRect;
               this.origin.X = 0;
               this.origin.Y = 0;
               Velocity = new Vector2(0, 0);
               blockFactory = new BlockFactory(scene.Game);
               itemFactory = new ItemFactory(scene.Game);
               enemyFactory = new EnemyFactory(scene.Game);
               this.sounds = new SoundMachine(scene.Game);
              // frameWidth = sourceRect.Width / numberOfFrames;
               frameWidth = 16;
               collisionBox = new CollisionBox(position.X, position.Y, 32, SourceRect.Height*2);
               
           }

           public Sprite(Vector2 pos, Texture2D tex)
           {
               this.texture = tex;
               this.position = pos;
           }

           public virtual void HandleCollision(ISprite movSprite)
           {

           }


           public virtual void Update(float elapsed,int direction)
           {
               if (isAnimated)
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
               
                 
           }
           public virtual void Draw(SpriteBatch spriteBatch)
           {          
               if(!isAnimated)
               spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0, origin, 2f, SpriteEffects.None, 0);
           else
           {
               Rectangle drawRect = new Rectangle(sourceRect.X + (frameWidth * frame), sourceRect.Y, frameWidth, sourceRect.Height);
               spriteBatch.Draw(texture, Position, drawRect, Color.White, 0, origin, 2f, SpriteEffects.None, 0);
           }
       }
    }
}

