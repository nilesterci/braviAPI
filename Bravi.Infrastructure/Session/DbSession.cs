using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Bravi.Infrastructure.Session
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IConfiguration configuriration)
        {
           var connectionString = configuriration.GetConnectionString("postgres");
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
