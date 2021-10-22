using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace NxSim
{
    class Map
    {
        public List<MapFrag> backFrags = new();
        public Map(GraphicsDevice gfxd)
        {
            FileStream fileStream = new FileStream("wz\\cake.png", FileMode.Open);
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
