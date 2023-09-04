using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NxSim;

namespace NxSim.Physics.Collision
{
    class UniformGrid
    {
        private HashSet<Rectangle>[,] grid;
        private readonly int cellSize = 320;

        public UniformGrid()
        {
            this.grid = new HashSet<Rectangle>[10, 10];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    this.grid[i, j] = new();
                }
            }
        }

        public void AddBox(Rectangle r)
        {
            for (int i = r.X/this.cellSize; i<=(r.X+r.Width)/this.cellSize; i++)
            {
                for (int j = r.Y/this.cellSize; j <= (r.Y+r.Height)/this.cellSize; j++)
                {
                    this.grid[i, j].Add(r);
                }
            }
        }

        public void RemoveBox(Rectangle r)
        {
            for (int i = r.X / this.cellSize; i <= (r.X + r.Width) / this.cellSize; i++)
            {
                for (int j = r.Y / this.cellSize; j <= (r.Y + r.Height) / this.cellSize; j++)
                {
                    this.grid[i, j].Remove(r);
                }
            }
        }

        public void UpdateBox(Rectangle r)
        {
            this.RemoveBox(r);
            this.AddBox(r);
        }

        public HashSet<Rectangle> GetNeighbors(Rectangle r)
        {
            HashSet<Rectangle> neighbors = new();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (this.grid[i, j].Contains(r)) {
                        foreach (Rectangle neighbor in this.grid[i, j])
                        {
                            neighbors.Add(neighbor);
                        }
                    }
                }
            }
            neighbors.Remove(r);
            return neighbors;
        }
    }
}
