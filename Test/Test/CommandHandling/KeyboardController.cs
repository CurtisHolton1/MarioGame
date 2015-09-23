using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MarioGame;

namespace MarioGame
{
    class KeyboardController : IController
    {
        private Dictionary<int, ICommand> keyBoardCommands = new Dictionary<int, ICommand>();
        //Game1 game;
        KeyboardState oldState;
        float time,timeSinceToggle;

        public KeyboardController(Game1 game, Mario mario)
        {
            time = 0;
            timeSinceToggle = 0;
            oldState = Keyboard.GetState(PlayerIndex.One);
            //action state keys
            keyBoardCommands.Add((int)Keys.Up, new UpCommand(mario));
            keyBoardCommands.Add((int)Keys.W, new UpCommand(mario)); 
            keyBoardCommands.Add((int)Keys.Down, new DownCommand(mario));
            keyBoardCommands.Add((int)Keys.S, new DownCommand(mario));
            keyBoardCommands.Add((int)Keys.Left, new LeftCommand(mario));
            keyBoardCommands.Add((int)Keys.A, new LeftCommand(mario));
            keyBoardCommands.Add((int)Keys.Right, new RightCommand(mario));
            keyBoardCommands.Add((int)Keys.D, new RightCommand(mario));
            keyBoardCommands.Add((int)Keys.Up & (int)Keys.Left, new UpLeftCommand(mario));
            keyBoardCommands.Add((int)Keys.Space, new FireballCommand(mario));
            keyBoardCommands.Add((int)Keys.F, new PlaceBlockCommand(game.Scene));
            keyBoardCommands.Add((int)Keys.H, new DestroyBlockCommand(game.Scene));

            //power up state keys
            keyBoardCommands.Add((int)Keys.Y, new StandardCommand(mario));
            keyBoardCommands.Add((int)Keys.U, new SuperCommand(mario));
            keyBoardCommands.Add((int)Keys.I, new FireCommand(mario));
            keyBoardCommands.Add((int)Keys.O, new DeadCommand(mario));           
            //Game commands
            keyBoardCommands.Add((int)Keys.R, new ResetCommand(game));
            keyBoardCommands.Add((int)Keys.P, new PauseCommand(game));
            keyBoardCommands.Add((int)Keys.Q, new QuitCommand(game));
            keyBoardCommands.Add((int)Keys.M, new MuteCommand(game));
            keyBoardCommands.Add((int)Keys.V, new VictoryCommand(game));
            keyBoardCommands.Add((int)Keys.G, new GameoverCommand(game));

        }

        public void UpdateInput(float elapsed)
        {
            KeyboardState state = Keyboard.GetState(PlayerIndex.One);
            Keys[] keys = state.GetPressedKeys();
            
            time += elapsed;
            timeSinceToggle += elapsed;
            
            if (keys.Length >0)
            {
                if (time >50)
                {
                    if (keyBoardCommands.ContainsKey((int)keys[0]))
                    {
                        if(keyBoardCommands[(int)keys[0]].Toggled) {
                            if (timeSinceToggle > 500)
                            {
                                keyBoardCommands[(int)keys[0]].Execute();
                                timeSinceToggle = 0;
                            }
                        }
                        else
                            keyBoardCommands[(int)keys[0]].Execute();
                    }
                    time = 0;
                }
            }
        }
    }
}
