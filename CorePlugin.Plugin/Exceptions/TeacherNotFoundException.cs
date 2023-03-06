namespace CorePlugin.Plugin.Exceptions;

public class TeacherNotFoundException : NotFoundException
{
    public TeacherNotFoundException(int id) : base($"Teacher with id {id} does not exist")
    {
    }
}
