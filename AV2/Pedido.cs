namespace AV2.Models;

class Pedido{

    public int CodPedido { get; set; }
    public DateTime PrazoEntrega { get; set; }
    public DateTime DataPedido { get; set; }  
    public int CodCliente { get; set; }    
    public int CodVendedor  { get; set; }    

    public Pedido(int codPedido, DateTime prazoEntrega, DateTime dataPedido, int codCliente, int codVendedor){
        CodPedido = codPedido;
        PrazoEntrega = prazoEntrega;
        DataPedido = dataPedido;
        CodCliente = codCliente;
        CodVendedor = codVendedor;
    }
}