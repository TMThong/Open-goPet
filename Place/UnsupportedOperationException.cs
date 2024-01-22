using System.Runtime.Serialization;

[Serializable]
internal class UnsupportedOperationException : Exception
{
    public UnsupportedOperationException()
    {
    }

    public UnsupportedOperationException(string? message) : base(message)
    {
    }

    public UnsupportedOperationException(string? message ? innerException) : base(message, innerException)
    {
    }

    protected UnsupportedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}