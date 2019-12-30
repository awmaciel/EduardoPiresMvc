using System;
using System.Web.Mvc;
using EP.CursoMvc.Application.Interfaces;
using EP.CursoMvc.Application.ViewModels;
using EP.CursoMvc.Infra.CrossCutting.Filters;

namespace EP.CursoMvc.UI.Web.Controllers
{
    [Authorize]
    [RoutePrefix("area-administrativa/gestao-clientes")]
    public class ClientesController : BaseController
    {
        private readonly IClienteAppService _clienteAppService;

        public ClientesController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        [ClaimsAuthorize("Clientes","LI")]
        [Route("listar-todos")]
        public ActionResult Index()
        {
            return View(_clienteAppService.ObterAtivos());
        }

        [ClaimsAuthorize("Clientes", "DE")]
        [Route("{id:guid}/detalhes")]
        public ActionResult Details(Guid id)
        {
            var clienteViewModel = _clienteAppService.ObterPorId(id);

            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }

            return View(clienteViewModel);
        }

        [ClaimsAuthorize("Clientes", "IN")]
        [Route("criar-novo")]
        public ActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Clientes", "IN")]
        [Route("criar-novo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            if (!ModelState.IsValid) return View(clienteEnderecoViewModel);

            var clienteEnd = _clienteAppService.Adicionar(clienteEnderecoViewModel);

            if (clienteEnd.Cliente.ValidationResult.IsValid) return RedirectToAction("Index");

            PopularModelStateComErros(clienteEnd.Cliente.ValidationResult);
            
            return View(clienteEnderecoViewModel);
        }

        [Route("{id:guid}/editar")]
        [ClaimsAuthorize("Clientes", "ED")]
        public ActionResult Edit(Guid id)
        {
            var clienteViewModel = _clienteAppService.ObterPorId(id);

            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }

            return View(clienteViewModel);
        }

        [Route("{id:guid}/editar")]
        [ClaimsAuthorize("Clientes", "ED")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid) return View(clienteViewModel);

            _clienteAppService.Atualizar(clienteViewModel);
            return RedirectToAction("Index");
        }

        [Route("{id:guid}/excluir")]
        [ClaimsAuthorize("Clientes", "EX")]
        public ActionResult Delete(Guid id)
        {
            var clienteViewModel = _clienteAppService.ObterPorId(id);

            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }

            return View(clienteViewModel);
        }

        [Route("{id:guid}/excluir")]
        [ClaimsAuthorize("Clientes", "EX")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _clienteAppService.Remover(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _clienteAppService.Dispose();
        }
    }
}
