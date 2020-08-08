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
        public ICharacterState characterState;
        public CharacterModel characterModel;
        public bool facingRight;

        public Character()
        {
            this.CharacterModel = new CharacterModel();
            this.CharacterState = new IdleState();
            this.CharacterState.Enter(this);
            facingRight = false;
        }

        public void UpdateModel()
        {
            CharacterModel = new CharacterModel();
        }

        public void HandleInput(KeyboardState keyboardState)
        {
            ICharacterState newState = this.CharacterState.HandleInput(keyboardState, this);
            if (newState != null)
            {
                this.CharacterState = newState;
                this.CharacterState.Enter(this);
            }
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

        public ICharacterState CharacterState
        {
            get { return this.characterState; }
            set { this.characterState = value; }
        }
        public CharacterModel CharacterModel
        {
            get { return this.characterModel; }
            set { this.characterModel = value; }
        }
    }
}
