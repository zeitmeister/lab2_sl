namespace CustomCollections
{
    using System;

    namespace CustomCollections
    {
        class OperationRejectedException : InvalidOperationException
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
}
