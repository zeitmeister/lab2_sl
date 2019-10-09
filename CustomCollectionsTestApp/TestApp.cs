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
        private static ObservableList<object> list = new ObservableList<object>();

        /*Subscriber subscriber = new Subscriber("sub1", list);
        TestSubscriber testSubscriber = new TestSubscriber("sub2", list);
        TestSubscriber2 testSubscriber2 = new TestSubscriber2("sub3", list);*/

        public TestApp()
        {
            InitializeComponent();
            list.Changed += List_Changed;
        }

        private void List_Changed(object sender, CustomDatastructures.Core.ListChangedEventArgs<object> e)
        {
            if (e.Operation.ToString() == "Add")
            {
                listBox1.Items.Add(e.Value);
                MessageBox.Show(e.Value + " was added to the list. The list now contains " + e.Count + " items");
            }
            if (e.Operation.ToString() == "Remove")
            {
                listBox1.Items.Remove(e.Value);
                MessageBox.Show(e.Value + " was removed from the list. The list now contains " + e.Count + " items");
            }
        }

        public void Button1_Click(object sender, EventArgs e)
        {   
            list.Add(textBox1.Text);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TestApp_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (list.IsEmpty)
                {
                    MessageBox.Show("You can't remove from an empty list");
                }
                else
                {
                    list.Remove(listBox1.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (list.IsEmpty)
            {
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (list.Contains(textBox1.Text))
            {
                MessageBox.Show("'" + textBox1.Text + "'" + " is in the list");
            }
            else if (list.IsEmpty)
            {
                MessageBox.Show("There is no items in the list");
            }
            else if(textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Please enter an item to check");
            }
            else
            {
                MessageBox.Show("There is no " + "'" + textBox1.Text + "'" + " in the list");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
