using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCollections;
using CustomDatastructures.Core;

namespace CustomCollections
{
    public class RejectableCustomEventArgs<T> : RejectableEventArgs<T>
    {
        private bool _isOperationRejected;
        public RejectableCustomEventArgs(Operation operation, T value, int count) : base(operation, value, count)
        {

        }

        public override bool IsOperationRejected
        {
            get { return _isOperationRejected; }
        }

        public override void RejectOperation()
        {
            _isOperationRejected = true;
        }
    }
}
