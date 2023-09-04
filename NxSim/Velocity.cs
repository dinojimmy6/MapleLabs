using Microsoft.Xna.Framework;

namespace NxSim
{
    class Velocity
    {
        private Vector2 speed;
        public Velocity(Vector2 speed)
        {
            this.speed = speed;
        }

        public void UpdateX(float x)
        {
            this.speed.X = x;
        }

        public void UpdateY(float y)
        {
            this.speed.Y = y;
        }

        public void Update(float x, float y)
        {
            this.UpdateX(x);
            this.UpdateY(y);
        }

        public float X {
            get => this.speed.X;
            set => this.speed.X = value;
        }

        public float Y
        {
            get => this.speed.Y;
            set => this.speed.Y = value;
        }
    }
}
