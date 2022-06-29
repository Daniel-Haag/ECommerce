namespace ECommerce.Models
{
    public class RegistroCompra
    {
        public int RegistroCompraID { get; set; }
        public Usuario? Comprador { get; set; }
        public Produto? Produto{ get; set; }
    }
}
