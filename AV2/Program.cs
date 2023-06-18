// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using Microsoft.Data.Sqlite;
using AV2.Database;
using AV2.Repositories;
using AV2.Models;


var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);

var clienteRepository = new ClienteRepository(databaseConfig);
var itensPedidosRepository = new ItensPedidosRepository(databaseConfig);
var pedidoRepository = new PedidoRepository(databaseConfig);
var produtoRepository = new ProdutoRepository(databaseConfig);
var vendedoresRepository = new VendedoresRepository(databaseConfig);

var modelNome = args[0];
var modelAcao = args[1];

if(modelNome == "Cliente"){
    if(modelAcao == "Listar"){
        Console.WriteLine("Listar Cliente");
        Console.WriteLine("Código Cliente   Nome Cliente      Endereço      Cidade      CEP   UF      IE");
        foreach (var cliente in clienteRepository.GetAll()){
            Console.WriteLine($"{cliente.CodCliente, -12} {cliente.Nome, -24} {cliente.Endereco, -11} {cliente.Cidade, -11} {cliente.Cep, -15} {cliente.Uf, -9} {cliente.Ie}");
        }
    }

    if(modelAcao == "Inserir"){
        Console.WriteLine("Inserir Cliente");
        int     codCliente;
        string  nome;
        string  endereco;
        string  cidade;
        string  cep;
        string  uf;
        string  ie;

        Console.Write("Digite o código do cliente            : ");
        codCliente = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o nome do cliente      : ");
        nome = Console.ReadLine();
        Console.Write("Digite o endereco do cliente        : ");
        endereco = Console.ReadLine();
        Console.Write("Digite a cidade do cliente        : ");
        cidade = Console.ReadLine();
        Console.Write("Digite o CEP do cliente : ");
        cep = Console.ReadLine();
        Console.Write("Digite a UF do cliente          : ");
        uf = Console.ReadLine();
        Console.Write("Digite a IE do cliente      : ");
        ie = Console.ReadLine();
        
        var cliente = new Cliente(codCliente, nome, endereco, cidade, cep, uf, ie);
        clienteRepository.Save(cliente);
    }

    if(modelAcao == "Apresentar"){
        Console.WriteLine("Apresentar Cliente");
	    Console.Write("Digite o código do cliente : ");
        var codCliente = Convert.ToInt32(Console.ReadLine());
       
        if(clienteRepository.ExistByIdCliente(codCliente)){
            var cliente = clienteRepository.GetByIdCliente(codCliente);
            Console.WriteLine($"{cliente.CodCliente}, {cliente.Nome}, {cliente.Endereco}, {cliente.Cidade}, {cliente.Cep}, {cliente.Uf}, {cliente.Ie}");
        } 
        else {
            Console.WriteLine($"O cliente com Id {codCliente} não existe.");
        }
    }
}

if(modelNome == "Itens Pedidos"){
    if(modelAcao == "Listar"){
        Console.WriteLine("Listar Pedidos");
        Console.WriteLine("Código do Item   Código do Pedido   Código do Produto    Quantidade");
        foreach (var item in itensPedidosRepository.GetAll()){
            Console.WriteLine($"{item.CodItemPedido, -12} {item.CodPedido, -24} {item.CodProduto, -11} {item.Quantidade, -11}");
        }
    }
    if(modelAcao == "Inserir"){
         Console.WriteLine("Inserir ItensPedidos");
        int codItemPedido;
        int codPedido;
        int codProduto;
        int quantidade;

        Console.Write("Digite o código do item pedido            : ");
        codItemPedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do pedido                 : ");
        codPedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do produto                : ");
        codProduto = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a quantidade do produto            : ");
        quantidade = Convert.ToInt32(Console.ReadLine());
        
        var item = new ItensPedidos(codItemPedido, codPedido, codProduto, quantidade);
        itensPedidosRepository.Save(item);
    }
    if(modelAcao == "Apresentar"){
        Console.WriteLine("Apresentar Itens Pedidos");
	    Console.Write("Digite o código do item : ");
        var codItem = Convert.ToInt32(Console.ReadLine());
       
        if(itensPedidosRepository.ExistByIdItensPedidos(codItem)){
            var item = itensPedidosRepository.GetByIdItensPedidos(codItem);
            Console.WriteLine($"{item.CodItemPedido}, {item.CodPedido}, {item.CodProduto}, {item.Quantidade}");
        } 
        else {
            Console.WriteLine($"O cliente com Id {codItem} não existe.");
        }
    }
}


if(modelNome == "Pedido"){
    if(modelAcao == "Listar")
    {
        Console.WriteLine("Listar Pedido");
        Console.WriteLine("Código do Pedido   Prazo de Entrega   Data do Pedido   Código do Cliente   Código do Vendedor");
        foreach (var pedido in pedidoRepository.GetAll()){
            Console.WriteLine($"{pedido.CodPedido, -12} {pedido.PrazoEntrega, -14} {pedido.DataPedido, -16} {pedido.CodCliente, -9} {pedido.CodVendedor, -26}");
        }
    }

    if(modelAcao == "Inserir")
    {
        Console.WriteLine("Inserir Pedido");
        
        int        codPedido;
        DateTime   prazoEntrega;
        DateTime   dataPedido;
        int        codCliente;                
        int        codVendedor;        

        Console.Write("Digite o código do pedido                   : ");
        codPedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o prazo de entrega                   : ");
        prazoEntrega = Convert.ToDateTime(Console.ReadLine());
        Console.Write("Digite a data do pedido                     : ");
        dataPedido = Convert.ToDateTime(Console.ReadLine());
        Console.Write("Digite o código do cliente                  : ");
        codCliente = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do vendedor                 : ");
        codVendedor = Convert.ToInt32(Console.ReadLine());
                
        var pedido = new Pedido(codPedido, prazoEntrega, dataPedido, codCliente, codVendedor);
        pedidoRepository.Save(pedido);
    }

    if(modelAcao == "Apresentar")
    {
        Console.WriteLine("Apresentar Pedido");
	    Console.Write("Digite o código do pedido : ");
        var pedidoid = Convert.ToInt32(Console.ReadLine());

        if(pedidoRepository.ExistByIdPedido(pedidoid)){
            var pedido = pedidoRepository.GetByIdPedido(pedidoid);
            Console.WriteLine($"{pedido.CodPedido}, {pedido.PrazoEntrega}, {pedido.DataPedido}, {pedido.CodCliente}, {pedido.CodVendedor}");
        } 
        else {
            Console.WriteLine($"O pedido com código {pedidoid} não existe.");
        }
    }
}
    

if(modelNome == "Produto"){
    if(modelAcao == "Listar"){
        Console.WriteLine("Listar Produto");
        Console.WriteLine("Código do produto   Descrição   Valor unitário");
        foreach (var produto in produtoRepository.GetAll()){
            Console.WriteLine($"{produto.CodProduto, -12} {produto.Descricao, -14} {produto.ValorUnitario, -16}");
        }
    }
    if(modelAcao == "Inserir"){
        Console.WriteLine("Inserir Produto");
        
        int        codProduto;
        string     descricao;
        int        valorUnitario;      

        Console.Write("Digite o código do produto                   : ");
        codProduto = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a descricao do produto                : ");
        descricao = Convert.ToString(Console.ReadLine());
        Console.Write("Digite o valor do produto                    : ");
        valorUnitario = Convert.ToInt32(Console.ReadLine());
                
        var produto = new Produto(codProduto, descricao, valorUnitario);
        produtoRepository.Save(produto);
    }
    if(modelAcao == "Apresentar"){
        Console.WriteLine("Apresentar Produtos");
	    Console.Write("Digite o código do produto : ");
        var codProduto = Convert.ToInt32(Console.ReadLine());

        if(produtoRepository.ExistByIdProduto(codProduto)){
            var produto = produtoRepository.GetByIdProduto(codProduto);
            Console.WriteLine($"{produto.CodProduto}, {produto.Descricao}, {produto.ValorUnitario}");
        } 
        else {
            Console.WriteLine($"O pedido com código {codProduto} não existe.");
        }
    }
}

if(modelNome == "Vendedores"){
    if(modelAcao == "Listar"){
        Console.WriteLine("Listar Vendedores");
        Console.WriteLine("Código do vendedor    Nome    Salário fixo    Faixa comissao");
        foreach (var vendedor in vendedoresRepository.GetAll()){
            Console.WriteLine($"{vendedor.CodVendedor, -12} {vendedor.Nome, -14} {vendedor.SalarioFixo, -16} {vendedor.FaixaComissao}");
        }
    }
    if(modelAcao == "Inserir"){
        Console.WriteLine("Inserir Produto");
        
        int codVendedor;
        string nome;
        double salarioFixo;
        double faixaComissao;    

        Console.Write("Digite o código do vendedor                   : ");
        codVendedor = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o nome do vendedor                     : ");
        nome = Convert.ToString(Console.ReadLine());
        Console.Write("Digite o salario do vendedor                  : ");
        salarioFixo = Convert.ToDouble(Console.ReadLine());
        Console.Write("Digite a comissao do vendedor                  : ");
        faixaComissao = Convert.ToDouble(Console.ReadLine());
                
        var vendedor = new Vendedores(codVendedor, nome, salarioFixo, faixaComissao);
        vendedoresRepository.Save(vendedor);
    }
    if(modelAcao == "Apresentar"){
        Console.WriteLine("Apresentar Vendedores");
	    Console.Write("Digite o código do vendedor : ");
        var codVendedor = Convert.ToInt32(Console.ReadLine());

        if(vendedoresRepository.ExistByIdVendedores(codVendedor)){
            var vendedor = vendedoresRepository.GetByIdVendedores(codVendedor);
            Console.WriteLine($"{vendedor.CodVendedor}, {vendedor.Nome}, {vendedor.SalarioFixo} {vendedor.FaixaComissao}");
        } 
        else {
            Console.WriteLine($"O pedido com código {codVendedor} não existe.");
        }
    }
}
