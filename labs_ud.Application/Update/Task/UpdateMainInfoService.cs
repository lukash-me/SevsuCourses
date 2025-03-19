using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Task;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Task;

public class UpdateMainInfoService
{
    private readonly TaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMainInfoService(TaskRepository taskRepository, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var taskResult = await _taskRepository.GetById(request.TaskId, cancellationToken);
        if (taskResult.IsFailure)
            return taskResult.Error;
        
        taskResult.Value.UpdateMainInfo(
            request.Dto.Title,
            request.Dto.Condition,
            request.Dto.MinMark,
            request.Dto.MaxMark);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.TaskId;
    }
}

public record UpdateMainInfoRequest(
    Guid TaskId,
    MainInfoDto Dto
    );