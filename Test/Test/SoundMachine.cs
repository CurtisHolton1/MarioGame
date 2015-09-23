using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace MarioGame
{
    
    public class SoundMachine
    {
        Dictionary<String, SoundEffect> soundFX;

        public SoundMachine(Game1 game)
        {
            soundFX = new Dictionary<String, SoundEffect>();
            soundFX.Add("small-jump", game.Content.Load<SoundEffect>("jump-small"));
            soundFX.Add("big-jump", game.Content.Load<SoundEffect>("jump-super"));
            soundFX.Add("die", game.Content.Load<SoundEffect>("mariodie"));
            soundFX.Add("powerup", game.Content.Load<SoundEffect>("powerup"));
            soundFX.Add("stomp", game.Content.Load<SoundEffect>("stomp"));

            soundFX.Add("oneup", game.Content.Load<SoundEffect>("1-up"));
            soundFX.Add("bump", game.Content.Load<SoundEffect>("bump"));
            soundFX.Add("coin", game.Content.Load<SoundEffect>("coin"));
            soundFX.Add("powerdown", game.Content.Load<SoundEffect>("pipe"));
            soundFX.Add("gameover", game.Content.Load<SoundEffect>("GameOverSound"));

            soundFX.Add("warning", game.Content.Load<SoundEffect>("warning"));
            soundFX.Add("bricksmash", game.Content.Load<SoundEffect>("breakblock"));
            soundFX.Add("pause", game.Content.Load<SoundEffect>("pause"));
            soundFX.Add("winner", game.Content.Load<SoundEffect>("stage_clear"));
        }

        public void PlayDie()
        {
            soundFX["die"].Play();
        }

        public void PlaySmallJump()
        {
            soundFX["small-jump"].Play();
        }
        public void PlayBigJump()
        {
            soundFX["big-jump"].Play();
        }
        public void PlayPowerUp()
        {
            soundFX["powerup"].Play();
        }
        public void PlayStomp()
        {
            soundFX["stomp"].Play();
        }
        public void PlayOneUp()
        {
            soundFX["oneup"].Play();
        }
        public void PlayBump()
        {
            soundFX["bump"].Play();
        }
        public void PlayCoin()
        {
            soundFX["coin"].Play();
        }
        public void PlayPowerDown()
        {
            soundFX["powerdown"].Play();
        }
        public void PlayGameOver()
        {
            soundFX["gameover"].Play(); ;
        }
        public void PlayWarning()
        {
            soundFX["warning"].Play();
        }
        public void PlayBrickSmash()
        {
            soundFX["bricksmash"].Play();
        }
        public void PlayPause()
        {
            soundFX["pause"].Play();
        }
        public void PlayWinner()
        {
            soundFX["winner"].Play();
        }
    }
}
