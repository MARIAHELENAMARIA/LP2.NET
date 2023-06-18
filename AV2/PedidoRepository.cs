using AV2.Database;
using AV2.Models;

using Microsoft.Data.Sqlite;

namespace AV2.Repositories;

class PedidoRepository{
    private readonly DatabaseConfig _databaseConfig;
    public PedidoRepository(DatabaseConfig databaseConfig){
        _databaseConfig = databaseConfig;
    }

    public List<Pedido> GetAll(){
        var pedidos = new List<Pedido>();
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Pedido";

        var reader = command.ExecuteReader();

        while(reader.Read()){
            var CodPedido = reader.GetInt32(0);
            var PrazoEntrega = reader.GetString(1);
            var DataPedido = reader.GetString(2);
            var CodCliente = reader.GetInt32(3);
            var CodVendedor = reader.GetInt32(4);                                 
            var pedido = ReaderToPedido(reader);
            pedidos.Add(pedido);
        }

        connection.Close();
        return pedidos;
    }

    public Pedido Save(Pedido pedido){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        
        command.CommandText = "INSERT INTO Pedido VALUES($CodPedido, $PrazoEntrega, $DataPedido, $CodCliente, $CodVendedor)";
        command.Parameters.AddWithValue("$CodPedido", pedido.CodPedido);
        command.Parameters.AddWithValue("$PrazoEntrega", pedido.PrazoEntrega);
        command.Parameters.AddWithValue("$DataPedido", pedido.DataPedido);      
        command.Parameters.AddWithValue("$CodCliente", pedido.CodCliente);        
        command.Parameters.AddWithValue("$CodVendedor", pedido.CodVendedor);        

        command.ExecuteNonQuery();
        connection.Close();

        return pedido;
    }
    public Pedido GetByIdPedido(int CodPedido){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Pedido WHERE (CodPedido = $CodPedido)";
        command.Parameters.AddWithValue("$CodPedido", CodPedido);

        var reader = command.ExecuteReader();
        reader.Read();

        var pedido = ReaderToPedido(reader);

        connection.Close(); 
        return pedido;
    }
    
    public bool ExistByIdPedido(int CodPedido){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(CodPedido) FROM Pedidos WHERE (CodPedido = $CodPedido)";
        command.Parameters.AddWithValue("$CodPedido", CodPedido);

        var reader = command.ExecuteReader();
        reader.Read();
        var resultPedido = reader.GetBoolean(0);
        return resultPedido;
    }
    private Pedido ReaderToPedido(SqliteDataReader reader){
        var pedido = new Pedido(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2), reader.GetInt32(3), reader.GetInt32(4));

        return pedido;
    }
}