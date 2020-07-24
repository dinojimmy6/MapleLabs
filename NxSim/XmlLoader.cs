using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace Game1
{
    class XmlLoader
    {
        public static Dictionary<string, ComponentFrame> sprites = new Dictionary<string, ComponentFrame>();

        static HashSet<string> animations = new HashSet<string>{"walk1"};

        public static void LoadXml(GraphicsDevice gfxd, string xmlPath, string imgPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            foreach(XmlNode node in doc["imgdir"].ChildNodes)
            {
                if(animations.Contains(node.Attributes["name"].Value))
                {
                    LoadAnimation(node, gfxd, imgPath);
                }
            }
            
        }

        public static void LoadHeadXml(GraphicsDevice gfxd)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("wz\\12013xml\\12013xml.xml");
            XmlNode front = doc["xmldump"]["wzimg"].SelectNodes("imgdir[@name='front']")[0]["canvas"];
            string imgName = "front.head";
            short x = short.Parse(front["vector"].Attributes["x"].Value);
            short y = short.Parse(front["vector"].Attributes["y"].Value);
            FileStream fileStream = new FileStream("wz\\00012013.img\\" + imgName + ".png", FileMode.Open);
            ComponentFrame cf = new ComponentFrame(Texture2D.FromStream(gfxd, fileStream), new Vector2(x, y));
            fileStream.Dispose();
            XmlNodeList mapNodes = front.SelectNodes("imgdir[@name='map']");
            if (mapNodes.Count == 1)
            {
                XmlNodeList neck = mapNodes[0].SelectNodes("vector[@name='neck']");
                if (neck.Count == 1)
                {
                    cf.neck = new Vector2(short.Parse(neck[0].Attributes["x"].Value), short.Parse(neck[0].Attributes["y"].Value));
                }
                XmlNodeList brow = mapNodes[0].SelectNodes("vector[@name='brow']");
                if (brow.Count == 1)
                {
                    cf.brow = new Vector2(short.Parse(brow[0].Attributes["x"].Value), short.Parse(brow[0].Attributes["y"].Value));
                }
            }
            sprites[imgName] = cf;
        }

        public static void LoadHairXml(GraphicsDevice gfxd)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("wz\\00041656xml\\00041656xml.xml");
            XmlNode default_ = doc["xmldump"]["wzimg"].SelectNodes("imgdir[@name='default']")[0];
            foreach (XmlNode component in default_.SelectNodes("canvas"))
            {
                string componentName = component.Attributes["name"].Value;
                string imgName = "default" + "." + componentName;
                short x = short.Parse(component["vector"].Attributes["x"].Value);
                short y = short.Parse(component["vector"].Attributes["y"].Value);
                FileStream fileStream = new FileStream("wz\\00041656.img\\" + imgName + ".png", FileMode.Open);
                ComponentFrame cf = new ComponentFrame(Texture2D.FromStream(gfxd, fileStream), new Vector2(x, y));
                fileStream.Dispose();
                XmlNodeList mapNodes = component.SelectNodes("imgdir[@name='map']");
                if (mapNodes.Count == 1)
                {
                    XmlNodeList brow = mapNodes[0].SelectNodes("vector[@name='brow']");
                    if (brow.Count == 1)
                    {
                        cf.brow = new Vector2(short.Parse(brow[0].Attributes["x"].Value), short.Parse(brow[0].Attributes["y"].Value));
                    }
                }
                sprites[imgName] = cf;
            }
            
        }

        public static void LoadFaceXml(GraphicsDevice gfxd)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("wz\\Face\\00020644.img.xml");
            XmlNode default_ = doc["imgdir"].SelectNodes("imgdir[@name='default']")[0];
            foreach (XmlNode component in default_.SelectNodes("canvas"))
            {
                string componentName = component.Attributes["name"].Value;
                string imgName = "default" + "." + componentName;
                short x = short.Parse(component["vector"].Attributes["x"].Value);
                short y = short.Parse(component["vector"].Attributes["y"].Value);
                FileStream fileStream = new FileStream("wz\\Face\\00020644.img\\00020644.img\\" + imgName + ".png", FileMode.Open);
                ComponentFrame cf = new ComponentFrame(Texture2D.FromStream(gfxd, fileStream), new Vector2(x, y));
                fileStream.Dispose();
                XmlNodeList mapNodes = component.SelectNodes("imgdir[@name='map']");
                if (mapNodes.Count == 1)
                {
                    XmlNodeList brow = mapNodes[0].SelectNodes("vector[@name='brow']");
                    if (brow.Count == 1)
                    {
                        cf.brow = new Vector2(short.Parse(brow[0].Attributes["x"].Value), short.Parse(brow[0].Attributes["y"].Value));
                    }
                }
                sprites[imgName] = cf;
            }
            XmlNode blink = doc["imgdir"].SelectNodes("imgdir[@name='blink']")[0];
            foreach (XmlNode frame in blink.ChildNodes)
            {
                string frameNum = frame.Attributes["name"].Value;
                foreach (XmlNode component in frame.SelectNodes("canvas"))
                {
                    string componentName = component.Attributes["name"].Value;
                    string imgName = ResolveInLink(component);
                    string keyName = blink.Attributes["name"].Value + "." + frameNum + "." + componentName;
                    imgName = imgName == null ? keyName : imgName;
                    short x = short.Parse(component["vector"].Attributes["x"].Value);
                    short y = short.Parse(component["vector"].Attributes["y"].Value);
                    FileStream fileStream = new FileStream("wz\\Face\\00020644.img\\00020644.img\\" + imgName + ".png", FileMode.Open);
                    ComponentFrame cf = new ComponentFrame(Texture2D.FromStream(gfxd, fileStream), new Vector2(x, y));
                    fileStream.Dispose();
                    XmlNodeList mapNodes = component.SelectNodes("imgdir[@name='map']");
                    if (mapNodes.Count == 1)
                    {
                        XmlNodeList brow = mapNodes[0].SelectNodes("vector[@name='brow']");
                        if (brow.Count == 1)
                        {
                            cf.brow = new Vector2(short.Parse(brow[0].Attributes["x"].Value), short.Parse(brow[0].Attributes["y"].Value));
                        }
                    }
                    sprites[keyName] = cf;
                }
            }
        }

        private static void LoadAnimation(XmlNode node, GraphicsDevice gfxd, string imgPath)
        {
            foreach (XmlNode frame in node.ChildNodes)
            {
                string frameNum = frame.Attributes["name"].Value;
                foreach (XmlNode component in frame.SelectNodes("canvas"))
                {
                    string componentName = component.Attributes["name"].Value;
                    string imgName = node.Attributes["name"].Value + "." + frameNum + "." + componentName;
                    short x  = short.Parse(component["vector"].Attributes["x"].Value);
                    short y = short.Parse(component["vector"].Attributes["y"].Value);
                    FileStream fileStream = new FileStream(imgPath + imgName + ".png", FileMode.Open);
                    ComponentFrame cf = new ComponentFrame(Texture2D.FromStream(gfxd, fileStream), new Vector2(x, y));
                    fileStream.Dispose();
                    XmlNodeList mapNodes = component.SelectNodes("imgdir[@name='map']");
                    if(mapNodes.Count == 1)
                    {
                        XmlNodeList neck = mapNodes[0].SelectNodes("vector[@name='neck']");
                        if(neck.Count == 1)
                        {
                            cf.neck = new Vector2(short.Parse(neck[0].Attributes["x"].Value), short.Parse(neck[0].Attributes["y"].Value));
                        }
                        XmlNodeList navel = mapNodes[0].SelectNodes("vector[@name='navel']");
                        if(navel.Count == 1)
                        {
                            cf.navel = new Vector2(short.Parse(navel[0].Attributes["x"].Value), short.Parse(navel[0].Attributes["y"].Value));
                        }
                        XmlNodeList hand = mapNodes[0].SelectNodes("vector[@name='hand']");
                        if(hand.Count == 1)
                        {
                            cf.hand = new Vector2(short.Parse(hand[0].Attributes["x"].Value), short.Parse(hand[0].Attributes["y"].Value));
                        }
                    }
                    sprites[imgName] = cf;
                }
            }
        }

        private static string ResolveInLink(XmlNode node)
        {
            XmlNodeList inLink = node.SelectNodes("imgdir[@name='_inlink']");
            if(inLink.Count > 0)
            {
                return inLink[0].Attributes["value"].Value.Replace('/', '.');
            }
            return null;
        }
    }

    class ComponentFrame
    {
        public Texture2D sprite;
        public Vector2 origin;
        public Vector2 neck;
        public Vector2 navel;
        public Vector2 hand;
        public Vector2 brow;

        public ComponentFrame(Texture2D _sprite, Vector2 origin)
        {
            sprite = _sprite;
            this.origin = origin;
        }
    }
}
