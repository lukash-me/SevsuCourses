using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Group;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;

namespace labs_ud.Application.Repositories;

public class GroupRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public GroupRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(Group group, CancellationToken cancellationToken = default)
    {
        await _dbContext.Group.AddAsync(group, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return group.Id;
    }
    
    public async Task<Result<Group, Error>> GetById(
        GroupId groupId, 
        CancellationToken cancellationToken = default)
    {
        var group = await _dbContext.Group
            .FirstOrDefaultAsync(v => v.Id == groupId, cancellationToken);
        
        if (group is null)
        {
            return Errors.Errors.General.NotFound(groupId.Value);
        }
        
        return group;
    }
    
    public async Task<Result<List<Group>, Error>> GetByMentorIdAndCourseId(
        GroupsRequest request, 
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Group
            .Where(g => g.MentorId == request.MentorId && g.CourseId == request.CourseId)
            .ToListAsync(cancellationToken);
        
        if (result.Count == 0)
        {
            return Errors.Errors.General.NotFound(request.MentorId.Value);
        }
        
        return result;
    }
    
    public async Task<Result<List<Group>, Error>> GetByCourseId(
        Guid CourseId, 
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Group
            .Where(g => g.CourseId == CourseId)
            .ToListAsync(cancellationToken);
        
        if (result.Count == 0)
        {
            return Errors.Errors.General.NotFound(CourseId);
        }
        
        return result;
    }
    
    
    public Guid Delete(Group group, CancellationToken cancellationToken = default)
    {
        _dbContext.Group.Remove(group);
        _dbContext.SaveChanges();

        return group.Id;
    }
}