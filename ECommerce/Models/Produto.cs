namespace ECommerce.Models
{
    public class Produto
    {
        public int ProdutoID { get; set; }
        public Usuario? Vendedor { get; set; }
        public string? Nome { get; set; }
        public double Valor { get; set; }
        public string? Descricao { get; set; }
    }
}
