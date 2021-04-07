using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    interface ICharacterState
    {
        ICharacterState HandleInput(KeyboardState keyboardState, Character chr);

        void Update(KeyboardState keyboardState, Character chr);

        void Enter(Character chr);
    }

    class IdleState : ICharacterState
    {
        public IdleState()
        {

        }

        public ICharacterState HandleInput(KeyboardState keyboardState, Character chr)
        {
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.Left))
            {
                return new WalkingState();
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                return new DuckState();
            }
            else
            {
                return chr.CurrentState;
            }
        }

        public void Update(KeyboardState keyboardState, Character chr)
        {

        }

        public void Enter(Character chr)
        {
            chr.SetAnimation(Animations.Idle);
        }
    }

    class WalkingState : ICharacterState
    {
        public WalkingState()
        {

        }

        public ICharacterState HandleInput(KeyboardState keyboardState, Character chr)
        {
            if (keyboardState.IsKeyDown(Keys.Right)) {
                chr.FacingRight = true;
                return chr.currentState;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                chr.FacingRight = false;
                return chr.currentState;
            }
            else
            {
                return new IdleState();
            }
        }

        public void Update(KeyboardState KeyboardState, Character chr)
        {

        }

        public void Enter(Character chr)
        {
            chr.SetAnimation(Animations.Walk);
        }
    }

    class DuckState : ICharacterState
    {
        public DuckState()
        {

        }

        public ICharacterState HandleInput(KeyboardState keyboardState, Character chr)
        {
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                return chr.CurrentState;
            }
            return new IdleState();
        }

        public void Update(KeyboardState KeyboardState, Character chr)
        {

        }

        public void Enter(Character chr)
        {
            chr.SetAnimation(Animations.Duck);
        }
    }
}
