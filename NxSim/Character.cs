using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NxSim.Physics.Collision;
using States;
using System;
using System.Collections.Generic;

namespace NxSim
{
    class Character
    {
        private readonly StateHandler stateHandler;
        public CharacterModel characterModel;
        private Vector2 position;
        private Velocity velocity;
        private bool facingRight;
        private bool isGrounded;

        public List<Line> platforms = new();

        public Character()
        {
            this.CharacterModel = new CharacterModel();
            this.stateHandler = new StateHandler(this);
            this.position = new Vector2(400, 1000);
            this.velocity = new(new Vector2(0, 10));
            this.facingRight = false;
            this.IsGrounded = false;

            platforms.Add(new Line(new Vector2(0, 1184), new Vector2(800, 1184)));
            platforms.Add(new Line(new Vector2(0, 1600), new Vector2(1600, 1600)));
            platforms.Add(new Line(new Vector2(801, 1300), new Vector2(1300, 1600)));
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

        public void Update(GameTime gameTime, KeyboardState kbs)
        {
            var src = new Vector2(this.position.X, this.position.Y - 5);
            var dest_x = this.position.X + Convert.ToSingle(this.Velocity.X * gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
            var dest_y = this.position.Y + Convert.ToSingle(this.Velocity.Y * gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
            var dest = new Vector2(dest_x, dest_y);
            Line movement = new(src, dest);
            if (this.Velocity.Y >= 0)
            {
                foreach (Line l in this.platforms)
                {
                    var intersect = movement.GetIntersection(l);
                    if (!float.IsInfinity(intersect.X))
                    {
                        System.Diagnostics.Debug.WriteLine(intersect.X.ToString());
                        System.Diagnostics.Debug.WriteLine(intersect.Y.ToString());
                        dest.Y = intersect.Y;
                        this.IsGrounded = true;
                    }
                }
            }
            this.position = dest;
            this.CharacterModel.Update(gameTime);
            this.stateHandler.Update(kbs, this);
            this.Velocity.Y += 25;
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

        public bool IsGrounded {
            get => isGrounded;
            set => isGrounded = value;
        }
    }
}
