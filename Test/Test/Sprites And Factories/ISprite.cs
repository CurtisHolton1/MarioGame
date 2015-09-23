using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    public interface ISprite 
    {

        CollisionBox CollisionBox { get; set; }
        Vector2 Position { get; set; }
        
        bool IsBlock { get; }
        
     
        void Update(float elapsed, int direction);
        void Draw(SpriteBatch spriteBatch);

        void HandleCollision(ISprite movSprite);
  
 

    }
}
