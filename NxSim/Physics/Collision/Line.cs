using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NxSim.Physics.Collision
{
    class Line
    {
        private Vector2 p1;
        private Vector2 p2;
        public Line(Vector2 p1, Vector2 p2)
        {
            if(p2.X > p1.X)
            {
                this.p1 = p1;
                this.p2 = p2;
            }  else
            {
                this.p1 = p2;
                this.p2 = p1;
            }
        }

        public Vector2 GetIntersection(Line l)
        {
            Vector2 AB = this.P2 - this.P1;
            Vector2 CD = l.P2 - l.P1;
            Vector2 AC = l.P1 - this.P1;

            var ABxCD = CrossProduct2D(AB, CD);
            if (ABxCD == 0)
            {
                return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
            }
            var alpha = CrossProduct2D(AC, CD);
            if (alpha > 0)
            {
                if (alpha < 0 || alpha > ABxCD)
                {
                    return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
                }
            } else
            {
                if (alpha > 0 || alpha < ABxCD)
                {
                    return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
                }
            }
            var beta = CrossProduct2D(AC, AB);
            if (beta > 0)
            {
                if (beta < 0 || beta > ABxCD)
                {
                    return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
                }
            } else
            {
                if (beta > 0 || beta < ABxCD)
                {
                    return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
                }
            }
            var intersect = this.P1 + AB * (alpha / ABxCD);
            return intersect;
        }

        public Vector2 P1
        {
            get { return this.p1; }
            set { this.p1 = value; }
        }

        public Vector2 P2
        {
            get { return this.p2; }
            set { this.p2 = value; }
        }

        private static float CrossProduct2D(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.Y - v2.X * v1.Y;
        }
    }
}
