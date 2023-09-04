using Microsoft.Xna.Framework;
using NxSim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NxSim.Physics.Collision
{
    class Foothold
    {
        private List<Dictionary<int, int>> footholds;
        private int activeFoothold;
        private int currentLocation;
        private Dictionary<int, List<Tuple<int, int>>> xWise;
        private Dictionary<int, List<Tuple<int, int>>> yWise;

        public Foothold(int activeFoothold, int currentLocation)
        {
            this.footholds = new();
            this.activeFoothold = activeFoothold;
            this.currentLocation = currentLocation;
            xWise = new();
            yWise = new();
        }

        public void AddFoothold(List<Point> points)
        {
            Dictionary<int, int> fh = new();
            foreach (Point p in points)
            {
                fh.Add(p.X, p.Y);
            }
            this.footholds.Add(fh);
            foreach (Point p in points)
            {
                if (!xWise.ContainsKey(p.X))
                {
                    xWise[p.X] = new();
                }
                xWise[p.X].Add(Tuple.Create(this.footholds.Count, p.Y));
                if (!yWise.ContainsKey(p.Y))
                {
                    yWise[p.Y] = new();
                }
                yWise[p.Y].Add(Tuple.Create(this.footholds.Count, p.X));
            }
        }

        public Point SnapPoint(int uX, int uY)
        {
            if (this.footholds[this.activeFoothold].ContainsKey(uX))
            {
                return new Point(uX, this.footholds[this.activeFoothold][uX]);
            }
            return new Point(uX, uY);
        }
    }
}
