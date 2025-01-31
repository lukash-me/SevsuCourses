using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Mentor;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;

namespace labs_ud.Application.Repositories;

public class MentorRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public MentorRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(Mentor mentor, CancellationToken cancellationToken = default)
    {
        await _dbContext.Mentor.AddAsync(mentor, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return mentor.Id;
    }
    
    public async Task<Result<List<Mentor>, Error>> GetAll(
        CancellationToken cancellationToken = default)
    {
        var mentors = await _dbContext.Mentor
            .ToListAsync(cancellationToken);
        
        if (mentors.Count == 0)
        {
            return Errors.Errors.General.NotFound();
        }
        
        return mentors;
    } 
    
    public async Task<Result<Mentor, Error>> GetById(
        MentorId mentorId, 
        CancellationToken cancellationToken = default)
    {
        var mentor = await _dbContext.Mentor
            .FirstOrDefaultAsync(v => v.Id == mentorId, cancellationToken);
        
        if (mentor is null)
        {
            return Errors.Errors.General.NotFound(mentorId.Value);
        }
        
        return mentor;
    } 
    
    public async Task<Result<Mentor, Error>> GetByLoginPassword(
        MentorRequest request, 
        CancellationToken cancellationToken = default)
    {
        var mentor = await _dbContext.Mentor
            .FirstOrDefaultAsync(s => s.Email == request.Email 
                                      && s.Password == request.Password, 
                cancellationToken);
            
        if (mentor is null)
        {
            return Errors.Errors.General.NotFound();
        }
        
        return mentor;
    } 
    
    public Guid Delete(Mentor mentor, CancellationToken cancellationToken = default)
    {
        _dbContext.Mentor.Remove(mentor);
        _dbContext.SaveChanges();

        return mentor.Id;
    }
}