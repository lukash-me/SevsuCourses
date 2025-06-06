using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Entities.Validation;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Course
{
    public Course(
        Guid teacherId,
        string title,
        string? description,
        string? photo,
        DateTime dateCreated
        )
    {
        Id = CourseID.NewCourseId();
        TeacherId = teacherId;
        Title = title;
        Description = description;
        Photo = photo;
        DateCreated = dateCreated;
    }
    public Guid Id { get; set ; }
    public Guid TeacherId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Photo { get; set; }
    public DateTime DateCreated { get; set; }

    public Result<string, Error> UpdateMainInfo(string title, string description, string? photo)
    {
        var checkMainInfoResult = Block.CheckMainInfo(title, description);
        if (checkMainInfoResult.IsFailure)
        {
            return checkMainInfoResult.Error;
        }
        
        if (photo?.Length > Constants.Values.PHOTO_LENGTH)
        {
            return Errors.Errors.General.InvalidLength("photo");
        }
        
        Title = title;
        Description = description;
        Photo = photo;

        return "Success";
    }
    
    public static Result<Course, Error> Create(
        string teacherId,
        string title,
        string description,
        string? photo,
        DateTime dateCreated
        )
    {
        var checkGuidResult = Ids.CheckGuid(teacherId, "student id");
        if (checkGuidResult.IsFailure)
        {
            return checkGuidResult.Error;
        }
        
        var checkMainInfoResult = Block.CheckMainInfo(title, description);
        if (checkMainInfoResult.IsFailure)
        {
            return checkMainInfoResult.Error;
        }

        if (photo?.Length > Constants.Values.PHOTO_LENGTH)
        {
            return Errors.Errors.General.InvalidLength("photo");
        }
        
        var course = new Course(
            Guid.Parse(teacherId),
            title,
            description,
            photo,
            dateCreated
            );
        
        return course;
    }
}