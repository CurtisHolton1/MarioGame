using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
  public  class MarioCollision
    {
        Mario mario;
        Dictionary<Point, ISprite> sprites;
        Quadtree quad;
        SoundMachine sounds;
        ISprite sprite;
        Scene scene;
        List<ISprite> spriteList;
        float normalX;
        float normalY;
        private CollisionBox varBox;
        public CollisionBox VarBox { get { return varBox; } }
        private float collisionTime;
        public float CollisionTime { get { return collisionTime; } }
       
        private CollisionBox floorbox;
        public CollisionBox Floorbox { get { return floorbox; }  }
        public MarioCollision(Mario mario, Scene scene)
        {
            this.scene = scene;
            this.mario = mario;
            spriteList = scene.Sprites;      
            this.sounds = new SoundMachine(scene.Game);
        }


        public void Update()
        {
            
                bool flag = false;
                floorbox = FloorBox(mario.CollisionBox);
                mario.IsGrounded = false;
                collisionTime = 1;
                varBox = GetBox(mario.CollisionBox);
                for (int i = 0; i < spriteList.Count; i++)
                {
                    sprite = spriteList.ElementAt(i);
                    if (AABBCheck(varBox, sprite.CollisionBox))
                    {
                        collisionTime = SweptAABB(varBox, sprite.CollisionBox, out normalX, out normalY);
                        float remainingTime = 1.0f - collisionTime;
                        if (NonSweep(mario.CollisionBox, sprite.CollisionBox))
                        {
                            float t = sprite.CollisionBox.Y - (mario.CollisionBox.Y + mario.CollisionBox.Height);
                            float b = (sprite.CollisionBox.Y + sprite.CollisionBox.Height) - mario.CollisionBox.Y;
                            float l = sprite.CollisionBox.X - (mario.CollisionBox.X + mario.CollisionBox.Width);
                            float r = (sprite.CollisionBox.X + sprite.CollisionBox.Width) - mario.CollisionBox.X;
                            if (sprite.IsBlock)
                            {
                                if (t < 0 && b > 0 && mario.MarioVel_Y < 0)
                                {
                                    mario.MarioVec_Y = sprite.CollisionBox.Max_Y + 1 + mario.CollisionBox.Height;
                                    mario.MarioVel_Y = 0;
                                }
                                else if (t < 0 && b > 0 && mario.MarioVel_Y > 0)
                                {
                                    mario.IsGrounded = true;
                                    mario.MarioVel_Y = 0;
                                    mario.MarioVec_Y = sprite.CollisionBox.Min_Y - 1;
                                }
                                else if (l < 0 && r > 0 && mario.MarioVel_X > 0 && mario.MarioVel_Y == 0)
                                {
                                    mario.MarioVel_X = 0;
                                    mario.MarioAccel_X = 0;
                                    mario.MarioVec_X = sprite.CollisionBox.Min_X - 1 - mario.CollisionBox.Width;
                                }

                                else if (l < 0 && r > 0 && mario.MarioVel_X < 0 && mario.MarioVel_Y == 0)
                                {
                                    mario.MarioVec_X = sprite.CollisionBox.Max_X + 1;
                                    mario.MarioVel_X = 0;
                                    mario.MarioAccel_X = 0;
                                }
                                sprite.HandleCollision(mario.CurrentSprite);
                                break;
                            }
                            else
                            {
                                sprite.HandleCollision(mario.CurrentSprite);
                            }
                        }
                    }
                    if (NonSweep(floorbox, sprite.CollisionBox))
                    {
                        flag = true;
                    }
                }
                if (!flag && !(mario.CurrentState is MarioJumpingState))
                {
                    mario.ToFall();

                }

            
        }

        public static bool AABBCheck(CollisionBox b1, CollisionBox b2)
        {
            return !(b1.X + b1.Width < b2.X || b1.X > b2.X + b2.Width || b1.Y + b1.Height < b2.Y || b1.Y > b2.Y + b2.Height);
        }


        public static bool NonSweep(CollisionBox b1, CollisionBox b2)
        {
           

            float l = b2.X - (b1.X + b1.Width);
            float r = (b2.X + b2.Width) - b1.X;
            float t = b2.Y - (b1.Y + b1.Height);
            float b = (b2.Y + b2.Height) - b1.Y;

            // check that there was a collision
            if (l > 0 || r < 0 || t > 0 || b < 0)
                return false;

            // find the offset of both sides
           

            return true;
        }

        public bool Intersect(CollisionBox a, CollisionBox b)
        {
            if (a.Max_X < b.Min_X || a.Min_X > b.Max_X)
                return false;
            if (a.Max_Y < b.Min_Y || a.Min_Y > b.Max_Y)
                return false;
            return true;
        }


        public CollisionBox FloorBox(CollisionBox marioBox)
        {
            CollisionBox floorbox = new CollisionBox(mario.CollisionBox.X, mario.CollisionBox.Max_Y, mario.CollisionBox.Width, 5);
            return floorbox;
        }



        //for broad detection
        public CollisionBox GetBox(CollisionBox marioBox)
        {
            CollisionBox varBox = new CollisionBox(mario.CollisionBox.X, mario.CollisionBox.Y, mario.CollisionBox.Width, mario.CollisionBox.Height);
            if (marioBox.Velocity.X >= 0)
            {
                varBox.X = marioBox.X;
                varBox.Width = marioBox.Velocity.X + marioBox.Width;
            }
            else
            {
                varBox.X = marioBox.X + marioBox.Velocity.X;
                varBox.Width = marioBox.Width - marioBox.Velocity.X;
            }
            if (marioBox.Velocity.Y >= 0)
            {
                varBox.Y = marioBox.Y;
                varBox.Height = marioBox.Velocity.Y + marioBox.Height;
            }
            else
            {
                varBox.Y = marioBox.Y + marioBox.Velocity.Y;
                varBox.Height = marioBox.Height - marioBox.Velocity.Y;
            }
            // to set mins and max
            //varBox.Physics(varBox.Position, varBox.Velocity, varBox.Acceleration);

            return varBox;

        }


        public static float SweptAABB(CollisionBox b1, CollisionBox b2, out float normalx, out float normaly)
        {
            float xInvEntry, yInvEntry;
            float xInvExit, yInvExit;

            // find the distance between the objects on the near and far sides for both x and y
            if (b1.Velocity.X > 0.0f)
            {
                xInvEntry = b2.X - (b1.X + b1.Width);
                xInvExit = (b2.X + b2.Width) - b1.X;
            }
            else
            {
                xInvEntry = (b2.X + b2.Width) - b1.X;
                xInvExit = b2.X - (b1.X+ b1.Width);
            }

            if (b1.Velocity.Y > 0.0f)
            {
                yInvEntry = b2.Y - (b1.Y + b1.Height);
                yInvExit = (b2.Y + b2.Height) - b1.Y;
            }
            else
            {
                yInvEntry = (b2.Y + b2.Height) - b1.Y;
                yInvExit = b2.Y - (b1.Y + b1.Height);
            }

            // find time of collision and time of leaving for each axis (if statement is to prevent divide by zero)
            float xEntry, yEntry;
            float xExit, yExit;

            if (b1.Velocity.X == 0.0f)
            {
                xEntry = -float.PositiveInfinity;
                xExit = float.PositiveInfinity;
            }
            else
            {
                xEntry = xInvEntry / b1.Velocity.X;
                xExit = xInvExit / b1.Velocity.X;
            }

            if (b1.Velocity.Y == 0.0f)
            {
                yEntry = -float.PositiveInfinity;
                yExit = float.PositiveInfinity;
            }
            else
            {
                yEntry = yInvEntry / b1.Velocity.Y;
                yExit = yInvExit / b1.Velocity.Y;
            }

            // find the earliest/latest times of collision
            float entryTime = Math.Max(xEntry, yEntry);
            float exitTime = Math.Min(xExit, yExit);

            // if there was no collision
            if (entryTime > exitTime || xEntry < 0.0f && yEntry < 0.0f || xEntry > 1.0f || yEntry > 1.0f)
            {
                normalx = 0.0f;
                normaly = 0.0f;
                return 1.0f;
            }
            else // if there was a collision
            {
                // calculate normal of collided surface
                if (xEntry > yEntry)
                {
                    if (xInvEntry < 0.0f)
                    {
                        normalx = 1.0f;
                        normaly = 0.0f;
                    }
                    else
                    {
                        normalx = -1.0f;
                        normaly = 0.0f;
                    }
                }
                else
                {
                    if (yInvEntry < 0.0f)
                    {
                        normalx = 0.0f;
                        normaly = 1.0f;
                    }
                    else
                    {
                        normalx = 0.0f;
                        normaly = -1.0f;
                    }
                }

                // return the time of collision
                return entryTime;
            }
        }
    }
}

