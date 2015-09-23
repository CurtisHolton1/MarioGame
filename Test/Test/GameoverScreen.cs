using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class GameOverScreen
    {
        private Texture2D texture;
        private Game1 game;
        private KeyboardState oldState;

        public GameOverScreen(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("gameover");
            oldState = Keyboard.GetState();
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
            {
                game.ResetCommand();
            }
            else if (keyboardState.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
            {
                game.Exit();
            }

            oldState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);
            spriteBatch.End();
        }
    }
}
