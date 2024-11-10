using System.Runtime.Serialization;

namespace FornecedoresApi.Domain.Exceptions
{
    public class NoContentException : InvalidOperationException
    {
        public NoContentException() { }
        public NoContentException(string message) : base (message) { }
        public NoContentException(string message, Exception innerException) : base(message, innerException) { }
        public NoContentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
