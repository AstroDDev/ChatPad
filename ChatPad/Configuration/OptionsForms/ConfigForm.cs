using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatPad.Configuration.OptionsForms
{
    public partial class ConfigForm : Form
    {
        public delegate void Save();

        public Save save;

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            addBTN1.Click += (s, evt) => { AddItem(0); };
            addBTN2.Click += (s, evt) => { AddItem(1); };
            addBTN3.Click += (s, evt) => { AddItem(2); };

            removeBTN1.Click += (s, evt) => { RemoveItem(0); };
            removeBTN2.Click += (s, evt) => { RemoveItem(1); };
            removeBTN3.Click += (s, evt) => { RemoveItem(2); };
        }

        private void saveBTN_Click(object sender, EventArgs e)
        {
            save();

            Close();
        }

        private void AddItem(int list)
        {
            ListBox listBox;
            TextBox textBox;

            switch (list)
            {
                case 0:
                    listBox = list1;
                    textBox = textBox1;
                    break;
                case 1:
                    listBox = list2;
                    textBox = textBox2;
                    break;
                case 2:
                    listBox = list3;
                    textBox = textBox3;
                    break;
                default:
                    listBox = list1;
                    textBox = textBox1;
                    break;
            }

            string input = textBox.Text.ToLower().Replace(" ", "").Replace("\t", "").Replace("\n", "");
            
            if (input.Length > 0 && !listBox.Items.Contains(input))
            {
                listBox.Items.Add(input);
            }

            textBox.Text = "";
        }

        private void RemoveItem(int list)
        {
            ListBox listBox;
            TextBox textBox;

            switch (list)
            {
                case 0:
                    listBox = list1;
                    textBox = textBox2;
                    break;
                case 1:
                    listBox = list2;
                    textBox = textBox1;
                    break;
                case 2:
                    listBox = list3;
                    textBox = textBox3;
                    break;
                default:
                    listBox = list1;
                    textBox = textBox2;
                    break;
            }

            if (listBox.SelectedIndex >= 0)
            {
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
        }
    }
}
