namespace Core.Shared.Errors;

public sealed class BadRequestError(Error error) : Error(error.Code, error.Message) { }
