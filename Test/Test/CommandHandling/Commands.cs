using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace MarioGame
{
    public class MarioCommand : ICommand
    {
        protected Mario receiver;
        private bool toggled = false;
        public bool Toggled
        {
            get { return toggled; }
        }
        protected MarioCommand(Mario receiver)
        {
            this.receiver = receiver;
        }
        public virtual void Execute() { }
    }

    public class UpCommand : MarioCommand
    {
        public UpCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.UpCommand();
        }
    }


    public class DownCommand : MarioCommand
    {
        public DownCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
           receiver.DownCommand();
        }
    }
    public class LeftCommand : MarioCommand
    {
        public LeftCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.LeftCommand();
        }
    }
    public class RightCommand : MarioCommand
    {
        public RightCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.RightCommand();
        }
    }
    public class UpLeftCommand : MarioCommand
    {
        public UpLeftCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.UpLeftCommand();
        }
    }
    public class FireballCommand : MarioCommand
    {
        public FireballCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            //receiver.UpCommand();
            System.Console.WriteLine("Fireball command");
        }
    }
    public class StandardCommand : MarioCommand
    {
        public StandardCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.StandardCommand();
        }
    }

    public class SuperCommand : MarioCommand
    {
        public SuperCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.SuperCommand();
        }
    }

    public class FireCommand : MarioCommand
    {
        public FireCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.FireCommand();
        }
    }
    public class DeadCommand : MarioCommand
    {
        public DeadCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.DeadCommand();
        }
    }
    public class ResetCommand: ICommand
    {
        Game1 receiver;
        private bool toggled = true;
        public bool Toggled
        {
            get { return toggled; }
        }
        public ResetCommand(Game1 receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.ResetCommand();
        }
    }
    public class PauseCommand: ICommand
    {
        Game1 receiver;
        private bool toggled = true;
        public bool Toggled
        {
            get { return toggled; }
        }
        public PauseCommand(Game1 receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.PauseCommand();
        }
    }

    public class VictoryCommand : ICommand
    {
        Game1 receiver;
        private bool toggled = true;
        public bool Toggled
        {
            get { return toggled; }
        }
        public VictoryCommand(Game1 receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.VictoryCommand();
        }
    }

    public class GameoverCommand : ICommand
    {
        Game1 receiver;
        private bool toggled = true;
        public bool Toggled
        {
            get { return toggled; }
        }
        public GameoverCommand(Game1 receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.GameoverCommand();
        }
    }

    public class QuitCommand : ICommand
    {
        Game1 receiver;
        private bool toggled = true;
        public bool Toggled
        {
            get { return toggled; }
        }
        public QuitCommand(Game1 receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.QuitCommand();
        }
    }
    public class MuteCommand : ICommand
    {
        Game1 receiver;
        private bool toggled = true;
        public bool Toggled
        {
            get { return toggled; }
        }
        public MuteCommand(Game1 receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.MuteCommand();
        }
    }

    public class PlaceBlockCommand : ICommand
    {
        Scene receiver;
        private bool toggled = true;
        public bool Toggled
        {
            get { return toggled; }
        }
        public PlaceBlockCommand(Scene receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.PlaceBlockCommand();
        }
    }

    public class DestroyBlockCommand : ICommand
    {
        Scene receiver;
        private bool toggled = true;
        public bool Toggled
        {
            get { return toggled; }
        }
        public DestroyBlockCommand(Scene receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.DestroyBlockCommand();
        }
    }
}
  