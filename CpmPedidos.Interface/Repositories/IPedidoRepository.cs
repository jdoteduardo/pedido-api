using CpmPedidos.Domain;

namespace CpmPedidos.Interface
{
    public interface IPedidoRepository
    {
        decimal TicketMaximo();
        dynamic PedidosClientes();
        string SalvarPedido(PedidoDTO pedido);
    }
}
