using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;

namespace labs_ud.Application.Entities.Validation;

public class Block
{
    public static Result<string, Error> CheckMainInfo(
        string title,
        string description)
    {
        
        if (string.IsNullOrWhiteSpace(title))
        {
            return Errors.Errors.General.ValueIsRequired("title");
        }
        
        if (title.Length > Constants.Values.TITLE_LENGTH)
        {
            return Errors.Errors.General.InvalidLength("title");
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
}