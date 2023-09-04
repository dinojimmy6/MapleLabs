using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NxSim.Physics.Collision
{
    class Quadtree
    {
        private int max_objects = 10;
        private int max_levels = 5;

        private int level;
        private List<Rectangle> colliders;
        private Rectangle bounds;
        private Quadtree[] nodes;

        public Quadtree(int level, Rectangle bounds)
        {
            this.level = level;
            this.bounds = bounds;
            this.colliders = new();
            this.nodes = new Quadtree[4];
        }

        public void Insert(Rectangle r)
        {
            if (nodes[0] != null)
            {
                int index = this.GetIndex(r);

                if (index != -1)
                {
                    nodes[index].Insert(r);
                    return;
                }
            }

            colliders.Add(r);
            if (colliders.Count() > this.max_objects && this.level < this.max_levels)
            {
                if (nodes[0] == null)
                {
                    this.Split();
                }

                int i = 0;
                while (i < colliders.Count()) {
                    int index = this.GetIndex(colliders[i]);
                    if (index != -1)
                    {
                        nodes[index].Insert(colliders[i]);
                        colliders.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public List<Rectangle> Retrieve(ref List<Rectangle> neighbors, Rectangle r)
        {
            int index = this.GetIndex(r);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].Retrieve(ref neighbors, r);
            }

            neighbors.AddRange(this.colliders);
            return neighbors;
        }

        public void Clear()
        {
            this.colliders.Clear();
            this.nodes = new Quadtree[4];
        }

        private void Split()
        {
            int subWidth = (int)(bounds.Width / 2);
            int subHeight = (int)(bounds.Height / 2);
            int x = (int)bounds.X;
            int y = (int)bounds.Y;
            //nodes correspond to cartesian quadrants
            nodes[0] = new Quadtree(this.level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new Quadtree(this.level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new Quadtree(this.level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new Quadtree(this.level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        private int GetIndex(Rectangle r)
        {
            double xMidpoint = bounds.X + (bounds.Width / 2);
            double yMidpoint = bounds.Y + (bounds.Height / 2);

            bool fitLeft = r.X + r.Width < xMidpoint;
            bool fitRight = r.X > xMidpoint;
            if (r.Y + r.Height < yMidpoint)
            {
                if (fitLeft)
                {
                    return 1;
                }
                else if (fitRight)
                {
                    return 0;
                }
            }
            else if (r.Y > yMidpoint)
            {
                if (fitLeft)
                {
                    return 2;
                }
                else if (fitRight)
                {
                    return 3;
                }
            }
            return -1;
        }
    }
}
