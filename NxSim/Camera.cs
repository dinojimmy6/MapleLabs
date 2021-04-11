using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Camera
    {
        protected float zoom; // Camera Zoom
        protected float rotation; // Camera Rotation
        public Vector2 position; // Camera Position
        public Matrix transform; // Matrix Transform

        public Camera()
        {
            Zoom = 3.0f;
            Rotation = 0.0f;
            Position = new Vector2(0, 0);
            UpdateTransform();
        }

        public void UpdateTransform()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *//subtract camera coordinates to bring scene to origin [0, 0]
                 Matrix.CreateRotationZ(Rotation) *//rotate it
                 Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *//zoom it
                 Matrix.CreateTranslation(new Vector3(1920 * 0.5f, 1080 * 0.5f, 0));//shift entire scene to middle of screen 
        }

        public void Move(int toMove)
        {
            
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
