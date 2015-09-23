using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MarioGame
{
    public class Parser
    {
        BlockFactory blockFactory;
        ItemFactory itemFactory;
        EnemyFactory enemyFactory;
        Game1 game;
        private List<Vector2> latestCheckpoint;
        public List<Vector2> LatestCheckpoint
        {
            get { return latestCheckpoint; }
            set { latestCheckpoint = value; }
        }

        public Parser(Game1 game)
        {
            this.game = game;
            blockFactory = new BlockFactory(game);
            itemFactory = new ItemFactory(game);
            enemyFactory = new EnemyFactory(game);
            latestCheckpoint = new List<Vector2>();
        }


        public void Parse(Mario mario, List<ISprite> sprites, List<Sprite> movSprites, List<Sprite> ssprites, Layer[] _layers, Camera _camera, String file)
        {
            //Get the list of checkpoints
            XmlReader reader = XmlReader.Create(file);
            int type;
            int posX;
            int posY;
            float scroll;
            float visibility;
            String str;
            String tex;
            Sprite sprite;
            Vector2 vec;
            Layer layer;
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "block":
                        {
                            if (reader.IsStartElement())
                            {
                                str = reader.GetAttribute("type");
                                type = Convert.ToInt32(str);
                                str = reader.GetAttribute("posX");
                                posX = Convert.ToInt32(str);
                                posX *= 32;
                                str = reader.GetAttribute("posY");
                                posY = Convert.ToInt32(str);
                                posY *= 32;
                                vec = new Vector2(posX, posY);
                                sprite = blockFactory.MakeProduct(type);
                                sprite.CollisionBox.Physics(vec, Vector2.Zero, Vector2.Zero);
                                sprite.ContainsItem = Convert.ToInt32(reader.GetAttribute("containsItem"));
                                sprite.ContainsCoin = Convert.ToInt32(reader.GetAttribute("containsCoin"));
                                sprites.Add(sprite);
                                ssprites.Add(sprite);
                                //check for coins and items
                            }
                            break;
                        }
                    case "mario":
                        {
                            if (reader.IsStartElement())
                            {
                                str = reader.GetAttribute("posX");
                                posX = Convert.ToInt32(str);
                                str = reader.GetAttribute("posY");
                                posY = Convert.ToInt32(str);
                                vec = new Vector2(posX, posY);
                                mario.MarioVec = vec;
                            }
                            break;
                        }
                    case "camera":
                        {
                            if (reader.IsStartElement())
                            {
                                int width;
                                int height;
                                str = reader.GetAttribute("posX");
                                posX = Convert.ToInt32(str);
                                posX *= 32;
                                str = reader.GetAttribute("posY");
                                posY = Convert.ToInt32(str);
                                posY *= 32;
                                str = reader.GetAttribute("width");
                                width = Convert.ToInt32(str);
                                str = reader.GetAttribute("height");
                                height = Convert.ToInt32(str);
                                _camera.Limits = new Rectangle(posX, posY, width, height);
                            }
                            break;
                        }
                    case "item":
                        {
                            if (reader.IsStartElement())
                            {
                                str = reader.GetAttribute("type");
                                type = Convert.ToInt32(str);
                                sprite = itemFactory.MakeProduct(type);
                                str = reader.GetAttribute("posX");
                                posX = Convert.ToInt32(str);
                                posX *= 32;
                                str = reader.GetAttribute("posY");
                                posY = Convert.ToInt32(str);
                                posY *= 32;
                                vec = new Vector2(posX, posY);
                                sprite.CollisionBox.Physics(vec, Vector2.Zero, Vector2.Zero);
                                sprites.Add(sprite);
                                ssprites.Add(sprite);
                            }
                            break;
                        }
                    case "enemy":
                        {
                            if (reader.IsStartElement())
                            {
                                str = reader.GetAttribute("type");
                                type = Convert.ToInt32(str);
                                sprite = enemyFactory.MakeProduct(type);
                                str = reader.GetAttribute("posX");
                                posX = Convert.ToInt32(str);
                                posX *= 32;
                                str = reader.GetAttribute("posY");
                                posY = Convert.ToInt32(str);
                                posY *= 32;
                                posY -= 1;
                                vec = new Vector2(posX, posY);
                                sprite.CollisionBox.Physics(vec, Vector2.Zero, Vector2.Zero);
                                sprites.Add(sprite);
                                movSprites.Add(sprite);
                                ssprites.Add(sprite);
                            }
                            break;
                        }
                    case "floor":
                        {
                            if (reader.IsStartElement())
                            {                    
                                str = reader.GetAttribute("start");
                                int start = Convert.ToInt32(str);
                                str = reader.GetAttribute("num");
                                int num = Convert.ToInt32(str);
                                posY = 17*32;
                                for (int i = start; i < num+start; i++)
                                {
                                    posX = i * 32;
                                    sprite = blockFactory.MakeProduct(3);
                                    vec = new Vector2(posX, posY);
                                    sprite.CollisionBox.Physics(vec, Vector2.Zero, Vector2.Zero);
                                    sprites.Add(sprite);
                                    ssprites.Add(sprite);
                                    sprite = blockFactory.MakeProduct(3);
                                    vec = new Vector2(posX, posY + 32);
                                    sprite.CollisionBox.Physics(vec, Vector2.Zero, Vector2.Zero);
                                    sprites.Add(sprite);
                                    ssprites.Add(sprite);
                                }
                            }
                            break;
                        }
                    case "backgroundLayer":
                        {
                            if (reader.IsStartElement())
                            {
                                str = reader.GetAttribute("scroll");
                                scroll = float.Parse(str);
                                str = reader.GetAttribute("visibility");
                                visibility = float.Parse(str);
                                str = reader.GetAttribute("num");
                                type = Convert.ToInt32(str);
                                layer = new Layer(_camera) { Parallax = new Vector2(scroll, visibility) };
                                _layers[type] = layer;
                            }
                            break;
                        }
                    case "backgroundSprite":
                        {
                            if (reader.IsStartElement())
                            {
                                str = reader.GetAttribute("texture");
                                tex = str;
                                str = reader.GetAttribute("num");
                                type = Convert.ToInt32(str);
                                str = reader.GetAttribute("posX");
                                posX = Convert.ToInt32(str);
                                posX *= 32;
                                str = reader.GetAttribute("posY");
                                posY = Convert.ToInt32(str);
                                posY *= 32;
                                _layers[type].Sprites.Add(new BGSprite(new Vector2(posX, posY), game.Content.Load<Texture2D>(tex)));
                            }
                            break;
                        }
                    case "triangle":
                        {
                            if (reader.IsStartElement())
                            {
                                str = reader.GetAttribute("type");
                                type = Convert.ToInt32(str);
                                posX = Convert.ToInt32(reader.GetAttribute("posX")) *32;
                                posY = Convert.ToInt32(reader.GetAttribute("posY"))*32;
                                int width = Convert.ToInt32(reader.GetAttribute("width"));
                                int height = Convert.ToInt32(reader.GetAttribute("height"));
                                bool reverse = Convert.ToBoolean(reader.GetAttribute("reverse"));
                                if (reverse)
                                {
                                    for (int j = 0; j < height; j++)
                                    {
                                        for (int i = 0; i < width - j; i++)
                                        {
                                            sprite = blockFactory.MakeProduct(type);
                                            sprite.CollisionBox.Physics(new Vector2(posX + i * 32, posY - j * 32), Vector2.Zero, Vector2.Zero);
                                            sprites.Add(sprite);
                                            ssprites.Add(sprite);
                                        }
                                    }
                                }
                                else
                                {
                                    for (int j = height; j >= 0; j--)
                                    {
                                        for (int i = width; i >0 + j; i--)
                                        {
                                            
                                            sprite = blockFactory.MakeProduct(type);
                                            sprite.CollisionBox.Physics(new Vector2(posX + (i - width) * 32, posY - j * 32), Vector2.Zero, Vector2.Zero);
                                            sprites.Add(sprite);
                                            ssprites.Add(sprite);
                                        }
                                    }
                                }
                            }
                            break;
                        }
                        
                    case "row":
                        {
                            if (reader.IsStartElement())
                            {
                                str = reader.GetAttribute("type");
                                type = Convert.ToInt32(str);
                                posX = Convert.ToInt32(reader.GetAttribute("posX")) * 32;
                                posY = Convert.ToInt32(reader.GetAttribute("posY")) * 32;
                                int num = Convert.ToInt32(reader.GetAttribute("num"));
                                for (int i = 0; i < num; i++)
                                {
                                    sprite = blockFactory.MakeProduct(type);
                                    sprite.CollisionBox.Physics(new Vector2(posX + i*32, posY), Vector2.Zero, Vector2.Zero);
                                    sprites.Add(sprite);
                                    ssprites.Add(sprite);
                                }
                            }
                            break;
                        }
                    case "flag":
                        {
                            if (reader.IsStartElement())
                            {
                                posX = Convert.ToInt32(reader.GetAttribute("posX")) * 32;
                                posY = Convert.ToInt32(reader.GetAttribute("posY")) * 32;
                                sprite = blockFactory.MakeProduct(8);
                                sprite.CollisionBox.Physics(new Vector2(posX, posY), Vector2.Zero, Vector2.Zero);
                                sprites.Add(sprite);
                                ssprites.Add(sprite);
                                posY += (int)sprite.CollisionBox.Height;
                                for (int i = 0; i < 7; i++)
                                {
                                    sprite = blockFactory.MakeProduct(9);
                                    sprite.CollisionBox.Physics(new Vector2(posX, posY+i*32), Vector2.Zero, Vector2.Zero);
                                    sprites.Add(sprite);
                                    ssprites.Add(sprite);
                                }
                                sprite = blockFactory.MakeProduct(10);
                                sprite.CollisionBox.Physics(new Vector2(posX-16, posY), Vector2.Zero, Vector2.Zero);
                                sprites.Add(sprite);
                                ssprites.Add(sprite);
                            }
                            break;
                        }
                    case "checkpoints":
                        {
                            if (reader.IsStartElement())
                            {
                                this.latestCheckpoint.Add(new Vector2(Convert.ToInt32(reader.GetAttribute("ch1X")),Convert.ToInt32(reader.GetAttribute("ch1Y"))));
                                this.latestCheckpoint.Add(new Vector2(Convert.ToInt32(reader.GetAttribute("ch2X")), Convert.ToInt32(reader.GetAttribute("ch2Y"))));
                                this.latestCheckpoint.Add(new Vector2(Convert.ToInt32(reader.GetAttribute("ch3X")), Convert.ToInt32(reader.GetAttribute("ch3Y"))));
                                this.latestCheckpoint.Add(new Vector2(Convert.ToInt32(reader.GetAttribute("ch4X")), Convert.ToInt32(reader.GetAttribute("ch4Y"))));
                            }
                            break;
                        }
                    case "gravity":
                        {
                            if (reader.IsStartElement())
                            {
                                str = reader.GetAttribute("accelY");
                                game.scene.Gravity = (float)Convert.ToDouble(str);
                            }
                            break;
                        }
                }
            }
        }

    }
}

