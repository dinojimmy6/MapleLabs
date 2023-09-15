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

        //State pool
        public ICharacterState idle;
        public ICharacterState walk;
        public ICharacterState duck;
        public ICharacterState jump;

        public StateHandler(Character chr)
        {
            this.InitializeStates();
            this.Push(this.idle, chr);
        }

        //probably pull from command queue after input handler is implemented
        public void HandleInput(Character chr, InputComponent input)
        {
            this.pda.Peek().HandleInput(chr, input);
        }

        public void Update(Character chr)
        {
            this.pda.Peek().Update(chr);
        }

        public void Push(ICharacterState s, Character chr)
        {
            s.Enter(chr);
            this.pda.Push(s);
        }

        public void Pop(Character chr)
        {
            this.pda.Pop();
            this.pda.Peek().Reenter(chr);
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