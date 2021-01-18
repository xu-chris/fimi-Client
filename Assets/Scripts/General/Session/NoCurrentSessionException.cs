using System;

namespace General.Session
{
    public class NoCurrentSessionException : Exception
    {
        public NoCurrentSessionException() : base() { }
        public NoCurrentSessionException(string message) : base(message) { }
        public NoCurrentSessionException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected NoCurrentSessionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}