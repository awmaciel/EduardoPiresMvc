using System;
using System.Collections.Generic;
using AutoMapper;
using DomainValidation.Validation;
using EP.CursoMvc.Application.Interfaces;
using EP.CursoMvc.Application.ViewModels;
using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Models;
using EP.CursoMvc.Infra.Data.UoW;

namespace EP.CursoMvc.Application.Services
{
    public class ClienteAppService : BaseService, IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;

        public ClienteAppService(IClienteRepository clienteRepository, 
                                 IClienteService clienteService,
                                 IUnitofWork uow) : base(uow)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
        }

        public ClienteEnderecoViewModel Adicionar(ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            var cliente = Mapper.Map<Cliente>(clienteEnderecoViewModel.Cliente);
            var endereco = Mapper.Map<Endereco>(clienteEnderecoViewModel.Endereco);

            cliente.DefinirComoAtivo();
            cliente.AdicionarEndereco(endereco);

            var clienteRet = _clienteService.Adicionar(cliente);
            // depois add um registro secundario

            AdicionarResultadoProcessamento(clienteRet.ValidationResult);

            if (ValidacaoProcesso.IsValid)
            {
                if (!Commit())
                {
                    // fazer alguma coisa, log, exception, add um erro para retornar ao cliente
                    clienteEnderecoViewModel.Cliente.ValidationResult.Add(new ValidationError("Ocorreu um erro no momento de salvar os dados no banco!"));
                }
            }

            clienteEnderecoViewModel.Cliente = Mapper.Map<ClienteViewModel>(clienteRet);
            return clienteEnderecoViewModel;
        }

        public ClienteViewModel Atualizar(ClienteViewModel clienteViewModel)
        {
            var cliente = Mapper.Map<Cliente>(clienteViewModel);
            _clienteService.Atualizar(cliente);

            return clienteViewModel;
        }

        public void Remover(Guid id)
        {
            _clienteService.Remover(id);
        }

        public ClienteViewModel ObterPorId(Guid id)
        {
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorId(id));
        }

        public IEnumerable<ClienteViewModel> ObterTodos()
        {
            return Mapper.Map<IEnumerable<ClienteViewModel>>(_clienteRepository.ObterTodos());
        }

        public IEnumerable<ClienteViewModel> ObterAtivos()
        {
            return Mapper.Map<IEnumerable<ClienteViewModel>>(_clienteRepository.ObterAtivos());
        }

        public ClienteViewModel ObterPorCpf(string cpf)
        {
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorCpf(cpf));
        }

        public ClienteViewModel ObterPorEmail(string email)
        {
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorEmail(email));
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
        }
    }
}