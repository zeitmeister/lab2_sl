﻿using System;
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
                var rejArg = new RejectableCustomEventArgs<T>(Operation.Add, item, internalList.Count);
                OnBeforeChange(rejArg);
                if (!rejArg.IsOperationRejected)
                {
                    internalList.Add(item);
                    var arg = new ListChangedEventArgs<T>(Operation.Add, item, internalList.Count);
                    OnChanged(arg);
                }
            else
            {
                throw new OperationRejectedException("You can't add this item");
            }
        }

        public void Remove(T item)
        {
            if (!internalList.Contains(item))
            {
                throw new InvalidOperationException("You have to select an item to be able to remove something form the list");
            }
            var rejArg = new RejectableCustomEventArgs<T>(Operation.Remove, item, internalList.Count);
                OnBeforeChange(rejArg);
                if (!rejArg.IsOperationRejected)
                {
                    internalList.Remove(item);
                    var arg = new ListChangedEventArgs<T>(Operation.Remove, item, internalList.Count);
                    OnChanged(arg);
                }
            else
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
            if (!internalList.Contains(item))
            {
                return false;
            }
            var rejArg = new RejectableCustomEventArgs<T>(Operation.Remove, item, internalList.Count);
            OnBeforeChange(rejArg);
            if (!rejArg.IsOperationRejected)
            {
                internalList.Remove(item);
                var arg = new ListChangedEventArgs<T>(Operation.Remove, item, internalList.Count);
                OnChanged(arg);
                return true;
            }
            else return false;
        }

        public bool TryAdd(T item)
        {
            var rejArg = new RejectableCustomEventArgs<T>(Operation.Add, item, internalList.Count);
            OnBeforeChange(rejArg);
            if (!rejArg.IsOperationRejected)
            {
                internalList.Add(item);
                var arg = new ListChangedEventArgs<T>(Operation.Add, item, internalList.Count);
                OnChanged(arg);
                return true;
            }
            else return false;
        }
    }
}
