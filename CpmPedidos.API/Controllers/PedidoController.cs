using CpmPedidos.Domain;
using CpmPedidos.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CpmPedidos.API.Controllers
{
    [ApiController]
    [Route("pedidos")]
    public class PedidoController : AppBaseController
    {
        public PedidoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet("ticket-maximo")]
        public decimal TicketMaximo()
        {
            var repository = (IPedidoRepository)_serviceProvider.GetService(typeof(IPedidoRepository));
            return repository.TicketMaximo();
        }

        [HttpGet("por-cliente")]
        public dynamic PedidosClientes()
        {
            var repository = (IPedidoRepository)_serviceProvider.GetService(typeof(IPedidoRepository));
            return repository.PedidosClientes();
        }

        [HttpPost]
        public string SalvarPedido(PedidoDTO pedido)
        {
            return GetService<IPedidoRepository>().SalvarPedido(pedido);
        }
    }
}
