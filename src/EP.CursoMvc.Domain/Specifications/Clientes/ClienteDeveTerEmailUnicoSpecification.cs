using DomainValidation.Interfaces.Specification;
using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Models;

namespace EP.CursoMvc.Domain.Specifications.Clientes
{
    public class ClienteDeveTerEmailUnicoSpecification : ISpecification<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteDeveTerEmailUnicoSpecification(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public bool IsSatisfiedBy(Cliente cliente)
        {
            return _clienteRepository.ObterPorEmail(cliente.Email) == null;
        }
    }
}