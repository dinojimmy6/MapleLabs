using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Character
    {
        public ICharacterState currentState;
        public CharacterModel characterModel;
        public bool facingRight;

        public Character()
        {
            this.CharacterModel = new CharacterModel();
            this.CurrentState = new IdleState();
            this.CurrentState.Enter(this);
            facingRight = false;
        }

        public void UpdateModel()
        {
            CharacterModel = new CharacterModel();
        }

        public void HandleInput(KeyboardState keyboardState)
        {
            this.CurrentState = this.CurrentState.HandleInput(keyboardState, this);
            this.CurrentState.Enter(this);
        }

        public void SetAnimation(Animations a)
        {
            this.CharacterModel.currentAnimation = a;
        }

        public void Update(GameTime gameTime, KeyboardState KeyboardState)
        {
            this.CharacterModel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CharacterModel.CurrentFrame.Draw(spriteBatch, facingRight);
        }

        public bool FacingRight
        {
            get { return this.facingRight; }
            set { this.facingRight = value; }
        }

        public ICharacterState CurrentState
        {
            get { return this.currentState; }
            set { this.currentState = value; }
        }
        public CharacterModel CharacterModel
        {
            get { return this.characterModel; }
            set { this.characterModel = value; }
        }
    }
}
