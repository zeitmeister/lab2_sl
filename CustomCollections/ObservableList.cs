using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomDatastructures.Core;

namespace CustomCollections
{
    // Make this class generic by adding a type-parameter to the class
    public class ObservableList<T> : IEnumerable<T>, IObservableList<T>
    {
        // Declare an private variable, internalList, to work as 
        // the internal data storage for the list
        private List<T> internalList = new List<T>();

        public event EventHandler<ListChangedEventArgs<T>> Changed;

        public event EventHandler<RejectableEventArgs<T>> BeforeChange;

        //public delegate void EventHandler(object sender, ListChangedEventArgs<T> eventArgs);
        public void Add(T item)
        {
            var beforeChangeArg = new RejectableEventArgs<T>(Operation.Add, item, internalList.Count);
            internalList.Add(item);
            var arg = new ListChangedEventArgs<T>(Operation.Add, item, internalList.Count);          
            OnChanged(arg);
            
        }

        public void Remove(T item)
        {
            internalList.Remove(item);
            var arg = new ListChangedEventArgs<T>(Operation.Remove, item, internalList.Count);
            OnChanged(arg);
        }

        public bool Contains(T item)
        {
            return internalList.Contains(item);
        }

        protected virtual void OnBeforeChange(RejectableEventArgs<T> eventArgs)
        {
            var handler = BeforeChange;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }
        
        protected virtual void OnChanged(ListChangedEventArgs<T> eventArgs)
        {
            var handler = Changed;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
            
        }



        // The GetEnumerator methods is required by the IEnumerable and IEnumerable<T>
        // interfaces and you could use the following implementations for these methods.
        // internalList is the internal data storage for the ObservableList,
        // you should replace it with the name of your list instead.

        public IEnumerator<T> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)internalList).GetEnumerator();
        }

        public bool TryRemove(T item)
        {
            throw new NotImplementedException();
        }

        public bool TryAdd(T item)
        {
            throw new NotImplementedException();
        }
    }

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
