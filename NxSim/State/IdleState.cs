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

        public void HandleInput(KeyboardState kbs, Character chr)
        {
            if (kbs.IsKeyDown(Keys.Space))
            {
                chr.StateHandler.Push(chr.StateHandler.jump);
            }
            else if (kbs.IsKeyDown(Keys.Right) || kbs.IsKeyDown(Keys.Left))
            {
                chr.StateHandler.Push(chr.StateHandler.walk);
                return;
            }
            else if (kbs.IsKeyDown(Keys.Down))
            {
                chr.StateHandler.Push(chr.StateHandler.duck);
                return;
            }
            else
            {
                return;
            }
        }

        public void Update(KeyboardState kbs, Character chr)
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
