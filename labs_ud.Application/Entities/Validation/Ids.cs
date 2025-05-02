using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;

namespace labs_ud.Application.Entities.Validation;

public class Ids
{
    public static Result<string, Error> CheckGuid(
        string id,
        string name)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Errors.Errors.General.ValueIsRequired(name);
        }
        
        if (Regex.IsMatch(id, Constants.Regexes.GUID_REGEX) == false)
            return Errors.Errors.General.ValueIsInvalid(name);

        return "Ok";
    }
}