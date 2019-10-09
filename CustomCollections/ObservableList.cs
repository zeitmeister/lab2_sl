using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomDatastructures.Core;

namespace CustomCollections
{
    using global::CustomCollections.CustomCollections;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace CustomCollections
    {
    }

    public class ObservableList<T> : IEnumerable<T>, IObservableList<T>
    {
        private List<T> internalList = new List<T>();
        public event EventHandler<ListChangedEventArgs<T>> Changed;
        public event EventHandler<RejectableEventArgs<T>> BeforeChange;

        public bool IsEmpty
        {
            get { return internalList.Count == 0; }
        }
        
        public void Add(T item)
        {
            if (!PrepareListChange(item, Operation.Add))
            {
                throw new OperationRejectedException("You can't add this item");
            }
        }

        public void Remove(T item)
        {
            if (!Contains(item))
            {
                throw new InvalidOperationException("You have to select an item to be able to remove something form the list");
            }
            if (!PrepareListChange(item, Operation.Remove))
            {
                throw new OperationRejectedException();
            }
        }

        public bool Contains(T item)
        {
            return internalList.Contains(item);
        }

        protected virtual void OnBeforeChange(RejectableCustomEventArgs<T> eventArgs)
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


        public bool TryRemove(T item)
        {
            if (!internalList.Contains(item) || (!PrepareListChange(item, Operation.Remove)))
            {
                return false;
            } else
            {
                return true;
            }
        }

        public bool TryAdd(T item)
        {
            if (PrepareListChange(item, Operation.Add))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool PrepareListChange(T item, Operation operation)
        {
            var rejArg = new RejectableCustomEventArgs<T>(operation, item, internalList.Count);
            OnBeforeChange(rejArg);
            if (!rejArg.IsOperationRejected)
            {
                if (operation.ToString() == "Add")
                {
                    internalList.Add(item);
                } else
                {
                    internalList.Remove(item);
                }
                var arg = new ListChangedEventArgs<T>(operation, item, internalList.Count);
                OnChanged(arg);
                return true;
            } else
            {
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)internalList).GetEnumerator();
        }

    }
}
