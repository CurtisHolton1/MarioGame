using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class BGSprite: Sprite
    {
        public BGSprite(Vector2 pos, Texture2D tex) : base(pos, tex) { 
            CollisionBox = new CollisionBox(pos.X, pos.Y, 0, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        { 
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }
    }
}
