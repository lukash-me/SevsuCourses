using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;
using Task = labs_ud.Application.Entities.Task;


namespace labs_ud.Application.Create;

public class CreateTaskService
{
    private readonly TaskRepository _taskRepository;

    public CreateTaskService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var themeId = request.ThemeId;

        var title = request.Title;

        var condition = request.Condition;

        var minMark = request.MinMark;

        var maxMark = request.MaxMark;
        
        var taskResult = Task.Create(
            themeId,
            title,
            condition,
            minMark,
            maxMark);

        if (taskResult.IsFailure)
            return taskResult.Error;
        
        await _taskRepository.Add(taskResult.Value, cancellationToken);

        return taskResult.Value.Id;
    }
}