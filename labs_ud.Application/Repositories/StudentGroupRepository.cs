using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Student;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;

namespace labs_ud.Application.Repositories;

public class StudentGroupRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public StudentGroupRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(StudentGroup studentGroup, CancellationToken cancellationToken = default)
    {
        await _dbContext.StudentGroup.AddAsync(studentGroup, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return studentGroup.Id;
    }

    public async Task<Result<StudentGroup, Error>> GetById(
        StudentGroupId studentGroupId,
        CancellationToken cancellationToken = default)
    {
        var studentGroup = await _dbContext.StudentGroup
            .FirstOrDefaultAsync(sg => sg.Id == studentGroupId, cancellationToken);

        if (studentGroup is null)
        {
            return Errors.Errors.General.NotFound(studentGroupId.Value);
        }

        return studentGroup;
    }
    
    public async Task<Result<List<StudentGroup>, Error>> GetByStudentId(
        StudentId studentId,
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.StudentGroup
            .Where(sg => sg.StudentId == studentId)
            .ToListAsync(cancellationToken);
        
        if (result.Count == 0)
        {
            return Errors.Errors.General.NotFound(studentId.Value);
        }

        return result;
    }
    
    public async Task<Result<List<StudentGroup>, Error>> GetByGroupId(
        GroupId groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.StudentGroup
            .Where(sg => sg.GroupId == groupId)
            .ToListAsync(cancellationToken);
        
        if (result.Count == 0)
        {
            return Errors.Errors.General.NotFound(groupId.Value);
        }

        return result;
    }
    
    public Guid Delete(StudentGroup studentGroup, CancellationToken cancellationToken = default)
    {
        _dbContext.StudentGroup.Remove(studentGroup);
        _dbContext.SaveChanges();

        return studentGroup.Id;
    }
}