using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public class BlockFactory
    {
     
        Texture2D texture;
        Texture2D texture2;
        Rectangle sourceRect;
        int numberOfFrames;
        int timePerFrame;
        Game1 game;
        enum spriteType { questionBlock = 1, brickBlock = 2, floorBlock = 3, pyramidBlock = 4, usedBlock = 5, hiddenBlock = 6, brokenBlock = 7, flagBulb = 8, flagPole = 9, flag =10};
        spriteType sprite = spriteType.usedBlock;
        public BlockFactory(Game1 game)
        {
            this.texture = game.Content.Load<Texture2D>("tiles-2");
            this.texture2 = game.Content.Load<Texture2D>("items-objects");
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
                    case spriteType.questionBlock:
                    {               
                        numberOfFrames = 3;
                        timePerFrame = 250;
                        sourceRect.X = 368;
                        sourceRect.Width = 48;
                        Sprite blockQuestion = new BlockQuestionSprite(game.scene, Vector2.Zero, texture, sourceRect, timePerFrame, numberOfFrames,true);
                        return blockQuestion;
                    }
                    case spriteType.brickBlock:
                    {
                        sourceRect.X = 30;
                        Sprite blockBrick = new BlockBrickSprite(game.scene, Vector2.Zero, texture, sourceRect, 0, numberOfFrames, false);
                        return blockBrick;
                    }

                    case spriteType.brokenBlock:
                    {
                        sourceRect.X = 64;
                        sourceRect.Y = 0;
                        sourceRect.Width = 16;
                        sourceRect.Height = 16;
                        Sprite blockBrick = new BlockBrokenSprite(game.scene, Vector2.Zero, texture2, sourceRect, 0, numberOfFrames, false);
                        return blockBrick;
                    }
                    case spriteType.floorBlock:
                    {
                        Sprite blockFloor = new BlockFloorSprite(game.scene, Vector2.Zero, texture, sourceRect, 0, numberOfFrames, false);
                        return blockFloor;
                    }
                    case spriteType.pyramidBlock:
                    {
                        sourceRect.Y = 16;
                        Sprite blockPyramid = new BlockPyramidSprite(game.scene, Vector2.Zero, texture, sourceRect, 0, numberOfFrames, false);
                        return blockPyramid;
                    }
                    case spriteType.usedBlock:
                    {
                        sourceRect.X = 48;
                        Sprite blockUsed = new BlockUsedSprite(game.scene, Vector2.Zero, texture, sourceRect, 0, numberOfFrames, false);
                        return blockUsed;
                    }
                    case spriteType.hiddenBlock:
                    {
                        sourceRect.X = 432;
                        Sprite blockHidden = new BlockHiddenSprite(game.scene, Vector2.Zero, texture, sourceRect, 0, numberOfFrames, false);
                        return blockHidden;
                    }
                    case spriteType.flagBulb:
                    {
                        sourceRect.X = 256;
                        sourceRect.Y = 136;
                        sourceRect.Width = 16;
                        sourceRect.Height = 8;
                        Sprite bulb = new FlagBulbSprite(game.scene, Vector2.Zero, texture, sourceRect, 0, numberOfFrames, false);
                        return bulb;
                    }
                    case spriteType.flagPole:
                    {
                        sourceRect.X = 256;
                        sourceRect.Y = 145;
                        sourceRect.Width = 15;
                        sourceRect.Height = 16;
                        Sprite pole = new FlagPoleSprite(game.scene, Vector2.Zero, texture, sourceRect, 0, numberOfFrames, false);
                        return pole;
                    }
                    case spriteType.flag: {
                        sourceRect.X = 191;
                        sourceRect.Y = 322;
                        sourceRect.Width =16;
                        sourceRect.Height = 16;
                        Sprite flag = new FlagSprite(game.scene, Vector2.Zero, texture, sourceRect, 0, numberOfFrames, false);
                        return flag;
                   }

                    default:
                    {
                        return null;
                    }
                }
        }
    }
}
