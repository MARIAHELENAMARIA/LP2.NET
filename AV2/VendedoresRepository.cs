using AV2.Database;
using AV2.Models;
using Microsoft.Data.Sqlite;

namespace AV2.Repositories;

class VendedoresRepository{
    private readonly DatabaseConfig _databaseConfig;
    public VendedoresRepository(DatabaseConfig databaseConfig){
        _databaseConfig = databaseConfig;
    }

    public List<Vendedores> GetAll(){
        var vendedores = new List<Vendedores>();
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * Vendedores";
        
        var reader = command.ExecuteReader();

        while(reader.Read()){
            var CodVendedor = reader.GetInt32(0);
            var Nome = reader.GetString(1);
            var SalarioFixo = reader.GetDouble(2);     
            var FaixaComissao = reader.GetDouble(2);                            
            var vendedor = ReaderToVendedores(reader);
            
            vendedores.Add(vendedor);
        }
        connection.Close();
        return vendedores;
    }

    public Vendedores Save(Vendedores vendedor){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = "INSERT INTO Vendedores VALUES($CodVendedor, $Nome, $SalarioFixo, $FaixaComissao)";
        command.Parameters.AddWithValue("$CodVendedor", vendedor.CodVendedor);
        command.Parameters.AddWithValue("$Nome", vendedor.Nome);
        command.Parameters.AddWithValue("$SalarioFixo", vendedor.SalarioFixo);
        command.Parameters.AddWithValue("$FaixaComissao", vendedor.FaixaComissao); 

        command.ExecuteNonQuery();
        connection.Close();
        return vendedor;
    }

    public Vendedores GetByIdVendedores(Vendedores CodVendedor){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vendedores WHERE (CodVendedor = $CodVendedor)";
        command.Parameters.AddWithValue("$CodVendedor", CodVendedor);

        var reader = command.ExecuteReader();
        reader.Read();

        var vendedores = ReaderToVendedores(reader);

        connection.Close(); 
        return vendedores;
    }
    
    public bool ExistByIdVendedores(Vendedores CodVendedor){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(CodVendedor) FROM Vendedores WHERE (CodVendedor = $CodVendedor)";
        command.Parameters.AddWithValue("$CodVendedor", CodVendedor);

        var reader = command.ExecuteReader();
        reader.Read();

        var resultVendedor = reader.GetBoolean(0);
        return resultVendedor;
    }

    private Vendedores ReaderToVendedores(SqliteDataReader reader){
        var vendedores = new Vendedores(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetDouble(3));
        return vendedores;
    }
}