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
 
}