namespace ECommerce.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Email{ get; set; }
        public string? Senha { get; set; }
        public bool Comprador { get; set; }
        public bool Vendedor { get; set; }
    }
}
