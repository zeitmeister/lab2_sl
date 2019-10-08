using CustomCollections.CustomCollections;
using CustomDatastructures.Core;

namespace CustomCollections
{
    public class TestSubscriber2
    {
        private string id;

        public bool Rejected { get; set; }
        public TestSubscriber2(string ID, ObservableList<string> pub)
        {
            id = ID;
            pub.BeforeChange += HandleBeforeChanged;
        }

        private void HandleBeforeChanged(object sender, RejectableEventArgs<string> e)
        {
            if (e.Value == "lök")
            {
                e.RejectOperation();
                Rejected = true;
                //throw new OperationRejectedException();
            } // För test
        }
    }
}
