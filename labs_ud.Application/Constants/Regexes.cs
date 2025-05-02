namespace labs_ud.Application.Constants;

public class Regexes
{
    public const string LINK_REGEX =
        @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/=]*)";

    public const string RU_PHONE_REGEX = @"(7|8|\+7)(\d{3}){2}(\d{2}){2}";
    
    public const string GUID_REGEX = "(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$";

    public const string EMAIL_REGEX = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
}