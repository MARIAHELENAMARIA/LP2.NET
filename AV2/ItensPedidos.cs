namespace AV2.Models;

class ItensPedidos{
    public int CodItemPedido;
    public int CodPedido;
    public int CodProduto;
    public int Quantidade;

    public ItensPedidos(int codItemPedido, int codPedido, int codProduto, int quantidade){
        CodItemPedido = codItemPedido;
        CodPedido = codPedido;
        CodProduto = codProduto;
        Quantidade = quantidade;
    }
}

