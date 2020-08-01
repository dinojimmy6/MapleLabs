using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.DataStructures.StringSearch;


namespace WindowsFormsApp1
{
    public partial class GuiForm : Form
    {
        private HashSet<EquipTypes> Filter = new HashSet<EquipTypes>();

        public GuiForm()
        {
            InitializeComponent();
            var t = new Trie<String>();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            UpdateSearch();
        }

        private void SearchResult_DoubleClick(object sender, EventArgs e)
        {
            if(SearchResult.SelectedItem != null)
            {
                Program.sw.WriteLine("0" + ((Tuple<string, string, EquipTypes>) SearchResult.SelectedItem).Item2);
                Program.ev.Set();
            }
        }

        private void SearchResult_MouseClick(object sender, MouseEventArgs e)
        {
            if (SearchResult.SelectedItem != null)
            {
                var item = (Tuple<string, string, EquipTypes>) SearchResult.SelectedItem;
                string dir = "0" + item.Item2 + ".img\\";
                EquipTypes et = item.Item3;
                EquipIcon.Load("..\\Engine\\wz\\" + et + "\\" +  dir + dir + "info.icon.png");
            }
        }

        private void FilterOverall_CheckedChanged(object sender, EventArgs e)
        {
            HandleFilterEvent(EquipTypes.LongCoat, FilterOverall.Checked);
        }

        private void FilterShoes_CheckedChanged(object sender, EventArgs e)
        {
            HandleFilterEvent(EquipTypes.Shoes, FilterShoes.Checked);
        }

        private void FilterGloves_CheckedChanged(object sender, EventArgs e)
        {
            HandleFilterEvent(EquipTypes.Glove, FilterGloves.Checked);
        }

        private void FilterCape_CheckedChanged(object sender, EventArgs e)
        {
            HandleFilterEvent(EquipTypes.Cape, FilterCape.Checked);
        }

        private void HandleFilterEvent(EquipTypes et, bool isChecked)
        {
            if(isChecked)
            {
                Filter.Add(et);
            }
            else
            {
                Filter.Remove(et);
            }
            UpdateSearch();
        }

        private void UpdateSearch()
        {
            SearchResult.BeginUpdate();
            SearchResult.Items.Clear();
            foreach (EquipEntry item in XmlLoader.Trie.Retrieve(SearchBox.Text))
            {
                if(Filter.Contains(item.Item3))
                {
                    SearchResult.Items.Add(item);
                }
                
            }
            SearchResult.EndUpdate();
        }
    }
}
