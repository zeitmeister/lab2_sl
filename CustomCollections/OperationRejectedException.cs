using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollections
{
    class OperationRejectedException : InvalidOperationException
    {
        public OperationRejectedException()
        {

        }
        public OperationRejectedException(string Message) :base(Message)
        {

        }
        public OperationRejectedException(string Message, Exception exception) :base(Message, exception)
        {

        }
    }
}
