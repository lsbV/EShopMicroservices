namespace BuildingBlocks.Exceptions;

public class BedRequestException : Exception
{
    public BedRequestException(string message) : base(message) { }
    public BedRequestException(string message, string details) : base(message)
    {
        Details = details;
    }
    public string? Details { get; set; }
}