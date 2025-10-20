namespace Domain.Common;

public interface IUserContext
{
    string? LangId { get; set; }
}

public class UserContext : IUserContext
{
    public string? LangId { get; set; }
}

