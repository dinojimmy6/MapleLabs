using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;


namespace Game1
{
    public enum Animations
    {
        Default,
        Idle,
        Walk,
        Walk2,
        Alert,
        SwingO1,
        Duck,
        DuckStab
    }

    static class AnimationsExtensions
    {
        public static Dictionary<Animations, List<string>> Layers = new Dictionary<Animations, List<string>>();

        static AnimationsExtensions()
        {
            Layers.Add(Animations.Idle, new List<string> { "effect", "hairBelow", "body", "lGlove", "mail", "head", "arm", "rGlove", "hairAbove", "mailArm", "face", "shoes", "weapon" });
            Layers.Add(Animations.Walk, new List<string> { "effect", "hairBelow", "body", "lGlove", "mail", "head", "arm", "rGlove", "hairAbove", "mailArm", "face", "shoes", "weapon" });
            Layers.Add(Animations.Walk2, new List<string> { "effect", "hairBelow", "body", "lGlove", "mail", "head", "arm", "hairAbove", "mailArm", "rGlove", "face", "shoes", "weapon" });
            Layers.Add(Animations.Alert, new List<string> { "effect", "hairBelow", "body", "mail", "head", "arm", "hairAbove", "mailArm", "face", "rHand", "lHand", "rGlove", "lGlove", "shoes", "weapon" });
            Layers.Add(Animations.SwingO1, new List<string> { "effect", "hairBelow", "body", "lGlove", "mail", "head", "arm", "rGlove", "hairAbove", "mailArm", "face", "shoes", "weapon" });
        }

        public static List<string> Layer(this Animations animation)
        {
            return Layers[animation];
        }

    }

    class Skeleton
    {
        public static Dictionary<string, Dictionary<Animations, Dictionary<int, ComponentFrame>>> Sk = new Dictionary<string, Dictionary<Animations, Dictionary<int, ComponentFrame>>>();
        public static Dictionary<Animations, List<int>> Duration = new Dictionary<Animations, List<int>>();

        static Skeleton()
        {
            Sk.Add("effect", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());
            Sk.Add("arm", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());
            Sk.Add("body", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());
            Sk.Add("rHand", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());
            Sk.Add("lHand", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());
            Sk.Add("rGlove", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());
            Sk.Add("lGlove", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());
            Sk.Add("mail", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());
            var MailArm = new Dictionary<Animations, Dictionary<int, ComponentFrame>>();
            Sk.Add("mailArm", MailArm);
            Sk.Add("mailArm2", MailArm);
            Sk.Add("mailArm3", MailArm);
            Sk.Add("mailArmOverHair", MailArm);
            Sk.Add("shoes", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());
            Sk.Add("weapon", new Dictionary<Animations, Dictionary<int, ComponentFrame>>());

        }

        public static void AddBook(string part, Animations A, Dictionary<int, ComponentFrame> Book)
        {
            if(Sk.ContainsKey(part))
            {
                Sk[part][A] = Book;
            }
            
        }

        public static int GetAnimationLength(Animations A)
        {
            return Sk["arm"][A].Count;
        }
    }

    class XmlLoader
    {
        public static Dictionary<string, ComponentFrame> StaticSprites = new Dictionary<string, ComponentFrame>();
        public static Dictionary<string, Animations> AnimationStrings = new Dictionary<string, Animations>();

        public static void LoadXml(GraphicsDevice gfxd, EquipTypes et, string id)
        {
            string xmlPath = "wz\\" + et + "\\" + id + ".img.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            foreach (XmlNode node in doc["imgdir"].ChildNodes)
            {
                if (AnimationStrings.ContainsKey(node.Attributes["name"].Value))
                {
                    LoadAnimation(node, gfxd, et, id, null);
                }
            }

        }

        public static void LoadWeaponXml(GraphicsDevice gfxd, EquipTypes et, string id, string weaponBase)
        {
            string xmlPath = "wz\\" + et + "\\" + id + ".img.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            if(weaponBase == "0")
            {
                return;
            }
            XmlNodeList weaponBaseNode = doc["imgdir"].SelectNodes("imgdir[@name='" + weaponBase + "']");
            if (weaponBaseNode.Count != 1)
            {
                throw new Exception("Weapon Base not found in Weapon Xml.");
            }
            XmlNodeList xmlAnimations = weaponBaseNode[0].SelectNodes("imgdir");
            List<XmlNode> animations = new List<XmlNode>(xmlAnimations.Cast<XmlNode>().ToArray());
            foreach (XmlNode uol in weaponBaseNode[0].SelectNodes("uol"))
            {
                XmlNode target = ResolveXmlPath(uol, uol.Attributes["value"].Value);
                animations.Add(target);
            }
            foreach (XmlNode node in animations)
            {
                if (AnimationStrings.ContainsKey(node.Attributes["name"].Value))
                {
                    LoadAnimation(node, gfxd, et, id, weaponBaseNode[0].Attributes["name"].Value + ".");
                }
            }
        }

        public static void LoadHeadXml(GraphicsDevice gfxd)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("wz\\Misc\\head.img.xml");
            XmlNode front = doc["xmldump"]["wzimg"].SelectNodes("imgdir[@name='front']")[0]["canvas"];
            string imgName = "front.head";
            short x = short.Parse(front["vector"].Attributes["x"].Value);
            short y = short.Parse(front["vector"].Attributes["y"].Value);
            ComponentFrame cf = new ComponentFrame(gfxd, "wz\\Misc\\head.atlas.png", new Vector2(x, y), EquipTypes.Misc, "head", imgName);
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
            StaticSprites[imgName] = cf;
        }

        public static void LoadHairXml(GraphicsDevice gfxd)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("wz\\Misc\\00041656.img.xml");
            XmlNode default_ = doc["xmldump"]["wzimg"].SelectNodes("imgdir[@name='default']")[0];
            foreach (XmlNode component in default_.SelectNodes("canvas"))
            {
                string componentName = component.Attributes["name"].Value;
                string imgName = "default" + "." + componentName;
                short x = short.Parse(component["vector"].Attributes["x"].Value);
                short y = short.Parse(component["vector"].Attributes["y"].Value);
                ComponentFrame cf = new ComponentFrame(gfxd, "wz\\Misc\\00041656.atlas.png", new Vector2(x, y), EquipTypes.Misc, "00041656", imgName);
                XmlNodeList mapNodes = component.SelectNodes("imgdir[@name='map']");
                if (mapNodes.Count == 1)
                {
                    XmlNodeList brow = mapNodes[0].SelectNodes("vector[@name='brow']");
                    if (brow.Count == 1)
                    {
                        cf.brow = new Vector2(short.Parse(brow[0].Attributes["x"].Value), short.Parse(brow[0].Attributes["y"].Value));
                    }
                }
                StaticSprites[imgName] = cf;
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
                ComponentFrame cf = new ComponentFrame(gfxd, "wz\\Face\\00020644.atlas.png", new Vector2(x, y), EquipTypes.Face, "00020644", imgName);
                XmlNodeList mapNodes = component.SelectNodes("imgdir[@name='map']");
                if (mapNodes.Count == 1)
                {
                    XmlNodeList brow = mapNodes[0].SelectNodes("vector[@name='brow']");
                    if (brow.Count == 1)
                    {
                        cf.brow = new Vector2(short.Parse(brow[0].Attributes["x"].Value), short.Parse(brow[0].Attributes["y"].Value));
                    }
                }
                StaticSprites[imgName] = cf;
            }
            XmlNode blink = doc["imgdir"].SelectNodes("imgdir[@name='blink']")[0];
            foreach (XmlNode frame in blink.ChildNodes)
            {
                string frameNum = frame.Attributes["name"].Value;
                foreach (XmlNode component in frame.SelectNodes("canvas"))
                {
                    string componentName = component.Attributes["name"].Value;
                    string imgName = ResolveInLink(component);
                    string outLink = ResolveOutLink(component);
                    string imgPath = "wz\\Face\\00020644.atlas.png";
                    string id = "00020644";
                    if (outLink != null)
                    {
                        imgPath = "wz\\Face\\" + outLink.Split('\\')[0].Split(".img")[0] + ".atlas.png";
                        imgName = outLink.Split('\\').Last();
                        id = outLink.Split('\\').First();
                    }
                    string keyName = blink.Attributes["name"].Value + "." + frameNum + "." + componentName;
                    imgName = imgName == null ? keyName : imgName;
                    short x = short.Parse(component["vector"].Attributes["x"].Value);
                    short y = short.Parse(component["vector"].Attributes["y"].Value);
                    ComponentFrame cf = new ComponentFrame(gfxd, imgPath, new Vector2(x, y), EquipTypes.Face, id, imgName);
                    XmlNodeList mapNodes = component.SelectNodes("imgdir[@name='map']");
                    if (mapNodes.Count == 1)
                    {
                        XmlNodeList brow = mapNodes[0].SelectNodes("vector[@name='brow']");
                        if (brow.Count == 1)
                        {
                            cf.brow = new Vector2(short.Parse(brow[0].Attributes["x"].Value), short.Parse(brow[0].Attributes["y"].Value));
                        }
                    }
                    StaticSprites[keyName] = cf;
                }
            }
        }

        public static void MapStrings()
        {
            AnimationStrings.Add("stand1", Animations.Idle);
            AnimationStrings.Add("walk1", Animations.Walk);
            AnimationStrings.Add("walk2", Animations.Walk2);
            AnimationStrings.Add("default", Animations.Default);
            AnimationStrings.Add("alert", Animations.Alert);
            AnimationStrings.Add("swingO1", Animations.SwingO1);
            AnimationStrings.Add("proneStab", Animations.DuckStab);
            AnimationStrings.Add("prone", Animations.Duck);
        }

        private static void LoadAnimation(XmlNode node, GraphicsDevice gfxd, EquipTypes et, string id, string imgNameIn)
        {
            string imgPath;
            imgPath = "wz\\" + et + "\\" + id + ".atlas.png";
            Dictionary<string, Dictionary<int, ComponentFrame>> Books = new Dictionary<string, Dictionary<int, ComponentFrame>>();
            Animations animation = AnimationStrings[node.Attributes["name"].Value];
            List<int> delays = new List<int>();
            foreach (XmlNode frame in node.ChildNodes)
            {
                string frameNum = frame.Attributes["name"].Value;
                XmlNodeList xmlComponents = frame.SelectNodes("canvas");
                Dictionary<string, XmlNode> componentMap = new Dictionary<string, XmlNode>();
                foreach (XmlNode xmlComponent in xmlComponents)
                {
                    componentMap.Add(xmlComponent.Attributes["name"].Value, xmlComponent);
                }
                foreach (XmlNode uol in frame.SelectNodes("uol"))
                {
                    XmlNode target = ResolveXmlPath(uol, uol.Attributes["value"].Value);
                    componentMap.Add(uol.Attributes["name"].Value, target);
                }
                foreach (string componentMapName in componentMap.Keys)
                {   
                    XmlNode component = componentMap[componentMapName];
                    string componentName = component.Attributes["name"].Value;
                    if (!Books.ContainsKey(componentMapName))
                    {
                        Books.Add(componentMapName, new Dictionary<int, ComponentFrame>());
                    }
                    string imgName = ResolveInLink(component);
                    string outLink = ResolveOutLink(component);
                    string origImgPath = imgPath;

                    if (outLink != null)
                    {
                        imgPath = "wz\\" + et + "\\" + outLink.Split('\\')[0].Split(".img")[0] + ".atlas.png";
                        imgName = outLink.Split('\\').Last();
                        id = outLink.Split('\\').First();
                    }
                    imgName = imgName == null ? imgNameIn + node.Attributes["name"].Value + "." + frameNum + "." + componentName : imgName;
                    short x = short.Parse(component["vector"].Attributes["x"].Value);
                    short y = short.Parse(component["vector"].Attributes["y"].Value);
                    ComponentFrame cf = new ComponentFrame(gfxd, imgPath, new Vector2(x, y), et, id, imgName);
                    imgPath = origImgPath;
                    XmlNodeList mapNodes = component.SelectNodes("imgdir[@name='map']");
                    if (mapNodes.Count == 1)
                    {
                        XmlNodeList neck = mapNodes[0].SelectNodes("vector[@name='neck']");
                        if (neck.Count == 1)
                        {
                            cf.neck = new Vector2(short.Parse(neck[0].Attributes["x"].Value), short.Parse(neck[0].Attributes["y"].Value));
                        }
                        XmlNodeList navel = mapNodes[0].SelectNodes("vector[@name='navel']");
                        if (navel.Count == 1)
                        {
                            cf.navel = new Vector2(short.Parse(navel[0].Attributes["x"].Value), short.Parse(navel[0].Attributes["y"].Value));
                        }
                        XmlNodeList hand = mapNodes[0].SelectNodes("vector[@name='hand']");
                        if (hand.Count == 1)
                        {
                            cf.hand = new Vector2(short.Parse(hand[0].Attributes["x"].Value), short.Parse(hand[0].Attributes["y"].Value));
                        }
                        XmlNodeList handMove = mapNodes[0].SelectNodes("vector[@name='handMove']");
                        if (handMove.Count == 1)
                        {
                            cf.handMove = new Vector2(short.Parse(handMove[0].Attributes["x"].Value), short.Parse(handMove[0].Attributes["y"].Value));
                        }
                    }
                    Books[componentMapName].Add(Int32.Parse(frameNum), cf);
                }
                if (node.SelectNodes("int[@name='delay']") != null && !Skeleton.Duration.ContainsKey(animation))
                {
                    delays.Add(Int32.Parse(frame.SelectNodes("int[@name='delay']")[0].Attributes["value"].Value));
                }
            }
            foreach (KeyValuePair<string, Dictionary<int, ComponentFrame>> entry in Books)
            {
                Skeleton.AddBook(entry.Key, animation, entry.Value);
            }
            if (!Skeleton.Duration.ContainsKey(animation))
            {
                Skeleton.Duration[animation] = delays;
            }
        }

        private static string ResolveInLink(XmlNode node)
        {
            XmlNodeList inLink = node.SelectNodes("string[@name='_inlink']");
            if (inLink.Count > 0)
            {
                return inLink[0].Attributes["value"].Value.Replace('/', '.');
            }
            return null;
        }

        private static string ResolveOutLink(XmlNode node)
        {
            XmlNodeList inLink = node.SelectNodes("string[@name='_outlink']");
            if (inLink.Count > 0)
            {
                string[] sp = inLink[0].Attributes["value"].Value.Split('/');
                return sp[2].Split(".img").First() + "\\" + sp[3] + "." + sp[4] + "." + sp[5];
            }
            return null;
        }

        private static void ResolveUol(XmlNode node, Dictionary<string, List<ComponentFrame>> books)
        {
            foreach (XmlNode uol in node.SelectNodes("uol"))
            {
                string componentName = uol.Attributes["name"].Value;
                if (!books.ContainsKey(componentName))
                {
                    books.Add(componentName, new List<ComponentFrame>());
                }
                string[] strings = uol.Attributes["value"].Value.Split('/');
                string target = strings[2];
                foreach (string s in strings)
                {
                    if (AnimationStrings.ContainsKey(s))
                    {
                        books[componentName].Add(Skeleton.Sk[strings[4]][AnimationStrings[s]][Int32.Parse(strings[3])]);
                        goto eol;
                    }
                }
                books[strings[2]].Add(books[strings[2]][Int32.Parse(strings[1])]);
            eol: { }
            }
        }

        private static XmlNode ResolveXmlPath(XmlNode node, string path)
        {
            string[] strings = path.Split('/');
            XmlNode ret = node.ParentNode;
            foreach (string directory in strings)
            {
                if (directory == "..")
                {
                    ret = ret.ParentNode;
                }
                else
                {
                    XmlNodeList res = ret.SelectNodes("*[@name='" + directory + "']");
                    if (res.Count != 1)
                    {
                        throw new Exception("Ambiguous or invalid xml path found: " + path);
                    }
                    ret = res[0];
                }
            }
            return ret;
        }
    }

    class ComponentFrame
    {
        private static Dictionary<string, Texture2D> sheets = new Dictionary<string, Texture2D>();
        private string sheetKey;

        public Vector2 origin;
        public Vector2 neck;
        public Vector2 navel;
        public Vector2 hand;
        public Vector2 handMove;
        public Vector2 brow;
        public Rectangle spriteLoc;

        public ComponentFrame(GraphicsDevice gfxd, string imgPath, Vector2 origin, EquipTypes et, string id, string imgName)
        {
            sheetKey = imgPath.Split('\\').Last();
            if (!sheets.ContainsKey(sheetKey))
            {
                FileStream fileStream = new FileStream(imgPath, FileMode.Open);
                sheets.Add(sheetKey, Texture2D.FromStream(gfxd, fileStream));
                fileStream.Dispose();
            }
            XmlDocument doc = new XmlDocument();
            doc.Load("wz\\" + et + "\\" + id + ".atlas.xml");

            XmlNode node = doc["TextureAtlas"].SelectNodes("sprite[@n='" + imgName + ".png" + "']")[0];
            spriteLoc = new Rectangle(short.Parse(node.Attributes["x"].Value), short.Parse(node.Attributes["y"].Value),
                                      short.Parse(node.Attributes["w"].Value), short.Parse(node.Attributes["h"].Value));
            this.origin = origin;
        }

        public Texture2D Sprite
        {
            get
            {
                return sheets[sheetKey];
            }
        }

    }
}
