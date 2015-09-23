using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    public class CollisionBox
    {
        private Vector2 velocity;
        public Vector2 Velocity{ get {return velocity; } set{velocity = value; }}

        public float Velocity_X { get { return velocity.X; } set { velocity.X = value; } }
        public float Velocity_Y { get { return velocity.Y; } set { velocity.Y = value; } }
        private Vector2 acceleration;
        public Vector2 Acceleration{get {return acceleration;} set{acceleration = value;}}
        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }
        private float x;
        public float X { get{return position.X;} set{position.X = value;} }
        private float y;
        public float Y { get { return position.Y; } set { position.Y = value; } }
        private float width;
        public float Width { get { return width; } set { width = value; } }
        private float height;
        public float Height { get { return height; } set { height = value; } }
        private Point min;
        public Point Min { get { return min; } set { min = value; } }

        public int Min_X { get { return min.X; } }
        public int Min_Y { get { return min.Y; } }
        private Point max;
        public Point Max { get { return max; } set { max = value; } }
        public int Max_X { get { return max.X; } }
        public int Max_Y { get { return max.Y; } }
        private Vector2 center;
        public Vector2 Center { get { return center; } }
        

        private List<Point> cells;
        public List<Point> Cells { get { return cells; } set { cells = value; } }


        private static Texture2D pixel;
        private Texture2D Pixel
        {
            get { return pixel; }
            set { pixel = value; }
        }


        public CollisionBox(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            position.X = x;
            position.Y = y;
            this.width = width;
            this.height = height;
            this.velocity = Vector2.Zero;
            acceleration = Vector2.Zero;
            min.X = (int)Position.X;
            min.Y = (int)Position.Y;
            max.X = (int)Position.X + (int)width;
            max.Y = (int)Position.Y + (int)height;
            cells = new List<Point>();
            OccupyingCells();
            center = new Vector2(x + width / 2, y + height / 2);
        }
        public void Physics(Vector2 position, Vector2 velocity, Vector2 acceleration){
            Position = position;
                        Velocity = velocity;
            Acceleration = acceleration;
            
            min.X = (int)Position.X;
            min.Y = (int)Position.Y;
            max.X = (int)Position.X + (int)width;
            max.Y = (int)Position.Y + (int)height;
            OccupyingCells();
        }

        private void OccupyingCells()
        { 
            float c1;
            float c2;
            float c3;
            float c4;
            cells.Clear();
            c1 = position.X / 32;
            c2 = position.Y / 32;
            c3 = (position.X + width)/ 32;
            c4 = (position.Y + height) / 32;
            Cells.Add(new Point((int)c1, (int)c2));
            Cells.Add(new Point((int)c1, (int)c4));
            Cells.Add(new Point((int)c3, (int)c4));
            Cells.Add(new Point((int)c3, (int)c2));
        }

        public void DrawBox(SpriteBatch spriteBatch, int thicknessOfBorder, Color borderColor)
        {
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            spriteBatch.Draw(pixel, new Rectangle((int)this.X, (int)this.Y, (int)this.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle((int)this.X,(int)this.Y, thicknessOfBorder, (int)this.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle(((int)this.X + (int)this.Width - thicknessOfBorder),
                                            (int)this.Y,
                                            thicknessOfBorder,
                                            (int)this.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle((int)this.X,
                                            (int)this.Y + (int)this.Height - thicknessOfBorder,
                                            (int)this.Width,
                                            thicknessOfBorder), borderColor);

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
