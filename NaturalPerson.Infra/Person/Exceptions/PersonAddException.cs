using System.Runtime.Serialization;

namespace NaturalPersonl.Infra.Person.Exceptions
{
    [Serializable]
    public class PersonAddException : Exception
    {
        public PersonAddException()
        {
        }

        public PersonAddException(string? message) : base(message)
        {
        }

        public PersonAddException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PersonAddException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}