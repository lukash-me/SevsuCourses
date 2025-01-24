using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;

namespace labs_ud.Application.Repositories;

public class CourseRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public CourseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(Course course, CancellationToken cancellationToken = default)
    {
        await _dbContext.Course.AddAsync(course, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return course.Id;
    }
    
    public async Task<Result<Course, Error>> GetById(
        CourseID courseId, 
        CancellationToken cancellationToken = default)
    {
        var course = await _dbContext.Course
            .FirstOrDefaultAsync(v => v.Id == courseId, cancellationToken);
        
        if (course is null)
        {
            return Errors.Errors.General.NotFound(courseId.Value);
        }
        
        return course;
    } 
    
    public async Task<Result<Course[], Error>> GetAll(
        CancellationToken cancellationToken = default)
    {
        var courses = await _dbContext.Course.ToArrayAsync(cancellationToken);
        
        return courses;
    }
    
    public async Task<Result<List<Course>, Error>> GetByTeacherId(
        Guid teacherId, 
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Course
            .Where(t => t.TeacherId == teacherId)
            .ToListAsync(cancellationToken);
        
        return result;
    } 
    
    public Guid Delete(Course course, CancellationToken cancellationToken = default)
    {
        _dbContext.Course.Remove(course);
        _dbContext.SaveChanges();

        return course.Id;
    }
}