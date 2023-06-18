using AV2.Database;
using AV2.Models;
using Microsoft.Data.Sqlite;

namespace AV2.Repositories;

class ProdutoRepository{
    private readonly DatabaseConfig _databaseConfig;
    public ProdutoRepository(DatabaseConfig databaseConfig){
        _databaseConfig = databaseConfig;
    }

    public List<Produto> GetAll(){
        var produtos = new List<Produto>();
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Produto";
        var reader = command.ExecuteReader();

        while(reader.Read()){
            var CodProduto = reader.GetInt32(0);
            var Descricao = reader.GetString(1);
            var ValorUnitario = reader.GetInt32(2);                                
            var produto = ReaderToProduto(reader);
            produtos.Add(produto);
        }
        connection.Close();
        return produtos;
    }

    public Produto Save(Produto produto){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = "INSERT INTO Produto VALUES($CodProduto, $Descricao, $ValorUnitario)";
        command.Parameters.AddWithValue("$CodProduto", produto.CodProduto);
        command.Parameters.AddWithValue("$Descricao", produto.Descricao);
        command.Parameters.AddWithValue("$ValorUnitario", produto.ValorUnitario);   

        command.ExecuteNonQuery();
        connection.Close();
        return produto;
    }

    public Produto GetByIdProduto(Produto CodProduto){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Produto WHERE (CodProduto = $CodProduto)";
        command.Parameters.AddWithValue("$CodProduto", CodProduto);

        var reader = command.ExecuteReader();
        reader.Read();

        var produto = ReaderToProduto(reader);

        connection.Close(); 
        return produto;
    }

    public bool ExistByIdProduto(Produto CodProduto){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(CodProduto) FROM Produto WHERE (CodProduto = $CodProduto)";
        command.Parameters.AddWithValue("$CodProduto", CodProduto);

        var reader = command.ExecuteReader();
        reader.Read();
        var resultProduto = reader.GetBoolean(0);
        return resultProduto;
    }
    private Produto ReaderToProduto(SqliteDataReader reader){
        var produto = new Produto(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
        return produto;
    }
}