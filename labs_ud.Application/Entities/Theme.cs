using CSharpFunctionalExtensions;
using labs_ud.Application.Entities.Validation;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Theme
{
    public Theme(
        Guid courseId,
        string title,
        string? text,
        string? photo,
        int number)
    {
        Id = ThemeId.NewThemeId();
        CourseId = courseId;
        Title = title;
        Text = text;
        Photo = photo;
        Number = number;
    }

    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public string? Text { get; set; }
    public string? Photo { get; set; }
    public int Number { get; set; }

    public Result<string, Error> UpdateMainInfo(string title, string? text, string? photo)
    {
        Title = title;
        Text = text;
        Photo = photo;
        
        if (string.IsNullOrWhiteSpace(title))
        {
            return Errors.Errors.General.ValueIsRequired("title");
        }

        if (title.Length > Constants.Values.MEDIUM_TEXT)
        {
            return Errors.Errors.General.InvalidLength("title");
        }

        return "Success";
    }
    
    public static Result<Theme, Error> Create(
        Guid courseId,
        string title,
        string? text,
        string? photo,
        int number
    )
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Errors.Errors.General.ValueIsRequired("title");
        }

        if (title.Length > Constants.Values.MEDIUM_TEXT)
        {
            return Errors.Errors.General.InvalidLength("title");
        }

        if (int.IsNegative(number))
        {
            return Errors.Errors.General.ValueIsInvalid("number");
        }
        
        var theme = new Theme(
            courseId,
            title,
            text,
            photo,
            number
        );
        
        return theme;
    }
}