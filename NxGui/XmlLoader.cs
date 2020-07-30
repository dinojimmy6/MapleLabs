using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Gma.DataStructures.StringSearch;

namespace WindowsFormsApp1
{
    class XmlLoader
    {
        public static Trie<EquipEntry> Trie = new Trie<EquipEntry>();

        public static void LoadStrings(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            foreach (XmlNode node in doc["imgdir"].ChildNodes)
            {
                string id = node.Attributes["name"].Value;
                string name = null;
                if(node["string"] != null)
                {
                    name = node["string"].Attributes["value"].Value;
                    Trie.Add(name, new EquipEntry(name, id));
                }  
            }
        }

    }

    public class EquipEntry : Tuple<string, string, EquipTypes>
    {

        public EquipEntry(string name, string id) : base(name, id, EquipTypesExtension.GetEquipTypeFromId(id))
        {
        }

        public override string ToString()
        {
            return Item1;
        }
    }
}
