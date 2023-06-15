// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using Microsoft.Data.Sqlite;
using Aula10DB.Database;
using Aula10DB.Repositories;
using Aula10DB.Models;
var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var clienteRepository = new ClienteRepository(databaseConfig);
var pedidoRepository = new PedidoRepository(databaseConfig);

var modelNome = args[0];
var modelAcao = args[1];
if(modelNome == "Cliente")
{
    if(modelAcao == "Listar")
    {
        Console.WriteLine("Listar Cliente");
        foreach (var cliente in clienteRepository.GetAll())
        {
            Console.WriteLine($"{cliente.ClienteId}, {cliente.Endereco}, {cliente.Cidade}, {cliente.Regiao}, {cliente.CodigoPostal}, {cliente.Pais}, {cliente.Telefone}");
        }
    }

    if(modelAcao == "Inserir")
    {
        Console.WriteLine("Inserir Cliente");
        int     clienteid;
        string  endereco;
        string  cidade;
        string  regiao;
        string  codigopostal;
        string  pais;
        string  telefone;

        Console.Write("Digite o id do cliente            : ");
        clienteid = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o endereço do cliente      : ");
        endereco = Console.ReadLine();
        Console.Write("Digite a cidade do cliente        : ");
        cidade = Console.ReadLine();
        Console.Write("Digite a região do cliente        : ");
        regiao = Console.ReadLine();
        Console.Write("Digite o código postal do cliente : ");
        codigopostal = Console.ReadLine();
        Console.Write("Digite o país do cliente          : ");
        pais = Console.ReadLine();
        Console.Write("Digite o telefone do cliente      : ");
        telefone = Console.ReadLine();
        var cliente = new Cliente(clienteid, endereco, cidade, regiao, codigopostal, pais, telefone);
        clienteRepository.Save(cliente);
    }

    if(modelAcao == "Apresentar")
    {
        Console.WriteLine("Apresentar Cliente");
	    Console.Write("Digite o id do cliente : ");
        var clienteid = Convert.ToInt32(Console.ReadLine());
       
        if(clienteRepository.ExistByIdCliente(clienteid))
        {
            var cliente = clienteRepository.GetByIdCliente(clienteid);
            Console.WriteLine($"{cliente.ClienteId}, {cliente.Endereco}, {cliente.Cidade}, {cliente.Regiao}, {cliente.CodigoPostal}, {cliente.Pais}, {cliente.Telefone}");
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {clienteid} não existe.");
        }
    }
}

if(modelNome == "Pedido")
{
    if(modelAcao == "Listar")
    {
        Console.WriteLine("Listar Pedido");
        foreach (var pedido in pedidoRepository.GetAll())
        {
            Console.WriteLine($"{pedido.PedidoId}, {pedido.EmpregadoId}, {pedido.DataPedido}, {pedido.Peso}, {pedido.CodTransportadora}, {pedido.PedidoClienteId}");
        }
    }

    if(modelAcao == "Inserir")
    {
        Console.WriteLine("Inserir Pedido");
        
        int     pedidoid;
        int     empregadoid;
        string  datapedido;
        string  peso;
        int     codtransportadora;                
        int     pedidoclienteid;        

        Console.Write("Digite o id do pedido                       : ");
        pedidoid = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o id do empregado do pedido          : ");
        empregadoid = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a data do pedido                     : ");
        datapedido = Console.ReadLine();
        Console.Write("Digite o peso do pedido                     : ");
        peso = Console.ReadLine();
        Console.Write("Digite o código da transportadora do pedido : ");
        codtransportadora = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do cliente do pedido        : ");
        pedidoclienteid = Convert.ToInt32(Console.ReadLine());
                
        var pedido = new Pedido(pedidoid, empregadoid, datapedido, peso, codtransportadora, pedidoclienteid);
        pedidoRepository.Save(pedido);
    }

    if(modelAcao == "Apresentar")
    {
        Console.WriteLine("Apresentar Pedido");
	    Console.Write("Digite o id do pedido : ");
        var pedidoid = Convert.ToInt32(Console.ReadLine());

        if(pedidoRepository.ExistByIdPedido(pedidoid))
        {
            var pedido = pedidoRepository.GetByIdPedido(pedidoid);
            Console.WriteLine($"{pedido.PedidoId}, {pedido.EmpregadoId}, {pedido.DataPedido}, {pedido.Peso}, {pedido.CodTransportadora}, {pedido.PedidoClienteId}");
        } 
        else 
        {
            Console.WriteLine($"O pedido com Id {pedidoid} não existe.");
        }
    }
    if(modelAcao == "MostrarPedidosCliente")
    {
        Console.WriteLine("Mostrar Pedidos do Cliente");
        Console.Write("Digite o id do cliente : ");
        
        var pedidoclienteid = Convert.ToInt32(Console.ReadLine());
                
        if(clienteRepository.ExistByIdCliente(pedidoclienteid))
        {
            foreach (var pedido in pedidoRepository.GetAll())
            {
                if (pedidoclienteid == pedido.PedidoClienteId)
                {
                    Console.WriteLine($"{pedido.PedidoId}, {pedido.EmpregadoId}, {pedido.DataPedido}, {pedido.Peso}, {pedido.CodTransportadora}");
                } 
            }   
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {pedidoclienteid} não existe.");
        }
    } 
}