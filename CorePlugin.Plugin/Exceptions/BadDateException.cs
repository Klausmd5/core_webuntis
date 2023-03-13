namespace CorePlugin.Plugin.Exceptions;

public class BadDateException : BadRequestException
{
    public BadDateException() : base($"\"from\" cannot be after \"to\"")
    {
    }
}
