using Npgsql;

namespace Infrastructure.Database;

public static class Database
{
    public static string ConnectionString = "host=localhost;port=5432;database=minishop_db;username=postgres;password=0510";
    public static NpgsqlConnection connection() => new NpgsqlConnection(ConnectionString);
}
