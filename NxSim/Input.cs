using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NxSim
{
    enum Actions
    {
        WalkLeft = 0,
        WalkRight = 1,
        Duck = 2,
        Jump = 3,
        ClimbUp = 4,
        ClimbDown = 5
    }
    class Input
    {
        private delegate void KeyEvent();

        private KeyboardState LastState;

        private Dictionary<Keys, KeyEvent> KeyDownMap = new();
        private Dictionary<Keys, KeyEvent> KeyUpMap = new();


        public Input()  
        {
            this.Bind();
            this.KeyDownMap.Add(Keys.Up, WalkLeft);
            this.KeyUpMap.Add(Keys.Up, Stop);
            this.KeyDownMap.Add(Keys.Right, WalkRight);
            this.KeyUpMap.Add(Keys.Up, Stop);
        }

        public void Bind()
        {

        }

        private void WalkLeft()
        {

        }

        public void WalkRight()
        {
           
        }

        public void Stop()
        {

        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            Keys[] pressedKeys = state.GetPressedKeys();
            Keys[] lastKeys = this.LastState.GetPressedKeys();
            foreach (Keys key in pressedKeys) {
                
            }
        }
    }
}
