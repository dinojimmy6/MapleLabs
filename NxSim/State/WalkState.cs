using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NxSim;

namespace States
{
    class WalkState : ICharacterState
    {
        public WalkState()
        {

        }

        public void HandleInput(Character chr, InputComponent input)
        {
            if (input.IsHeld(Actions.Jump))
            {
                chr.StateHandler.Push(chr.StateHandler.jump, chr);
            }
            else if (input.IsHeld(Actions.WalkRight))
            {
                chr.FacingRight = true;
                chr.Velocity.UpdateX(200);
                return;
            }
            else if (input.IsHeld(Actions.WalkLeft))
            {
                chr.FacingRight = false;
                chr.Velocity.UpdateX(-200);
                return;
            }
            else
            {
                chr.StateHandler.Pop(chr);
            }
        }

        public void Update(Character chr)
        {

        }

        public void Enter(Character chr)
        {
            chr.SetAnimation(Animations.Walk);
        }

        public void Reenter(Character chr)
        {
            this.Enter(chr);
        }
    }
}
