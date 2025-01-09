using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete;

public class DeleteTaskService
{
    private readonly TaskRepository _taskRepository;

    public DeleteTaskService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        DeleteTaskRequest request, 
        CancellationToken cancellationToken = default)
    {
        var taskResult = await _taskRepository.GetById(request.TaskId, cancellationToken);
        if (taskResult.IsFailure)
            return taskResult.Error;

        _taskRepository.Delete(taskResult.Value, cancellationToken);

        return taskResult.Value.Id;
    }
}