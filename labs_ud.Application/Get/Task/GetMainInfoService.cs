using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Task;

public class GetMainInfoService
{
    private readonly TaskRepository _taskRepository;

    public GetMainInfoService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<MainInfoDto, Error>> Handle(TaskId taskId, CancellationToken cancellationToken = default)
    {
        var taskResult = await _taskRepository.GetById(taskId, cancellationToken);
        var task = taskResult.Value;
        
        var title = task.Title;
        var condition = task.Condition;
        var attempsAmount = task.AttempsAmount;
        var minMark = task.MinMark;
        var maxMark = task.MaxMark;

        var response = new MainInfoDto(title, condition, attempsAmount, minMark, maxMark);

        return response;
    }
}