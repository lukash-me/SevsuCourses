using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Answer;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;

namespace labs_ud.Application.Repositories;

public class AnswerRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public AnswerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(Answer answer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Answer.AddAsync(answer, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return answer.Id;
    }
    
    public async Task<Result<Answer, Error>> GetById(
        AnswerId answerId, 
        CancellationToken cancellationToken = default)
    {
        var answer = await _dbContext.Answer
            .FirstOrDefaultAsync(v => v.Id == answerId, cancellationToken);
        
        if (answer is null)
        {
            return Errors.Errors.General.NotFound(answerId.Value);
        }
        
        return answer;
    } 
    
    public async Task<Result<List<Answer>, Error>> GetByTaskIdStudentId(
        AnswersRequest request, 
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Answer
            .Where(a => a.TaskId == request.TaskId && a.StudentId == request.StudentId)
            .ToListAsync(cancellationToken);
        
        if (result.Count == 0)
        {
            return Errors.Errors.General.NotFound();
        }
        
        return result;
    } 
    
    public Guid Delete(Answer answer, CancellationToken cancellationToken = default)
    {
        _dbContext.Answer.Remove(answer);
        _dbContext.SaveChanges();

        return answer.Id;
    }
}