using CpmPedidos.Domain;
using CpmPedidos.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CpmPedidos.API.Controllers
{
    [ApiController]
    [Route("cidades")]
    public class CidadeController : AppBaseController
    {
        public CidadeController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public dynamic Get()
        {
            return GetService<ICidadeRepository>().Get();
        }

        [HttpPost]
        public int Criar(CidadeDTO model)
        {
            return GetService<ICidadeRepository>().Criar(model);
        }

        [HttpPut]
        public int Alterar(CidadeDTO model)
        {
            return GetService<ICidadeRepository>().Alterar(model);
        }

        [HttpDelete("{id}")]
        public bool Excluir(int id)
        {
            return GetService<ICidadeRepository>().Excluir(id);
        }
    }
}
