using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomCollections;

namespace CustomCollectionsTestApp
{
    /// <summary>
    /// Test application for ObservableList.
    /// </summary>
    public partial class TestApp : Form
    {
        private ObservableList<string> list = new ObservableList<string>();
        public TestApp()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            

            Subscriber subscriber = new Subscriber("sub1", list);
            list.Add(textBox1.Text);

            listBox1.Items.Add(textBox1.Text);

            if(subscriber.Operation == "Add")
            {
                System.Windows.Forms.MessageBox.Show(textBox1.Text + " was added to the list. The list now contains " + subscriber.Count + " items.");
            }
            
        }



        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
