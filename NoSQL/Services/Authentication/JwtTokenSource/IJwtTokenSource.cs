namespace NoSQL.Services.Authentication.JwtTokenSource
{
    public interface IJwtTokenSource
    {
        string Get(string id);
    }
}
