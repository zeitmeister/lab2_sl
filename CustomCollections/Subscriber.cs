using CustomDatastructures.Core;

namespace CustomCollections
{
    public class Subscriber
    {
        private string id;
        public string Operation { get; set; }
        public string Value { get; set; }

        public int Count { get; set; }
        public Subscriber(string ID, ObservableList<string> pub)
        {
            id = ID;
            pub.Changed += HandleChanged;
        }

        public void HandleChanged(object sender, ListChangedEventArgs<string> eventArgs)
        {
            Operation = eventArgs.Operation.ToString();
            Value = eventArgs.Value;
            Count = eventArgs.Count;
        }

        
    }
}
