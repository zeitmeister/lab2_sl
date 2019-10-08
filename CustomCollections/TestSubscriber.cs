using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCollections;
using CustomDatastructures.Core;

namespace CustomCollections
{
    public class TestSubscriber
    {
        public int Count { get; set; }
        private string id;

        public TestSubscriber(string ID, ObservableList<string> pub)
        {
            id = ID;
            pub.BeforeChange += HandleBeforeChanged;
        }

        private void HandleBeforeChanged(object sender, RejectableEventArgs<string> e)
        {
            bool test = e.IsOperationRejected;
            //e.RejectOperation();
            Count = e.Count;
        }
    }
}
