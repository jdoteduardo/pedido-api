using CpmPedidos.Domain;
using CpmPedidos.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CpmPedidos.Repository
{
    public class PedidoRepository : BaseRepository, IPedidoRepository
    {
        private string GetProximoNumero()
        {
            var ret = 1.ToString("00000");

            var ultNumero = _context.Pedidos.Max(x => x.Numero);

            if(!string.IsNullOrEmpty(ultNumero))
            {
                ret = (Convert.ToInt32(ultNumero) + 1).ToString("00000");
            }

            return ret;
        }

        public PedidoRepository(ApplicationDbContext context) : base(context) { }

        public dynamic PedidosClientes()
        {
            var hoje = DateTime.Today;
            var inicioMes = new DateTime(hoje.Year, hoje.Month, 1);
            var finalMes = new DateTime(hoje.Year, hoje.Month, DateTime.DaysInMonth(hoje.Year, hoje.Month));

            return _context.Pedidos
                .Where(x => x.CriadoEm.Date >= inicioMes && x.CriadoEm.Date <= finalMes)
                .GroupBy(
                    pedido => new { pedido.IdCliente, pedido.Cliente.Nome },
                    (chave, pedidos) => new
                    {
                        Cliente = chave.Nome,
                        Pedidos = pedidos.Count(),
                        Total = pedidos.Sum(pedido => pedido.ValorTotal)
                    })
                .ToList();
        }

        public decimal TicketMaximo()
        {
            var hoje = DateTime.Today;

            return _context.Pedidos
                .Where(x => x.CriadoEm.Date == hoje)
                .Max(x => (decimal?)x.ValorTotal) ?? 0;
        }

        public string SalvarPedido(PedidoDTO pedido)
        {
            var ret = "";

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var entity = new Pedido
                        {
                            Numero = GetProximoNumero(),
                            IdCliente = pedido.IdCliente,
                            CriadoEm = DateTime.Now,
                            Produtos = new List<ProdutoPedido>()
                        };

                        var valorTotal = 0m;

                        foreach (var prodPed in pedido.Produtos)
                        {
                            var precoProduto = _context.Produtos
                                .Where(x => x.Id == prodPed.IdProduto)
                                .Select(x => x.Preco)
                                .FirstOrDefault();

                            if (precoProduto > 0)
                            {
                                valorTotal += prodPed.Quantidade * precoProduto;

                                entity.Produtos.Add(new ProdutoPedido
                                {
                                    IdProduto = prodPed.IdProduto,
                                    Quantidade = prodPed.Quantidade,
                                    Preco = precoProduto
                                });
                            }
                        }

                        entity.ValorTotal = valorTotal;

                        _context.Pedidos.Add(entity);
                        _context.SaveChanges();

                        transaction.Commit();

                        ret = entity.Numero;
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch(Exception ex)
            {
            }

            return ret;
        }
    }
}
