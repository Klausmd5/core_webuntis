namespace CorePlugin.Plugin.Exceptions;

public class BadDate : BadRequestException
{
    public BadDate() : base("\"from\" cannot be after \"to\"")
    {
    }
}
