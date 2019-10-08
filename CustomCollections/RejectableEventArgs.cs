using CustomDatastructures.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollections
{
    public class RejectableEventArgs<T> : ListChangedEventArgs<T>
    {
        private bool _isOperationRejected;
        public Operation Operation { get; set; }
        public T Value { get; set; }
        public int Count { get; set; }


        public RejectableEventArgs(Operation operation, T value, int count) : base(operation, value, count)
        {
            IsOperationRejected = _isOperationRejected;
            Operation = operation;
            Value = value;
            Count = count;
            //VAFALLS?
        }

        

        //public RejectableEventArgs(Operation operation, T value, int count) : base()
        //{
        //    IsOperationRejected = _isOperationRejected;
        //    Operation = operation;
        //    Value = value;
        //    Count = count;
        //}

        public  bool IsOperationRejected { get; }




        public void RejectOperation()
        {
            _isOperationRejected = true;
        }
    }
}
