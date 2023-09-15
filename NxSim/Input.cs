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
        ClimbUp = 4
    }
    class InputComponent
    {

        private KeyboardState LastState;
        private KeyboardState CurrentState;

        private readonly Dictionary<Actions, Keys> KeyBindings = new();

        public InputComponent()  
        {
            this.BindKey(Keys.Left, Actions.WalkLeft);
            this.BindKey(Keys.Right, Actions.WalkRight);
            this.BindKey(Keys.Down, Actions.Duck);
            this.BindKey(Keys.Up, Actions.ClimbUp);
            this.BindKey(Keys.Space, Actions.Jump);
        }

        public void BindKey(Keys key, Actions action)
        {
            this.KeyBindings.Add(action, key);
        }

        public void UnbindAction(Actions action)
        {
            this.KeyBindings.Remove(action);
        }

        public void Update()
        {
            this.LastState = CurrentState;
            this.CurrentState = Keyboard.GetState();
        }

        public bool IsTriggered(Actions a)
        {
            return this.CurrentState.IsKeyDown(KeyBindings[a]) && this.LastState.IsKeyUp(KeyBindings[a]);
        }

        public bool IsReleased(Actions a)
        {
            return this.LastState.IsKeyDown(KeyBindings[a]) && this.CurrentState.IsKeyUp(KeyBindings[a]);
        }

        public bool IsHeld(Actions a)
        {
            return this.CurrentState.IsKeyDown(KeyBindings[a]);
        }
    }
}
