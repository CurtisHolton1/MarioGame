using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace MarioGame
{
    class GamePadController : IController
    {
        private Dictionary<int, ICommand> GamepadCommands = new Dictionary<int, ICommand>();
        GamePadState oldState;
        public GamePadController(Game1 game, Mario mario)
        {
            oldState = GamePad.GetState(PlayerIndex.One);
            
                GamepadCommands.Add((int)Buttons.DPadUp, new UpCommand(mario));
                GamepadCommands.Add((int)Buttons.DPadDown, new DownCommand(mario));
                GamepadCommands.Add((int)Buttons.DPadLeft, new LeftCommand(mario));
                GamepadCommands.Add((int)Buttons.DPadRight, new RightCommand(mario));
                GamepadCommands.Add((int)Buttons.B, new FireballCommand(mario));
                GamepadCommands.Add((int)Buttons.Start, new PauseCommand(game));
                GamepadCommands.Add((int)Buttons.Back, new QuitCommand(game));
                GamepadCommands.Add((int)Buttons.LeftShoulder, new MuteCommand(game));
                GamepadCommands.Add((int)Buttons.RightShoulder, new ResetCommand(game));
    

            
        }

        public void UpdateInput(float elapsed)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));
            foreach (var button in buttonList)
            {
                if (state.IsButtonDown(button) &&
                            !oldState.IsButtonDown(button))
                    if (GamepadCommands.ContainsKey((int)button))
                    {
                        GamepadCommands[(int)button].Execute();
                    }
                    else { }
            }
            oldState = state;
        }
    }
}
