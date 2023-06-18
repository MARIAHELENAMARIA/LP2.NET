using AV2.Database;
using AV2.Models;

using Microsoft.Data.Sqlite;

namespace AV2.Repositories;
class ClienteRepository{
    private readonly DatabaseConfig _databaseConfig;
    public ClienteRepository(DatabaseConfig databaseConfig){
        _databaseConfig = databaseConfig;
    }

    public List<Cliente> GetAll()
    {
        var clientes = new List<Cliente>();
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Cliente";
        var reader = command.ExecuteReader();

        while(reader.Read()){
            var CodCliente = reader.GetInt32(0);
            var Nome = reader.GetString(1);
            var Endereco = reader.GetString(2);
            var Cidade = reader.GetString(3);            
            var Cep = reader.GetString(4);            
            var Uf = reader.GetString(5);
            var Ie = reader.GetString(6);    
            var cliente = ReaderToCliente(reader);
            clientes.Add(cliente);
        }

        connection.Close();
        return clientes;
    }
    public Cliente Save(Cliente cliente){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Clientes VALUES($CodCliente, $Nome, $Endereco, $Cidade, $Cep, $Uf, $Ie)";
        command.Parameters.AddWithValue("$CodCliente", cliente.CodCliente);
        command.Parameters.AddWithValue("$Nome", cliente.Nome);
        command.Parameters.AddWithValue("$Endereco", cliente.Endereco);
        command.Parameters.AddWithValue("$Cidade", cliente.Cidade);
        command.Parameters.AddWithValue("$Cep", cliente.Cep);                
        command.Parameters.AddWithValue("$Uf", cliente.Uf);
        command.Parameters.AddWithValue("$Ie", cliente.Ie);        

        command.ExecuteNonQuery();
        connection.Close();

        return cliente;
    }
    public Cliente GetByIdCliente(int clienteid){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Cliente WHERE (CodCliente = $CodCliente)";
        command.Parameters.AddWithValue("$CodCliente", clienteid);

        var reader = command.ExecuteReader();
        reader.Read();

        var cliente = ReaderToCliente(reader);

        connection.Close(); 

        return cliente;
    }
    
    public bool ExistByIdCliente(int CodCliente)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(CodCliente) FROM Clientes WHERE (CodCliente = $CodCliente)";
        command.Parameters.AddWithValue("$CodCliente", CodCliente);

        var reader = command.ExecuteReader();
        reader.Read();
        var resultCliente = reader.GetBoolean(0);

        return resultCliente;
    }
    
    private Cliente ReaderToCliente(SqliteDataReader reader){
        var cliente = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
        return cliente;
    }
}