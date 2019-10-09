namespace CustomCollections
{
    using System;


        public class OperationRejectedException : InvalidOperationException
        {
            public OperationRejectedException()
            {

            }
            public OperationRejectedException(string Message) : base(Message)
            {

            }
            public OperationRejectedException(string Message, Exception exception) : base(Message, exception)
            {

            }
        }
    
}
