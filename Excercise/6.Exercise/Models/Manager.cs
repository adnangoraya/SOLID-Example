namespace _6.Exercise.Models;

public sealed class Manager : IEmployee
{
    public void AssignTask() { }
    public void CreateSubTask() { }

    public void WorkOnTask() => throw new NotSupportedException("Manager doesn't work on tasks");

    public void ApproveBudget() { }
    public void AttendHiringLoop() { }
}
