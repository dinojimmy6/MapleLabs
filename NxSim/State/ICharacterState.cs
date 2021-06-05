using Microsoft.Xna.Framework.Input;
using NxSim;

namespace States
{
    interface ICharacterState
    {
        void HandleInput(KeyboardState keyboardState, Character chr);

        void Update(KeyboardState keyboardState, Character chr);

        void Enter(Character chr);

        void Reenter(Character chr);
    }
}
