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

        public void HandleInput(KeyboardState kbs, Character chr)
        {
            if (kbs.IsKeyDown(Keys.Right))
            {
                chr.FacingRight = true;
                chr.Velocity.UpdateX(chr.Velocity.X + 1);
                return;
            }
            else if (kbs.IsKeyDown(Keys.Left))
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

        public void Update(KeyboardState keyboardState, Character chr)
        {
            if (chr.IsGrounded)
            {
                chr.StateHandler.Pop();
            }
        }
    }
}
