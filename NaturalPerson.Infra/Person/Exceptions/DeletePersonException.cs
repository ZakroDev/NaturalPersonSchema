using System.Runtime.Serialization;

namespace NaturalPersonl.Infra.Person.Exceptions
{
    [Serializable]
    public class DeletePersonException : Exception
    {
        public DeletePersonException()
        {
        }

        public DeletePersonException(string? message) : base(message)
        {
        }

        public DeletePersonException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DeletePersonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}