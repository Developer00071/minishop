using Npgsql;

namespace Application.Data;

public class Database
{
    private string connection = "Host=localhost;Port=5432;Database=MiniShop;Username=postgres;Password=1234";
    public NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(connection);
    }
}
