using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class Quadtree
    {
        List<ISprite> objects;
        List<Sprite> sec_objects; 
        private int maxobjects = 5;
        private int maxlevels = 5;
        private int level;
        private CollisionBox bounds;
        private Quadtree[] nodes;

        public Quadtree(int plevel, CollisionBox pbounds)
        {
            level = plevel;
            bounds = pbounds;
            objects = new List<ISprite>();
            sec_objects = new List<Sprite>();
            nodes = new Quadtree[4];
        }


        /*
        * Clears the quadtree
        */
        public void Clear()
        {
            objects.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }


        /*
         * Splits the node into 4 subnodes
         */
        private void Split()
        {
            int subWidth = (int)(bounds.Width / 2);
            int subHeight = (int)(bounds.Height / 2);
            int x = (int)bounds.X;
            int y = (int)bounds.Y;

            nodes[0] = new Quadtree(level + 1, new CollisionBox(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new Quadtree(level + 1, new CollisionBox(x, y, subWidth, subHeight));
            nodes[2] = new Quadtree(level + 1, new CollisionBox(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new Quadtree(level + 1, new CollisionBox(x + subWidth, y + subHeight, subWidth, subHeight));
        }


        /*
        * Determine which node the object belongs to. -1 means
        * object cannot completely fit within a child node and is part
        * of the parent node
        */
        private int GetIndex(CollisionBox pRect)
        {
            int index = -1;
            float verticalMidpoint = bounds.X + (bounds.Width / 2);
            float horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (pRect.Y < horizontalMidpoint && pRect.Y + pRect.Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (pRect.Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (pRect.X < verticalMidpoint && pRect.X + pRect.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (pRect.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        /*
        * Insert the object into the quadtree. If the node
        * exceeds the capacity, it will split and add all
        * objects to their corresponding nodes.
        */
        public void Insert(ISprite nsprite)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(nsprite.CollisionBox);

                if (index != -1)
                {
                    nodes[index].Insert(nsprite);

                    return;
                }
            }

            objects.Add(nsprite);

            if (objects.Count > maxobjects && level < maxlevels)
            {
                if (nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < objects.Count)
                {
                    int index = GetIndex(objects.ElementAt(i).CollisionBox);
                    if (index != -1)
                    {
                        nodes[index].Insert(objects.ElementAt(i));
                        objects.RemoveAt(i);
                    }
                    else
                    {
                        i++; 
                    }
                }
            }
        }


        public void Insertsec(Sprite secsprite)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(secsprite.CollisionBox);

                if (index != -1)
                {
                    nodes[index].Insert(secsprite);

                    return;
                }
            }

            sec_objects.Add(secsprite);

            if (sec_objects.Count > maxobjects && level < maxlevels)
            {
                if (nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < sec_objects.Count)
                {
                    int index = GetIndex(sec_objects.ElementAt(i).CollisionBox);
                    if (index != -1)
                    {
                        nodes[index].Insert(sec_objects.ElementAt(i));
                        sec_objects.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }




        /*
        * Return all objects that could collide with the given object
        */
        public List<ISprite> retrieve(List<ISprite> returnObjects, ISprite mainSprite)
        {
            int index = GetIndex(mainSprite.CollisionBox);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].retrieve(returnObjects, mainSprite);
            }

            returnObjects.AddRange(objects);

            return returnObjects;
        }

        public List<Sprite> retrieve_sec(List<Sprite> returnSecObjects, Sprite mainSprite)
        {
            int index = GetIndex(mainSprite.CollisionBox);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].retrieve_sec(returnSecObjects, mainSprite);
            }

            returnSecObjects.AddRange(sec_objects);

            return returnSecObjects;
        }



       
    }
}

