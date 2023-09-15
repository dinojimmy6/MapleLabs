using Microsoft.Xna.Framework.Input;
using NxSim;

namespace States
{
    interface ICharacterState
    {
        void HandleInput(Character chr, InputComponent input);

        void Update(Character chr);

        void Enter(Character chr);

        void Reenter(Character chr);
    }
}
