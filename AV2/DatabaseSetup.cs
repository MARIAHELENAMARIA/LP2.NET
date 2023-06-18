using Microsoft.Data.Sqlite;

namespace AV2.Database;

class DatabaseSetup
{
    private readonly DatabaseConfig _databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        CreateClienteTable();
        CreateItensPedidosTable();
        CreatePedidoTable();
        CreateProdutoTable();
        CreateVendedoresTable();
    }

    private void CreateClienteTable(){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Cliente(
                CodCliente      int(4) not null primary key,
                Nome            varchar(45),
                Endereco        varchar(45),
                Cidade          varchar(45),
                Cep             varcchar(11),
                Uf              char(2),
                Ie              varchar(12);
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }

    private void CreateItensPedidosTable(){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS ItemPedido(
                CodItemPedido   int(4) not null primary key,
                CodPedido       int(4) foreign key,
                CodProduto      int(4) foreign key,
                Quantidade      int(5);
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }

    private void CreatePedidoTable(){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Pedido(
                CodPedido       int(4) not null primary key,
                PrazoEntrega    date,
                DataPedido      date,
                CodCliente      int(4) foreign key,
                CodVendedor     int(4) foreign key;
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }
    
    private void CreateProdutoTable(){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Produto(
                CodProduto      int(4) not null primary key,
                Descricao       varchar(45),
                ValorUnitario   decimal(10,2);
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }
    
    private void CreateVendedoresTable(){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Vendedores(
                CodVendedor     int(4) not null primary key,
                Nome            varchar(45),
                SalarioFixo     decimal(10,2),
                FaixaComissao   enum(...);
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }
}