
using Microsoft.Data.Sqlite;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    internal class Connection
    {
        private string connectionString = $"Data Source=base.db";

        public SqliteConnection ObterConexao()
        {
            return new SqliteConnection(connectionString);
        }
        
    }
}
