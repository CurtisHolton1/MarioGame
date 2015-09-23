using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public class ItemFactory
    {
        Texture2D texture;
        Rectangle sourceRect;
        int numberOfFrames;
        int timePerFrame;
        Game1 game;

        enum spriteType { fireFlower = 1, superMushroom = 2, star = 3, coin = 4, oneUp = 5};
        spriteType sprite = spriteType.fireFlower;
        public ItemFactory(Game1 game)
        {
            this.texture = game.Content.Load<Texture2D>("items-objects");
            this.game = game;
        }
        public Sprite MakeProduct(int x)
        {
            sprite = (spriteType)x;

            sourceRect.Width = 16;
            sourceRect.X = 0;
            sourceRect.Y = 0;
            sourceRect.Height = 16;
            timePerFrame = 0;
            switch (sprite)
            {
                case spriteType.fireFlower:
                    {
                        sourceRect.Width = 64;
                        sourceRect.Height = 16;
                        sourceRect.X = 0;
                        sourceRect.Y = 32;
                        timePerFrame = 250;
                        numberOfFrames = 4;
                        Sprite fireFlower = new FlowerSprite(game.scene, Vector2.Zero, texture, sourceRect, timePerFrame, numberOfFrames, true);
                        return fireFlower;
                    }
                case spriteType.superMushroom:
                    {
                        sourceRect.X = 0;
                        sourceRect.Y = 16;
                        sourceRect.Width = 16;
                        sourceRect.Height = 16;
                        Sprite superShroom = new MushroomSprite(game.scene, Vector2.Zero, texture, sourceRect, timePerFrame, numberOfFrames, false);
                        game.scene.MovSprites.Add(superShroom);
                        return superShroom;
                    }
                case spriteType.star:
                    {
                        sourceRect.X = 0;
                        sourceRect.Y = 48;
                        sourceRect.Width = 64;
                        sourceRect.Height = 16;
                        timePerFrame = 250;
                        numberOfFrames = 4;
                        Sprite star = new StarSprite(game.scene, Vector2.Zero, texture, sourceRect, timePerFrame, numberOfFrames, true);
                        return star;
                    }
                case spriteType.coin:
                    {
                        sourceRect.X = 0;
                        sourceRect.Y = 82;
                        sourceRect.Width = 64;
                        sourceRect.Height = 14;
                        timePerFrame = 250;
                        numberOfFrames = 4;
                        Sprite coin = new CoinSprite(game.scene, Vector2.Zero, texture, sourceRect, timePerFrame, numberOfFrames, true);
                        return coin;
                    }
                case spriteType.oneUp: 
                    {
                        sourceRect.X =16 ;
                        sourceRect.Y = 16;
                        sourceRect.Width = 16;
                        sourceRect.Height = 16;
                        Sprite oneUp = new OneUpSprite(game.scene, Vector2.Zero, texture, sourceRect, timePerFrame, numberOfFrames, false);
                        game.scene.MovSprites.Add(oneUp);
                        return oneUp;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
