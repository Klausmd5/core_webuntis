namespace CorePlugin.Plugin.Exceptions;

public class BadDateException : NotFoundException
{
    public BadDateException() : base($"From date cannot be after to date")
    {
    }
}
