using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Task;

public class GetTaskByIdService
{
    private readonly TaskRepository _taskRepository;

    public GetTaskByIdService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<TaskResponse, Error>> Handle(TaskId taskId, CancellationToken cancellationToken = default)
    {
        var taskResult = await _taskRepository.GetById(taskId, cancellationToken);
        var task = taskResult.Value;
        
        
        var id = task.Id;
        var themeId = task.ThemeId;
        var title = task.Title;
        var condition = task.Condition;
        var minMark = task.MinMark;
        var maxMark = task.MaxMark;

        var response = new TaskResponse(id, themeId, title, condition, minMark, maxMark);

        return response;
    }
}

public record TaskResponse(
    Guid Id,
    Guid ThemeId,
    string Title,
    string Condition,
    int MinMark,
    int MaxMark);