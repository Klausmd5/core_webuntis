namespace CorePlugin.Plugin.Exceptions;

public class NotEnoughParticipants : NotFoundException
{
    public NotEnoughParticipants() : base($"At least 2 participants are required")
    {
    }
}
