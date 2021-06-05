using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NxSim;

namespace States
{
    class DuckState : ICharacterState
    {
        public DuckState()
        {

        }

        public void HandleInput(KeyboardState kbs, Character chr)
        {
            if (kbs.IsKeyDown(Keys.Down))
            {
                return;
            }
            chr.StateHandler.Pop();
        }

        public void Update(KeyboardState kbs, Character chr)
        {

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
