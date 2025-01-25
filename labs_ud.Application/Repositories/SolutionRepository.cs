using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;

namespace labs_ud.Application.Repositories;

public class SolutionRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public SolutionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(Solution solution, CancellationToken cancellationToken = default)
    {
        await _dbContext.Solution.AddAsync(solution, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return solution.Id;
    }
    
    public async Task<Result<Solution, Error>> GetById(
        SolutionId solutionId, 
        CancellationToken cancellationToken = default)
    {
        var solution = await _dbContext.Solution
            .FirstOrDefaultAsync(v => v.Id == solutionId, cancellationToken);
        
        if (solution is null)
        {
            return Errors.Errors.General.NotFound(solutionId.Value);
        }
        
        return solution;
    } 
    
    public async Task<Result<List<Solution>, Error>> GetByTaskId(
        TaskId taskId, 
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Solution
            .Where(s => s.TaskId == taskId)
            .ToListAsync(cancellationToken);
        
        if (result.Count == 0)
        {
            return Errors.Errors.General.NotFound(taskId.Value);
        }
        
        return result;
    } 
    
    public Guid Delete(Solution solution, CancellationToken cancellationToken = default)
    {
        _dbContext.Solution.Remove(solution);
        _dbContext.SaveChanges();

        return solution.Id;
    }
}