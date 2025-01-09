using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;

namespace labs_ud.Application.Repositories;
using Task = Entities.Task;

public class TaskRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public TaskRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(Task task, CancellationToken cancellationToken = default)
    {
        await _dbContext.Task.AddAsync(task, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return task.Id;
    }
    
    public async Task<Result<Task, Error>> GetById(
        TaskId taskId, 
        CancellationToken cancellationToken = default)
    {
        var task = await _dbContext.Task
            .FirstOrDefaultAsync(v => v.Id == taskId, cancellationToken);
        
        if (task is null)
        {
            return Errors.Errors.General.NotFound(taskId.Value);
        }
        
        return task;
    } 
    
    public Guid Delete(Task task, CancellationToken cancellationToken = default)
    {
        _dbContext.Task.Remove(task);
        _dbContext.SaveChanges();

        return task.Id;
    }
    
    public async Task<Result<List<Task>, Error>> GetByThemeId(
        Guid themeId, 
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Task
            .Where(t => t.ThemeId == themeId)
            .ToListAsync(cancellationToken);
        
        return result;
    } 
}