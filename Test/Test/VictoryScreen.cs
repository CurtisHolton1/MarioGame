using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class VictoryScreen
    {
        private Texture2D texture;
        private Game1 game;
        private KeyboardState oldState;

        public VictoryScreen(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("victory");
            oldState = Keyboard.GetState();
        }

        public void Update()
        {
            if (game.scene.Map == 1)
            {
                game.scene.Map += 1;
                game.ResetCommand();
            }
            else if (game.scene.Map == 2)
            {
                game.scene.Map += 1;
                game.ResetCommand();
            }
            else if (game.scene.Map == 3)
            {
                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    game.scene.LoadContent();
                }
                else if (keyboardState.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
                {
                    game.Exit();
                }

                oldState = keyboardState;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);
            spriteBatch.End();
        }
    }
}
