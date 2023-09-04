using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using NxSim;

namespace States
{
    class StateHandler
    {
        private readonly Stack<ICharacterState> pda = new();
        private readonly Character character;

        //State pool
        public ICharacterState idle;
        public ICharacterState walk;
        public ICharacterState duck;
        public ICharacterState jump;

        public StateHandler(Character c)
        {
            this.InitializeStates();
            this.character = c;
            this.Push(this.idle);
        }

        //probably pull from command queue after input handler is implemented
        public void HandleInput(KeyboardState kbs)
        {
            this.pda.Peek().HandleInput(kbs, this.character);
        }

        public void Update(KeyboardState kbs, Character chr)
        {
            this.pda.Peek().Update(kbs, chr);
        }

        public void Push(ICharacterState s)
        {
            s.Enter(this.character);
            this.pda.Push(s);
        }

        public void Pop()
        {
            this.pda.Pop();
            this.pda.Peek().Reenter(this.character);
        }

        private void InitializeStates()
        {
            this.idle = new IdleState();
            this.walk = new WalkState();
            this.duck = new DuckState();
            this.jump = new JumpState();
        }
    }
}