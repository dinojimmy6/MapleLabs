using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NxSim.Physics.Collision
{
    class AABB
    {
        public static bool IsColliding(Rectangle r1, Rectangle r2)
        {
            return (r1.X < (r2.X + r2.Width) && r2.X < r1.X + r1.Width &&
                r1.Y < r2.Y + r2.Height && r2.Y < r1.Y + r1.Height);
        }
    }
}
