using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NxSim.Physics.Collision;
using States;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NxSim
{
    class Character
    {
        private readonly StateHandler stateHandler;
        private readonly InputComponent inputComponent;
        public CharacterModel characterModel;
        private readonly LinkedList<Vector2> positions;
        private Velocity velocity;
        private bool facingRight;
        private bool isGrounded;

        public List<Line> platforms = new();

        public Character()
        {
            this.CharacterModel = new CharacterModel();
            this.stateHandler = new StateHandler(this);
            this.inputComponent = new InputComponent();
            this.positions = new();
            this.Position = new Vector2(400, 1000);
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

        public void HandleInput()
        {
            this.inputComponent.Update();
            this.StateHandler.HandleInput(this, this.inputComponent);
        }

        public void SetAnimation(Animations a)
        {
            this.CharacterModel.currentAnimation = a;
        }

        public void Update(float dT)
        {
            var src = new Vector2(this.Position.X, this.Position.Y - 5);
            var dest_x = this.Position.X + ((float) (this.Velocity.X * dT / 1000));
            var dest_y = this.Position.Y + ((float) (this.Velocity.Y * dT / 1000));
            var dest = new Vector2(dest_x, dest_y);
            Line movement = new(src, dest);
            if (this.Velocity.Y >= 0)
            {
                foreach (Line l in this.platforms)
                {
                    var intersect = movement.GetIntersection(l);
                    if (!float.IsInfinity(intersect.X))
                    {
                        //System.Diagnostics.Debug.WriteLine(intersect.X.ToString());
                        //System.Diagnostics.Debug.WriteLine(intersect.Y.ToString());
                        dest.Y = intersect.Y;
                        this.IsGrounded = true;
                    }
                }
            }
            this.Position = dest;
            this.CharacterModel.Update(dT);
            this.StateHandler.Update(this);
            this.Velocity.Y += 25;
            Debug.WriteLine(this.Position.X - this.LastPosition.X);
        }

        public void Draw(SpriteBatch spriteBatch, float alpha)
        {
            CharacterModel.CurrentFrame.Draw(spriteBatch, CharacterModel.currentAnimation, Vector2.Lerp(this.LastPosition, this.Position, alpha), facingRight);
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
            get { return this.positions.First.Value; }
            set
            {
                this.positions.AddFirst(value);
                if (this.positions.Count > 5)
                {
                    this.positions.RemoveLast();
                }
            }
        }

        public Vector2 LastPosition
        {
            get
            {
                return this.positions.First.Next == null ? this.positions.First.Value : this.positions.First.Next.Value;
            }
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
