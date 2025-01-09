using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using Microsoft.EntityFrameworkCore;

namespace labs_ud.Application.Repositories;

public class ThemeRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public ThemeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(Theme theme, CancellationToken cancellationToken = default)
    {
        await _dbContext.Theme.AddAsync(theme, cancellationToken);
        await _dbContext.SaveChangesAsync();

        return theme.Id;
    }
    
    public async Task<Result<Theme, Error>> GetById(
        ThemeId themeId, 
        CancellationToken cancellationToken = default)
    {
        var theme = await _dbContext.Theme
            .FirstOrDefaultAsync(v => v.Id == themeId, cancellationToken);
        
        if (theme is null)
        {
            return Errors.Errors.General.NotFound(themeId.Value);
        }
        
        return theme;
    } 
    
    public async Task<Result<List<Theme>, Error>> GetByCourseId(
        Guid courseId, 
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Theme
            .Where(t => t.CourseId == courseId)
            .ToListAsync(cancellationToken);
        
        return result;
    } 
    
    public Guid Delete(Theme theme, CancellationToken cancellationToken = default)
    {
        _dbContext.Theme.Remove(theme);
        _dbContext.SaveChanges();

        return theme.Id;
    }
}