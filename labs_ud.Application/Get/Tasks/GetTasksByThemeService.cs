using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Tasks;

public class GetTasksByThemeService
{
    private readonly TaskRepository _taskRepository;

    public GetTasksByThemeService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    public async Task<Result<List<TaskByThemeResponse>, Error>> Handle(Guid themeId, CancellationToken cancellationToken = default)
    {
        var tasksResult = await _taskRepository.GetByThemeId(themeId, cancellationToken);
        var tasks = tasksResult.Value;
        
        var response = new List<TaskByThemeResponse>();
        
        foreach (var task in tasks)
        {
            var id = task.Id;
            var title = task.Title;
            
            var taskResponse = new TaskByThemeResponse(id, title);
            response.Add(taskResponse);
        }

        return response;
    }
}

public record TaskByThemeResponse
(
    Guid Id,
    string Title);