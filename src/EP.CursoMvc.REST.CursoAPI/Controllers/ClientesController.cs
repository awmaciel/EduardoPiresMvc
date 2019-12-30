using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using EP.CursoMvc.Application.Interfaces;
using EP.CursoMvc.Application.ViewModels;

namespace EP.CursoMvc.REST.CursoAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET")]
    public class ClientesController : ApiController
    {
        private readonly IClienteAppService _clienteAppService;

        public ClientesController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        public IEnumerable<ClienteViewModel> Get()
        {
            return _clienteAppService.ObterAtivos();
        }

        // GET: api/Clientes/5
        public ClienteViewModel Get(Guid id)
        {
            return _clienteAppService.ObterPorId(id);
        }

        // POST: api/Clientes
        public void Post([FromBody]ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            _clienteAppService.Adicionar(clienteEnderecoViewModel);
        }

        // PUT: api/Clientes/5
        public void Put(Guid id, [FromBody]ClienteViewModel clienteViewModel)
        {
            _clienteAppService.Atualizar(clienteViewModel);
        }

        // DELETE: api/Clientes/5
        public void Delete(Guid id)
        {
            _clienteAppService.Remover(id);
        }
    }
}
