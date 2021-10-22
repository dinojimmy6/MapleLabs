using Microsoft.Xna.Framework;

namespace NxSim
{
    class Velocity
    {
        private Vector2 speed;
        public Velocity(Vector2 speed)
        {
            this.Speed = speed;
        }

        public void Update()
        {
            
        }

        public Vector2 Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
    }
}
