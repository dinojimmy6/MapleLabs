using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using NxSim.Physics.Collision;
using System.Reflection.Metadata.Ecma335;

namespace NxSim
{
    class Map
    {
        public List<MapFrag> backFrags = new();
        private readonly List<Line> platforms = new();
        public Map(GraphicsDevice gfxd)
        {
            FileStream fileStream = new("wz\\cake.png", FileMode.Open);
            backFrags.Add(new MapFrag(Texture2D.FromStream(gfxd, fileStream), new Vector2(0, 0), 0));
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            foreach(MapFrag backFrag in this.backFrags)
            {
                //Rectangle destRect = new(Convert.ToInt32(backFrag.origin.X), Convert.ToInt32(backFrag.origin.Y), (int)backFrag.sprite.Width, (int)backFrag.sprite.Height);
                spriteBatch.Draw(backFrag.sprite, backFrag.origin, Color.White);
            }
        }

        public void LoadPlatforms()
        {
            this.platforms.Add(new Line(new Vector2(0, 1184), new Vector2(800, 1184)));
            this.platforms.Add(new Line(new Vector2(0, 1600), new Vector2(1600, 1600)));
            this.platforms.Add(new Line(new Vector2(801, 1300), new Vector2(1300, 1600)));
        }

        public List<Line> Platforms
        {
            get => this.platforms;
        }
    }

    class MapFrag
    {
        public Texture2D sprite;
        public Vector2 origin;
        public int z;

        public MapFrag(Texture2D sprite, Vector2 origin, int z)
        {
            this.sprite = sprite;
            this.origin = origin;
            this.z = z;
        }
    }
}
