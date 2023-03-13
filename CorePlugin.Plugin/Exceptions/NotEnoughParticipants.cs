namespace CorePlugin.Plugin.Exceptions;

public class NotEnoughParticipants : BadRequestException
{
    public NotEnoughParticipants() : base($"at least 2 participants are required")
    {
    }
}
