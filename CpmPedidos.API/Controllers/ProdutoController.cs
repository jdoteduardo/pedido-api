using CpmPedidos.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CpmPedidos.API.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutoController : AppBaseController
    {
        public ProdutoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public dynamic Get([FromQuery] string ordem = "")
        {
            var repository = (IProdutoRepository)_serviceProvider.GetService(typeof(IProdutoRepository));
            return repository.Get(ordem);
        }

        [HttpGet("search/{text}/{pagina?}")]
        public dynamic GetSearch(string text, int pagina = 1, [FromQuery] string ordem = "")
        {
            var repository = (IProdutoRepository)_serviceProvider.GetService(typeof(IProdutoRepository));
            return repository.Search(text, pagina, ordem);
        }

        [HttpGet("{id}")]
        public dynamic Detail(int? id)
        {
            if(( id ?? 0 ) > 0)
            {
                var repository = (IProdutoRepository)_serviceProvider.GetService(typeof(IProdutoRepository));
                return repository.Detail(id.Value);
            } else
            {
                return null;
            }
        }

        [HttpGet("{id}/imagens")]
        public dynamic Imagens(int? id)
        {
            if ((id ?? 0) > 0)
            {
                var repository = (IProdutoRepository)_serviceProvider.GetService(typeof(IProdutoRepository));
                return repository.Imagens(id.Value);
            }
            else
            {
                return null;
            }
        }
    }
}
