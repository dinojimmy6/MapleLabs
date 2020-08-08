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
            else
            {
                return null;
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
                return null;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                chr.FacingRight = false;
                return null;
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
}
