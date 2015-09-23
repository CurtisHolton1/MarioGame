using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class OtherCollision
    {
        Mario mario;
        List<Sprite> sprites;
        List<Sprite> movsprites;
        Quadtree quad;
        SoundMachine sounds;
        Sprite tmp;
        Scene scene;
        List<Sprite> nearObjects;
        public OtherCollision(Mario mario, Scene scene, Quadtree quadtree)
        {
            this.scene = scene;
            this.mario = mario;
            this.sprites = scene.SSprites;
            this.movsprites = scene.MovSprites;
            this.quad = quadtree;
            this.sounds = new SoundMachine(scene.Game);
            this.nearObjects = new List<Sprite>();
        }


        public void Update()
        {
            //Run sprites through Quadtree
            quad.Clear();
            for (int i = 0; i < sprites.Count; i++)
            {
                quad.Insertsec(sprites.ElementAt(i));
            }
            for (int j = 0; j < movsprites.Count; j++)
            {
                Sprite move = movsprites.ElementAt(j);
                nearObjects.Clear();
                //Get list of sprites near the moving sprite
                quad.retrieve_sec(nearObjects, move);
               //Console.WriteLine(movsprites.Count);
                //Console.WriteLine(nearObjects.Count);
               //Console.WriteLine(sprites.Count);
                //Run collision detection only with those sprites
                for (int x = 0; x < nearObjects.Count; x++)
                {
                    Sprite sprite = nearObjects.ElementAt(x);
                    foreach (Point p in sprite.CollisionBox.Cells)
                    {
                                //Now we know that the sprite and mario occupy at least one of the same cells.
                            if (Intersect(move.CollisionBox, sprite.CollisionBox))
                            {                                    
                                move.Velocity = new Vector2 (-(move.Velocity.X),0);
                            }
                    }
                }
                
            }            
        }         



        public bool Intersect(CollisionBox a, CollisionBox b)
        {
            if (a.Max_X < b.Min_X || a.Min_X > b.Max_X)
                return false;
            if (a.Max_Y < b.Min_Y || a.Min_Y > b.Max_Y)
                return false;
            return true;
        }
    }
}

