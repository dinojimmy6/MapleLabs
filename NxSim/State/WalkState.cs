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

        public void HandleInput(KeyboardState kbs, Character chr)
        {
            if (kbs.IsKeyDown(Keys.Right))
            {
                chr.FacingRight = true;
                chr.Velocity = new Velocity(new Vector2(200, 0));
                return;
            }
            else if (kbs.IsKeyDown(Keys.Left))
            {
                chr.FacingRight = false;
                chr.Velocity = new Velocity(new Vector2(-200, 0));
                return;
            }
            else
            {
                chr.StateHandler.Pop();
            }
        }

        public void Update(KeyboardState kbs, Character chr)
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
