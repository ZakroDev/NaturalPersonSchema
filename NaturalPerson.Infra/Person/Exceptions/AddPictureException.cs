using System.Runtime.Serialization;

namespace NaturalPersonl.Infra.Person.Exceptions
{
    [Serializable]
    public class AddPictureException : Exception
    {
        public AddPictureException()
        {
        }

        public AddPictureException(string? message) : base(message)
        {
        }

        public AddPictureException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AddPictureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}