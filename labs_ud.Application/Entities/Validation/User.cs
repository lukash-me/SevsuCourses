using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;

namespace labs_ud.Application.Entities.Validation;

public class User
{
    public static Result<string, Error> CheckMainInfo(
        string fio,
        string email,
        string phone,
        string photo)
    {
        
        if (string.IsNullOrWhiteSpace(fio))
        {
            return Errors.Errors.General.ValueIsRequired("fio");
        }

        if (fio.Length > Constants.Values.FIO_LENGTH)
        {
            return Errors.Errors.General.InvalidLength("fio");
        }
        
        if (string.IsNullOrWhiteSpace(phone))
        {
            return Errors.Errors.General.ValueIsRequired("phone");
        }

        if (Regex.IsMatch(phone, Constants.Regexes.RU_PHONE_REGEX) == false)
        {
            return Errors.Errors.General.ValueIsInvalid("phone");
        }
        
        if (string.IsNullOrWhiteSpace(email))
        {
            return Errors.Errors.General.ValueIsRequired("email");
        }
        
        if (Regex.IsMatch(phone, Constants.Regexes.EMAIL_REGEX) == false)
        {
            return Errors.Errors.General.ValueIsInvalid("phone");
        }

        if (string.IsNullOrWhiteSpace(photo))
        {
            return Errors.Errors.General.ValueIsRequired("photo");
        }

        return "Ok";
    }
    
    public static Result<string, Error> CheckAdditionalInfo(
        string education,
        string description)
    {
        
        if (string.IsNullOrWhiteSpace(education))
        {
            return Errors.Errors.General.ValueIsRequired("education");
        }

        if (education.Length > Constants.Values.MEDIUM_TEXT)
        {
            return Errors.Errors.General.InvalidLength("education");
        }
        
        if (string.IsNullOrWhiteSpace(description))
        {
            return Errors.Errors.General.ValueIsRequired("description");
        }
        
        if (description.Length > Constants.Values.DESCRIPTION_LENGTH)
        {
            return Errors.Errors.General.InvalidLength("description");
        }

        return "Ok";
    }

    public static Result<string, Error> CheckPassword(
        string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return Errors.Errors.General.ValueIsRequired("password");
        }
        
        if (password.Length > Constants.Values.SMALL_TEXT)
        {
            return Errors.Errors.General.InvalidLength("password");
        }
        
        return "Ok";
    }
}