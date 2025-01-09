using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Teacher;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;

namespace labs_ud.Application.Repositories;

public class TeacherRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public TeacherRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(Teacher teacher, CancellationToken cancellationToken = default)
    {
        await _dbContext.Teacher.AddAsync(teacher, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return teacher.Id;
    }
    
    public async Task<Result<Teacher, Error>> GetById(
        TeacherId teacherId, 
        CancellationToken cancellationToken = default)
    {
        var teacher = await _dbContext.Teacher
            .FirstOrDefaultAsync(v => v.Id == teacherId, cancellationToken);
        
        if (teacher is null)
        {
            return Errors.Errors.General.NotFound(teacherId.Value);
        }
        
        return teacher;
    } 
    
    public async Task<Result<Teacher, Error>> GetByLoginPassword(
        TeacherRequest request, 
        CancellationToken cancellationToken = default)
    {
        var teacher = await _dbContext.Teacher
            .FirstOrDefaultAsync(s => s.Login == request.Login 
                                      && s.Password == request.Password, 
                cancellationToken);
            
        if (teacher is null)
        {
            return Errors.Errors.General.NotFound();
        }
        
        return teacher;
    } 
    
    public Guid Delete(Teacher teacher, CancellationToken cancellationToken = default)
    {
        _dbContext.Teacher.Remove(teacher);
        _dbContext.SaveChanges();

        return teacher.Id;
    }
}