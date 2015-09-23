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
    public class Scene
    {
        int screenWidth;
        int screenHeight;
        //Texture2D backgroundTexture;
        Game1 game;
        public Game1 Game
        {
            get { return game; }
        }

        SpriteBatch spriteBatch;
        IController keyboardCont;
        IController gamePadCont;
        public Mario mario;
        List<ISprite> sprites;
        List<Sprite> ssprites;
        List<Sprite> movSprites;
       public  MarioCollision collision;
        OtherCollision sec_collision;
        Quadtree quad;
        Quadtree secquad;
        public SoundEffectInstance instance;
        SpriteFont Font1;
        bool isPaused;
        bool isMuted;
        bool victory; 
        int map;
        public int Map
        {
            get { return map; }
            set { map = value; }
        }
        float gravity = 10f;
        public float Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }
        public bool Victory
        { 
            get { return victory; } 
            set { victory = value; } 
        }
        bool defeat;
        Parser parser;
        VictoryScreen victoryScreen;
        GameOverScreen gameOverScreen;
        public LevelManager levelManager;
        HUD hud;

        public List<ISprite> Sprites { get { return sprites; } set { sprites = value; } }

        public List<Sprite> SSprites { get { return ssprites; } set { ssprites = value; } }

        public List<Sprite> MovSprites { get { return movSprites; } set { movSprites = value; } }

        private Camera _camera;
        //private readonly GraphicsDeviceManager _graphics;
        private Layer[] _layers;

        public Scene(Game1 game)
        {
            this.game = game;

        }
        public void Mute()
        {
            if (!isMuted)
            {
                isMuted = true;
                instance.Pause();
            }
            else
            {
                isMuted = false;
                instance.Play();
            }
        }
        public void Pause()
        {
            
            if (!isPaused)
            {
                isPaused = true;
                instance.Pause();
            }
            else
            {
                isPaused = false;
                instance.Play();
            }
        }

        public Vector2 FindLatestCheckpoint()
        {
            Vector2 latest = parser.LatestCheckpoint.ElementAt(0);
            foreach (Vector2 v in parser.LatestCheckpoint)
            {
                if (mario.MarioVec_X > v.X && v.X > latest.X)
                {
                    latest = v;
                }
            }
            return latest;
        }

        public void VictoryC()
        {
            victory = true;
        }

        public void Gameover()
        {
            defeat = true;
        }
        public void PlaceBlockCommand()
        {
            if (mario.Coins > 0)
            {
                BlockFactory bf = new BlockFactory(game);
                Sprite newBlock = bf.MakeProduct(2);
                if (mario.Direction == 1)
                {
                    newBlock.CollisionBox.Physics(new Vector2(mario.MarioVec_X + mario.CollisionBox.Width + 1, mario.MarioVec_Y - mario.CollisionBox.Height), Vector2.Zero, Vector2.Zero);
                }
                else
                {
                    newBlock.CollisionBox.Physics(new Vector2(mario.MarioVec_X - 33, mario.MarioVec_Y - mario.CollisionBox.Height), Vector2.Zero, Vector2.Zero);
                }
                foreach (ISprite s in SSprites)
                {
                    if (newBlock.CollisionBox.Intersect(newBlock.CollisionBox, s.CollisionBox))
                    {
                        newBlock.alive = false;
                        return;
                    }
                }
                SSprites.Add(newBlock);
                Sprites.Add(newBlock);
                mario.Coins--;
            } 
        }

        public void DestroyBlockCommand()
        {
            for (int i = 0; i < SSprites.Count; i++)
            {
                Sprite s = SSprites[i];
                if (s is BlockBrickSprite)
                {
                    int xDifference;
                    int yDifference = (int)Math.Abs((mario.MarioVec_Y - s.CollisionBox.Y));
                    
                    if (mario.Direction == 1) 
                    {
                        xDifference = (int)(s.Position.X - mario.CollisionBox.Max_X);
                        Console.WriteLine(yDifference + " " + mario.MarioVec_Y);
                        if (xDifference <= 3 && xDifference >= 0 && yDifference <= mario.CollisionBox.Height) {
                            SSprites.Remove(s);
                            Sprites.Remove(s);
                            mario.Coins++;
                        }
                    }
                    else if (mario.Direction == 0) 
                    {
                        xDifference = (int)((s.Position.X + s.CollisionBox.Width) - mario.MarioVec_X);
                        if (xDifference >= -3 && xDifference <= 0 && yDifference <= mario.CollisionBox.Height)
                        {
                            SSprites.Remove(s);
                            Sprites.Remove(s);
                            mario.Coins++;
                        }
                    }
                }
            }      
        }
        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            levelManager = new LevelManager(Game);
            victoryScreen = new VictoryScreen(game);
            gameOverScreen = new GameOverScreen(game);

            mario = new Mario(game, this);
            isPaused = false;
            isMuted = false;
            victory = false;
            defeat = false;
            SoundEffect effect = game.Content.Load<SoundEffect>("OriginalThemeSong");
            instance = effect.CreateInstance();
            instance.IsLooped = true;
            instance.Play();


            ////////////////////////////

            //this.Mute();
            ////////////////////////////////

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            Font1 = game.Content.Load<SpriteFont>("MarioPoints");
            keyboardCont = new KeyboardController(game, mario);
            gamePadCont = new GamePadController(game, mario);
            // TODO: use this.Content to load your game content here

        }

        public void ChooseContent()
        {
            sprites = new List<ISprite>();
            movSprites = new List<Sprite>();
            ssprites = new List<Sprite>();
            // Create a camera instance and limit its moving range
            _camera = new Camera(game.GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, 4000, 600) };

            // Create 9 layers with parallax ranging from 0% to 100% (only horizontal)
            _layers = new Layer[9];
            hud = new HUD(game, mario);
            parser = new Parser(game);
            if (Map == 1)
            {
                parser.Parse(mario, sprites, movSprites, ssprites, _layers, _camera, "map.xml");

            }
            else if (Map == 2)
            {
                parser.Parse(mario, sprites, movSprites, ssprites, _layers, _camera, "map2.xml");
            }
            else if (Map == 3)
            {
                parser.Parse(mario, sprites, movSprites, ssprites, _layers, _camera, "map3.xml");
            }
            quad = new Quadtree(0, new CollisionBox(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height));
            secquad = new Quadtree(0, new CollisionBox(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height));


            collision = new MarioCollision(mario, this);
            sec_collision = new OtherCollision(mario, this, secquad);
        }


        public void ResetContent()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            sprites = new List<ISprite>();
            movSprites = new List<Sprite>();
            ssprites = new List<Sprite>();
            victoryScreen = new VictoryScreen(game);
            gameOverScreen = new GameOverScreen(game);

            mario = new Mario(game, this);
            // Create a camera instance and limit its moving range
            _camera = new Camera(game.GraphicsDevice.Viewport);

            // Create 9 layers with parallax ranging from 0% to 100% (only horizontal)
            _layers = new Layer[9];
            hud = new HUD(game, mario);
            parser = new Parser(game);
            if (Map == 1)
            {
                parser.Parse(mario, sprites, movSprites, ssprites, _layers, _camera, "map.xml");

            }
            else if (Map == 2)
            {
                parser.Parse(mario, sprites, movSprites, ssprites, _layers, _camera, "map2.xml");
            }
            else if (Map == 3)
            {
                parser.Parse(mario, sprites, movSprites, ssprites, _layers, _camera, "map3.xml");
            }

            isPaused = false;
            isMuted = false;
            victory = false;
            defeat = false;
            SoundEffect effect = game.Content.Load<SoundEffect>("OriginalThemeSong");
            instance = effect.CreateInstance();
            instance.IsLooped = true;
            instance.Play();

            quad = new Quadtree(0, new CollisionBox(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height));
            secquad = new Quadtree(0, new CollisionBox(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height));


            collision = new MarioCollision(mario, this);
            sec_collision = new OtherCollision(mario, this, secquad);
            ////////////////////////////

            //this.Mute();
            ////////////////////////////////

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            Font1 = game.Content.Load<SpriteFont>("MarioPoints");
            keyboardCont = new KeyboardController(game, mario);
            gamePadCont = new GamePadController(game, mario);
            // TODO: use this.Content to load your game content here

        }
        public void Update(GameTime gameTime)
        {
            
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            keyboardCont.UpdateInput(elapsed);
            gamePadCont.UpdateInput(elapsed);
            if (!isPaused && !defeat && !victory && levelManager.choose)
            {
                //foreach (Sprite sprite in sprites)
                //{
                //    sprite.Update(elapsed, 0);
                //}

                for (int i = 0; i < sprites.Count; i++)
                {
                    sprites.ElementAt(i).Update(elapsed, 0);
                }
                    mario.Update(elapsed, game.graphicsdevice, collision);
                _camera.LookAt(mario.MarioVec);
                hud.Update(gameTime);
                ///////////////////////////////////////////////

                collision.Update();
              //  sec_collision.Update();
                
                /////////////////////////////////////////////////
            }

            else if (!levelManager.choose)
            {
                levelManager.Update();
            }

            else if (defeat == true)
            {
                gameOverScreen.Update();
                hud.Update(gameTime);
            }
            else if (victory == true)
            {
                victoryScreen.Update();
                hud.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (!defeat && !victory && levelManager.choose)
            {

                //  game.GraphicsDevice.Clear(Color.Black);
                foreach (Layer layer in _layers)
                    layer.Draw(spriteBatch);
                
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetViewMatrix(new Vector2(1.0f, 1.0f)));
               
                for (int index = 0; index < sprites.Count; index++)
                {
                    sprites[index].Draw(spriteBatch);
                }
                mario.Draw(spriteBatch);
              //  mario.CollisionBox.DrawBox(spriteBatch, 1, Color.Yellow);
            //    collision.VarBox.DrawBox(spriteBatch, 1, Color.Red);
              //  collision.Floorbox.DrawBox(spriteBatch, 1, Color.Green);
                spriteBatch.End();
                hud.Draw(spriteBatch);
              
            }
            else if (!levelManager.choose)
            {
                levelManager.Draw(spriteBatch);
            }
            else if (defeat)
            {
                gameOverScreen.Draw(spriteBatch);
            }
            else if (victory)
            {
                victoryScreen.Draw(spriteBatch);
            }
        }
    }
}