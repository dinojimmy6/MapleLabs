using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NxSim;
using System;

namespace States
{
    class JumpState : ICharacterState
    {
        public JumpState()
        {
            
        }

        public void Enter(Character chr)
        {
            chr.Velocity.UpdateY(-500);
            chr.IsGrounded = false;
        }

        public void HandleInput(Character chr, InputComponent input)
        {
            if (input.IsHeld(Actions.WalkRight))
            {
                chr.FacingRight = true;
                chr.Velocity.UpdateX(chr.Velocity.X + 1);
                return;
            }
            else if (input.IsHeld(Actions.WalkLeft))
            {
                chr.FacingRight = false;
                chr.Velocity.UpdateX(chr.Velocity.X - 1);
                return;
            }
        }

        public void Reenter(Character chr)
        {
            this.Enter(chr);
            chr.IsGrounded = false;
        }

        public void Update(Character chr)
        {
            if (chr.IsGrounded)
            {
                chr.StateHandler.Pop(chr);
            }
        }
    }
}
