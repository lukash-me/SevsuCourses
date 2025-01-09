using CSharpFunctionalExtensions;

using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete;

public class DeleteThemeService
{
    private readonly ThemeRepository _themeRepository;

    public DeleteThemeService(ThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        DeleteThemeRequest request, 
        CancellationToken cancellationToken = default)
    {
        var themeResult = await _themeRepository.GetById(request.ThemeId, cancellationToken);
        if (themeResult.IsFailure)
            return themeResult.Error;

        _themeRepository.Delete(themeResult.Value, cancellationToken);

        return themeResult.Value.Id;
    }
}