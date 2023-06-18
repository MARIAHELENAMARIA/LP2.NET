using AV2.Database;
using AV2.Models;

using Microsoft.Data.Sqlite;

namespace AV2.Repositories;
class ItensPedidosRepository{
    private readonly DatabaseConfig _databaseConfig;
    public ItensPedidosRepository(DatabaseConfig databaseConfig){
        _databaseConfig = databaseConfig;
    }

    public List<ItensPedidos> GetAll()
    {
        var itensPedidos = new List<ItensPedidos>();
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItemPedido";
        var reader = command.ExecuteReader();

        while(reader.Read()){
            var CodItemPedido = reader.GetInt32(0);
            var CodPedido = reader.GetInt32(1);
            var CodProduto = reader.GetInt32(2);
            var Quantidade = reader.GetInt32(3);  
            var item = ReaderToItens(reader);
            itensPedidos.Add(item);
        }
        connection.Close();
        return itensPedidos;
    }
    public ItensPedidos Save(ItensPedidos item){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO ItemPedido VALUES($CodItemPedido, $CodPedido, $CodProduto, $Quantidade)";
        
        command.Parameters.AddWithValue("$CodItemPedido", item.CodItemPedido);
        command.Parameters.AddWithValue("$CodPedido", item.CodPedido);
        command.Parameters.AddWithValue("$CodProduto", item.CodProduto);
        command.Parameters.AddWithValue("$Quantidade", item.Quantidade);      

        command.ExecuteNonQuery();
        connection.Close();

        return item;
    }
    public ItensPedidos GetByIdItensPedidos(ItensPedidos codItemPedido){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItemPedido WHERE (codItemPedido = $CodItemPedido)";
        command.Parameters.AddWithValue("$codItemPedido", codItemPedido);

        var reader = command.ExecuteReader();
        reader.Read();

        var item = ReaderToItens(reader);

        connection.Close(); 

        return item;
    }
    
    public bool ExistByIdItensPedidos(ItensPedidos codItemPedido){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(CodItemPedido) FROM ItemPedido WHERE (codItemPedido = $CodCliente)";
        command.Parameters.AddWithValue("$CodItemPedido", codItemPedido);

        var reader = command.ExecuteReader();
        reader.Read();
        var resultItens = reader.GetBoolean(0);

        return resultItens;
    }
    
    private ItensPedidos ReaderToItens(SqliteDataReader reader){
        var itens = new ItensPedidos(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
        return itens;
    }
}