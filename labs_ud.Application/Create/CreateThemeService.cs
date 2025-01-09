using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create;

public class CreateThemeService
{
    private readonly ThemeRepository _themeRepository;

    public CreateThemeService(ThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateThemeRequest request, CancellationToken cancellationToken)
    {
        var courseId = request.CourseId;

        var title = request.Title;
        
        var description  = request.Description;

        var text = request.Text;

        var photo = request.Photo;

        var number = request.Number;

        var themeResult = Theme.Create(
            courseId,
            title,
            description,
            text,
            photo,
            number);

        if (themeResult.IsFailure)
            return themeResult.Error;
        
        await _themeRepository.Add(themeResult.Value, cancellationToken);

        return themeResult.Value.Id;
    }
}