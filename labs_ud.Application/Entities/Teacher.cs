using CSharpFunctionalExtensions;
using labs_ud.Application.Entities.Validation;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Teacher
{
    public Teacher(
        string fio,
        int experience,
        string phone,
        string email,
        string education,
        string photo,
        string description,
        string password)
    {
        Id = TeacherId.NewTeacherId();
        Fio = fio;
        Experience = experience;
        Phone = phone;
        Email = email;
        Education = education;
        Photo = photo;
        Description = description;
        Password = password;
    }
    public Guid Id { get; set; }
    public string Fio { get; set; }
    public int Experience { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Education { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public string Password { get; set; }

    public void UpdateMainInfo(
        string fio,
        int experience,
        string email,
        string education,
        string description,
        string photo)
    {
        Fio = fio;
        Experience = experience;
        Email = email;
        Education = education;
        Description = description;
        Photo = photo;
    }
    
    public Result<string, Error> UpdatePhone(string phone)
    {
        var validationMainInfoResult = User.CheckMainInfo(Fio, Email, phone, Photo);
        if (validationMainInfoResult.IsFailure)
        {
            return validationMainInfoResult.Error;
        }
        
        Phone = phone;

        return "Success";
    }
    
    public Result<string, Error> UpdatePassword(string password)
    {
        var validationPasswordResult = User.CheckPassword(password);
        if (validationPasswordResult.IsFailure)
        {
            return validationPasswordResult.Error;
        }
        
        Password = password;

        return "Success";
    }
    
    public static Result<Teacher, Error> Create(
        string fio,
        int experience,
        string phone,
        string email,
        string education,
        string photo,
        string description,
        string password
    )
    {
        var validationMainInfoResult = User.CheckMainInfo(fio, email, phone, photo);
        if (validationMainInfoResult.IsFailure)
        {
            return validationMainInfoResult.Error;
        }

        var validationAdditionalInfoResult = User.CheckAdditionalInfo(education, description);
        if (validationAdditionalInfoResult.IsFailure)
        {
            return validationAdditionalInfoResult.Error;
        }

        var validationPasswordResult = User.CheckPassword(password);
        if (validationPasswordResult.IsFailure)
        {
            return validationPasswordResult.Error;
        }

        if (int.IsNegative(experience))
        {
            return Errors.Errors.General.ValueIsInvalid("experience");
        }

        var teacher = new Teacher(
            fio,
            experience,
            phone,
            email,
            education,
            photo,
            description,
            password
        );
        
        return teacher;
    }
}