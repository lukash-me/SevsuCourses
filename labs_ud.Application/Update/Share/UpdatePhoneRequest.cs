namespace labs_ud.Application.Update.Share;

public record UpdatePhoneRequest(
    Guid Id,
    PhoneDto Dto
);