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
        private static ObservableList<string> list = new ObservableList<string>();

        Subscriber subscriber = new Subscriber("sub1", list);
        TestSubscriber testSubscriber = new TestSubscriber("sub2", list);
        TestSubscriber2 testSubscriber2 = new TestSubscriber2("sub3", list);

        public TestApp()
        {
            InitializeComponent();
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            /*Subscriber subscriber = new Subscriber("sub1", list);
            TestSubscriber testSubscriber = new TestSubscriber("sub2", list);
            TestSubscriber2 testSubscriber2 = new TestSubscriber2("sub3", list);*/
            
            
                list.Add(textBox1.Text);
            
            

            if (!testSubscriber2.Rejected)
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

        private void TestApp_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*Subscriber subscriber = new Subscriber("sub1", list);
            TestSubscriber testSubscriber = new TestSubscriber("sub2", list);
            TestSubscriber2 testSubscriber2 = new TestSubscriber2("sub3", list);*/

            var objToBeRemoved = listBox1.SelectedItem;
            listBox1.Items.Remove(objToBeRemoved);
            list.Remove(objToBeRemoved.ToString());
            System.Windows.Forms.MessageBox.Show(objToBeRemoved.ToString() + " was removed to the list. The list now contains " + subscriber.Count + " items.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*Subscriber subscriber = new Subscriber("sub1", list);
            TestSubscriber testSubscriber = new TestSubscriber("sub2", list);
            TestSubscriber2 testSubscriber2 = new TestSubscriber2("sub3", list);*/

            if (list.Contains(textBox1.Text))
            {
                System.Windows.Forms.MessageBox.Show("'" + textBox1.Text + "'" + " is in the list");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("There is no " + "'" + textBox1.Text + "'" + " in the list");
            }

        }
    }
}
