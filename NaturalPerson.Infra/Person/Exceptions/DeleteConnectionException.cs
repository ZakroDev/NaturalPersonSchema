using System.Runtime.Serialization;

namespace NaturalPersonl.Infra.Person.Exceptions
{
    [Serializable]
    public class DeleteConnectionException : Exception
    {
        public DeleteConnectionException()
        {
        }

        public DeleteConnectionException(string? message) : base(message)
        {
        }

        public DeleteConnectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DeleteConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}