using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Entities.Validation;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Mentor
{
    public Mentor(
        string fio,
        string phone,
        string email,
        string education,
        string photo,
        string description,
        string password)
    {
        Id = MentorId.NewMentorId();
        Fio = fio;
        Phone = phone;
        Email = email;
        Education = education;
        Photo = photo;
        Description = description;
        Password = password;
    }

    public Guid Id { get; set; }
    public string Fio { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Education { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public string Password { get; set; }

    public Result<string, Error> UpdateMainInfo(
        string fio,
        string email,
        string education,
        string photo,
        string description)
    {
        var validationMainInfoResult = User.CheckMainInfo(fio, email, Phone, photo);
        if (validationMainInfoResult.IsFailure)
        {
            return validationMainInfoResult.Error;
        }
        
        var validationAdditionalResult = User.CheckAdditionalInfo(education, description);
        if (validationAdditionalResult.IsFailure)
        {
            return validationAdditionalResult.Error;
        }
        
        Fio = fio;
        Email = email;
        Education = education;
        Photo = photo;
        Description = description;
        
        return "Success";
    }
    
    public Result<string, Error> UpdatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return Errors.Errors.General.ValueIsRequired("phone");
        }

        if (Regex.IsMatch(phone, Constants.Regexes.RU_PHONE_REGEX) == false)
        {
            return Errors.Errors.General.ValueIsInvalid("phone");
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
    
    public static Result<Mentor, Error> Create(
        string fio,
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
        
        var validationAdditionalResult = User.CheckAdditionalInfo(education, description);
        if (validationAdditionalResult.IsFailure)
        {
            return validationAdditionalResult.Error;
        }
        
        var validationPasswordResult = User.CheckPassword(password);
        if (validationPasswordResult.IsFailure)
        {
            return validationPasswordResult.Error;
        }
        
        var mentor = new Mentor(
            fio,
            phone,
            email,
            education,
            photo,
            description,
            password
        );
        
        return mentor;
    }
}