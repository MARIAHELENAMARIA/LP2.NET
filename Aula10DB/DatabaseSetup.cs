using Microsoft.Data.Sqlite;

namespace Aula10DB.Database;

class DatabaseSetup
{
    private readonly DatabaseConfig _databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        CreateClienteTable();
        CreatePedidoTable();
    }

    private void CreateClienteTable()
    {
        
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Clientes(
                clienteid       int not null primary key,
                endereco        varchar(80) not null,
                cidade          varchar(30) not null,
                regiao          varchar(10) not null,
                codigopostal    varchar(10) not null,
                pais            varchar(10) not null,
                telefone        varchar(15) not null);
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }
    
    private void CreatePedidoTable()
    {
        
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Pedidos(
                pedidoid            int not null primary key,
                empregadoid         int not null,
                datapedido          varchar(10) not null,
                peso                varchar(10) not null,
                codtransportadora   int not null,
                pedidoclienteid     int not null);
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }
    
}