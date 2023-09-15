using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NxSim;
using System;

namespace States
{
    class DuckState : ICharacterState
    {
        public DuckState()
        {

        }

        public void HandleInput(Character chr, InputComponent input)
        {
            if (input.IsHeld(Actions.Duck))
            {
                return;
            }
            chr.StateHandler.Pop(chr);
        }

        public void Update(Character chr)
        {
            chr.Velocity.UpdateX((float) Math.Truncate(chr.Velocity.X * 0.1));
        }

        public void Enter(Character chr)
        {
            chr.SetAnimation(Animations.Duck);
        }

        public void Reenter(Character chr)
        {
            this.Enter(chr);
        }
    }
}
