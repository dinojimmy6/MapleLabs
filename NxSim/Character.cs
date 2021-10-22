using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using States;

namespace NxSim
{
    class Character
    {
        private readonly StateHandler stateHandler;
        public CharacterModel characterModel;
        private Vector2 position;
        private Velocity velocity;
        private bool facingRight;

        public Character()
        {
            this.CharacterModel = new CharacterModel();
            this.stateHandler = new StateHandler(this);
            this.position = new Vector2(705, 1184);
            this.velocity = new(Vector2.Zero);
            this.facingRight = false;
        }

        public void UpdateModel()
        {
            CharacterModel = new CharacterModel();
        }

        public void HandleInput(KeyboardState keyboardState)
        {
            this.StateHandler.HandleInput(keyboardState);
        }

        public void SetAnimation(Animations a)
        {
            this.CharacterModel.currentAnimation = a;
        }

        public void Update(GameTime gameTime, KeyboardState KeyboardState)
        {
            this.position.X += Convert.ToSingle(this.Velocity.Speed.X * gameTime.ElapsedGameTime.TotalSeconds);
            this.position.Y += Convert.ToSingle(this.Velocity.Speed.Y * gameTime.ElapsedGameTime.TotalSeconds);
            this.CharacterModel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CharacterModel.CurrentFrame.Draw(spriteBatch, CharacterModel.currentAnimation, this.position, facingRight);
        }

        public StateHandler StateHandler
        {
            get { return this.stateHandler; }
        }

        public bool FacingRight
        {
            get { return this.facingRight; }
            set { this.facingRight = value; }
        }

        public CharacterModel CharacterModel
        {
            get { return this.characterModel; }
            set { this.characterModel = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
        }

        public Velocity Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }
    }
}
