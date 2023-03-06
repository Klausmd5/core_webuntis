namespace CorePlugin.Plugin.Exceptions;

public class StudentNotFoundException : NotFoundException
{
    public StudentNotFoundException(int id) : base($"Student with id {id} does not exist")
    {
    }
}
