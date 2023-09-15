using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NxSim;
using System;

namespace States
{
    class IdleState : ICharacterState
    {
        public IdleState()
        {

        }

        public void HandleInput(Character chr, InputComponent input)
        {
            if (input.IsHeld(Actions.Jump))
            {
                chr.StateHandler.Push(chr.StateHandler.jump, chr);
            }
            else if (input.IsHeld(Actions.WalkRight) || input.IsHeld(Actions.WalkLeft))
            {
                chr.StateHandler.Push(chr.StateHandler.walk, chr);
            }
            else if (input.IsHeld(Actions.Duck))
            {
                chr.StateHandler.Push(chr.StateHandler.duck, chr);
            }
        }

        public void Update(Character chr)
        {
            chr.Velocity.UpdateX((float) Math.Truncate(chr.Velocity.X * 0.1));
        }

        public void Enter(Character chr)
        {
            chr.SetAnimation(Animations.Idle);
        }

        public void Reenter(Character chr)
        {
            this.Enter(chr);
        }
    }
}
