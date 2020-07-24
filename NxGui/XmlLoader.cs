using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsFormsApp1
{
    class XmlLoader
    {
        public static Dictionary<string, string> items = new Dictionary<string, string>();

        public static void LoadStrings(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            foreach (XmlNode node in doc["imgdir"].ChildNodes)
            {
                //string id = node.Attributes["name"].Value;
                //string name = node["string"].Attributes["value"].Value;
                //items.Add(name, id);
            }
        }
    }
}
