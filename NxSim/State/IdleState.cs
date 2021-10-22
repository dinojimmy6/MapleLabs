using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NxSim;

namespace States
{
    class IdleState : ICharacterState
    {
        public IdleState()
        {

        }

        public void HandleInput(KeyboardState kbs, Character chr)
        {
            if (kbs.IsKeyDown(Keys.Right) || kbs.IsKeyDown(Keys.Left))
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
        }

        public void Enter(Character chr)
        {
            chr.Velocity = new Velocity(Vector2.Zero);
            chr.SetAnimation(Animations.Idle);
        }

        public void Reenter(Character chr)
        {
            this.Enter(chr);
        }
    }
}
