using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    public class LevelManager
    {
        private Texture2D texture;
        private Game1 game;
        private KeyboardState oldState;
        private SpriteFont font;
        public bool choose;

        public LevelManager(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("LevelManager");
            oldState = Keyboard.GetState();
            font = game.Content.Load<SpriteFont>("LevelFonts");
            choose = false;
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.F1) && oldState.IsKeyUp(Keys.F1))
            {
                game.scene.Map = 1;
                game.scene.ChooseContent();
                choose = true;
            }
            else if (keyboardState.IsKeyDown(Keys.F2) && oldState.IsKeyUp(Keys.F2))
            {
                game.scene.Map = 2;
                game.scene.ChooseContent();
                choose = true;
            }
            else if (keyboardState.IsKeyDown(Keys.F3) && oldState.IsKeyUp(Keys.F3))
            {
                game.scene.Map = 3;
                game.scene.ChooseContent();
                choose = true;
            }

            oldState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Color.White;
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);
            spriteBatch.DrawString(font, "Welcome to our Mario Game!", new Vector2(10.0f, 25.0f), color);
            spriteBatch.DrawString(font, "Press f1 for Level 1", new Vector2(10.0f, 100.0f), color);
            spriteBatch.DrawString(font, "Press f2 for Level 2", new Vector2(10.0f, 150.0f), color);
            spriteBatch.DrawString(font, "Press f3 for Level 3", new Vector2(10.0f, 200.0f), color);
            spriteBatch.DrawString(font, "Gameplay tips:", new Vector2(10.0f, 300.0f), color);
            spriteBatch.DrawString(font, "Press F to spend coins and lay down blocks!", new Vector2(10.0f, 330.0f), color);
            spriteBatch.DrawString(font, "Press H to destroy blocks and gain coins!", new Vector2(10.0f, 360.0f), color);
            spriteBatch.DrawString(font, "Watch out - Gravity will sometimes change!", new Vector2(10.0f, 390.0f), color);
            spriteBatch.End();
        }

    }
}
