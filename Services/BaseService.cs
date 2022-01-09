using System.Data;
using Npgsql;

public abstract class BaseService
{
    private readonly IConfiguration _config;

    public BaseService(IConfiguration config)
    {
        _config = config;
    }

    protected IDbConnection CreateConnection()
    {
        return new NpgsqlConnection (_config.GetConnectionString("PostgreSqlConnection"));
    }
}