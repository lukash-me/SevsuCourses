using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Student;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;
using Task = labs_ud.Application.Entities.Task;

namespace labs_ud.Application.Repositories;

public class StudentRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public StudentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(Student student, CancellationToken cancellationToken = default)
    {
        await _dbContext.Student.AddAsync(student, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return student.Id;
    }

    public async Task<Result<Student, Error>> GetById(
        StudentId studentId,
        CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Student
            .FirstOrDefaultAsync(v => v.Id == studentId, cancellationToken);

        if (student is null)
        {
            return Errors.Errors.General.NotFound(studentId.Value);
        }

        return student;
    }
    
    public async Task<Result<List<Student>, Error>> GetByGroupId(
        GroupId groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Student
            .Where(s => s.GroupId == groupId)
            .ToListAsync(cancellationToken);
        
        if (result.Count == 0)
        {
            return Errors.Errors.General.NotFound(groupId.Value);
        }

        return result;
    }
    
    public async Task<Result<Student, Error>> GetByLoginPassword(
        StudentRequest request, 
        CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Student
            .FirstOrDefaultAsync(s => s.Login == request.Login 
                                            && s.Password == request.Password, 
                                            cancellationToken);
            
        if (student is null)
        {
            return Errors.Errors.General.NotFound();
        }
        
        return student;
    } 
    
    public Guid Delete(Student student, CancellationToken cancellationToken = default)
    {
        _dbContext.Student.Remove(student);
        _dbContext.SaveChanges();

        return student.Id;
    }
}