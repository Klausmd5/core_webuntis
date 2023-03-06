namespace CorePlugin.Plugin.Exceptions;

public class BadDateException : NotFoundException
{
    public BadDateException() : base($"\"From\" cannot be after \"To\"")
    {
    }
}
