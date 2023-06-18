namespace AV2.Models;

class Produto{
    public int CodProduto;
    public string Descricao;
    public int ValorUnitario;

    public Produto(int codProduto, string descricao, int valorUnitario){
        CodProduto = codProduto;
        Descricao = descricao;
        ValorUnitario = valorUnitario;
    }
}