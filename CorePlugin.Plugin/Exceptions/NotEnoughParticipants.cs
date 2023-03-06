namespace CorePlugin.Plugin.Exceptions;

public class NotEnoughParticipants : NotFoundException
{
    public NotEnoughParticipants() : base($"There need to be at least 2 participants")
    {
    }
}
