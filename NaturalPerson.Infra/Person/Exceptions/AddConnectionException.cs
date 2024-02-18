using System.Runtime.Serialization;

namespace NaturalPersonl.Infra.Person.Exceptions
{
    [Serializable]
    public class AddConnectionException : Exception
    {
        public AddConnectionException()
        {
        }

        public AddConnectionException(string? message) : base(message)
        {
        }

        public AddConnectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AddConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}