using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
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
    
    public Guid Delete(Group group, CancellationToken cancellationToken = default)
    {
        _dbContext.Group.Remove(group);
        _dbContext.SaveChanges();

        return group.Id;
    }
}