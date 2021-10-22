using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace NxSim
{
    class Camera
    {
        private float zoom; // Camera Zoom
        private float rotation; // Camera Rotation
        private Vector2 velocity;
        private Vector2 position; // Camera Position
        private Matrix transform; // Matrix Transform
        private Character character;

        public Camera()
        {
            Zoom = 1.5f;
            Rotation = 0.0f;
            Position = new Vector2(705, -1184);
            velocity = Vector2.Zero;
            UpdateTransform();
        }

        public void SetCharacter(Character c)
        {
            this.character = c;
        }

        public void UpdateTransform()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *//subtract camera coordinates to bring scene to origin [0, 0]
                 Matrix.CreateRotationZ(Rotation) *//rotate it
                 Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *//zoom it
                 Matrix.CreateTranslation(new Vector3(1920 * 0.5f, 1080 * 0.5f + 260, 0));//shift entire scene to middle of screen 
        }

        public void Update(GameTime gameTime)
        {
            this.position = SmoothCD(this.position, character.Position, ref velocity, 0.4F, Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds));
            this.UpdateTransform();
        }

        private Vector2 SmoothCD(Vector2 from, Vector2 to, ref Vector2 currentVelocity, float smoothTime, float deltaTime, float maxSpeed = Single.PositiveInfinity)
        {
            float omega = 2F / smoothTime;
            float x = omega * deltaTime;
            float exp = 1F / (1F + x + 0.48F * x * x + 0.235F * x * x * x);
            float change_x = from.X - to.X;
            float change_y = from.Y - to.Y;

            to.X = from.X - change_x;
            to.Y = from.Y - change_y;

            float temp_x = (currentVelocity.X + omega * change_x) * deltaTime;
            float temp_y = (currentVelocity.Y + omega * change_y) * deltaTime;

            currentVelocity.X = (currentVelocity.X - omega * temp_x) * exp;
            currentVelocity.Y = (currentVelocity.Y - omega * temp_y) * exp;

            float output_x = to.X + (change_x + temp_x) * exp;
            float output_y = to.Y + (change_y + temp_y) * exp;
            return new Vector2(output_x, output_y);
        }

        public float Zoom
        {
            get { return this.zoom; }
            set { this.zoom = value; if (zoom < 0.1f) zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Matrix Transform
        {
            get { return this.transform; }
            set { this.transform = value; }
        }
    }
}
