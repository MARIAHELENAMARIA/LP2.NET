namespace AV2.Models;

class Vendedores{
    public int CodVendedor;
    public string Nome;
    public double SalarioFixo;
    public double FaixaComissao;

    public Vendedores(int codVendedor, string nome, double salarioFixo, double faixaComissao){
        CodVendedor = codVendedor;
        Nome = nome;
        SalarioFixo = salarioFixo;
        FaixaComissao = faixaComissao;
    }

}