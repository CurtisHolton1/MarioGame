using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioGame
{
    class HUD
    {
        private int points;
        private int coins;
        private int lives;
        private double time;
        private Mario mario;
        private float spacing;
        private ItemFactory itemf;
        private MarioFactory mf;
        private MarioSprite ms;
        private Sprite coinSprite;
        private Game1 game;
        private SpriteFont font;
        private SoundMachine sounds;

        public HUD(Game1 game, Mario mario) {
            this.game = game;
            this.mario = mario;
            font = game.Content.Load<SpriteFont>("MarioPoints");
            time = 400;
            sounds = new SoundMachine(game);
            mf = new MarioFactory(game);
            itemf = new ItemFactory(game);
            ms = mf.MakeProduct(1);
            coinSprite = itemf.MakeProduct(4);   
            spacing = game.GraphicsDevice.Viewport.Width / 4;
            coinSprite.Position = new Vector2(spacing, 24);
            ms.Position = new Vector2(spacing*2, 24);
        }

        public void Update(GameTime gameTime) {
            time = 400.0 - gameTime.TotalGameTime.Seconds;
            if (time == 50.0)
            {
                sounds.PlayWarning();
                if (time == 0.1) 
                {
                    //game.scene.Pause();
                    time = 0.0;
                    mario.toDead();
                }
            }
            points = mario.Points;
            coins = mario.Coins;
            if (coins >= 100)
            {
                mario.Coins = 0;
                mario.Lives += 1;
            }
            lives = mario.Lives;
            
        }
        public void Draw(SpriteBatch spriteBatch) {
            Color color = Color.DarkBlue;
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Mario", new Vector2(64.0f, 32.0f), color);
            spriteBatch.DrawString(font, points.ToString("D6"), new Vector2(64.0f, 48.0f), color);
            coinSprite.Draw(spriteBatch);
            spriteBatch.DrawString(font, coins.ToString("D2"), new Vector2(spacing+40, 32.0f),color);
            ms.Draw (spriteBatch);
            spriteBatch.DrawString(font, " X " + lives.ToString(), new Vector2((spacing*2)+40, 32.0f), color);
            spriteBatch.DrawString(font, "TIME", new Vector2(spacing*3, 32), color);
            spriteBatch.DrawString(font, time.ToString(), new Vector2(spacing*3, 48.0f), color);
            spriteBatch.End(); 
        }
    }
}
