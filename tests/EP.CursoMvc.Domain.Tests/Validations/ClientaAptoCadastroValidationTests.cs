using System;
using System.Linq;
using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Models;
using EP.CursoMvc.Domain.Validations.Clientes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace EP.CursoMvc.Domain.Tests.Validations
{
    [TestClass]
    public class ClientaAptoCadastroValidationTests
    {
        [TestMethod]
        public void ClienteApto_Validation_True()
        {
            // Arrange
            var cliente = new Cliente("Eduardo", "contato@eduardopires.net.br", "30390600822", new DateTime(1982, 04, 24), true);

            // MOCK
            var repo = MockRepository.GenerateStub<IClienteRepository>();
            repo.Stub(s => s.ObterPorEmail(cliente.Email)).Return(null);
            repo.Stub(s => s.ObterPorCpf(cliente.CPF)).Return(null);

            var cliValidation = new ClienteEstaAptoParaCadastroValidation(repo);

            // Act
            var result = cliValidation.Validate(cliente);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void ClienteApto_Validation_False()
        {
            // Arrange
            var cliente = new Cliente("Eduardo", "contato@eduardopires.net.br", "30390600822", new DateTime(1982, 04, 24), true);

            // MOCK
            var repo = MockRepository.GenerateStub<IClienteRepository>();
            repo.Stub(s => s.ObterPorEmail(cliente.Email)).Return(cliente);
            repo.Stub(s => s.ObterPorCpf(cliente.CPF)).Return(cliente);

            var cliValidation = new ClienteEstaAptoParaCadastroValidation(repo);

            // Act
            var result = cliValidation.Validate(cliente);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Erros.Any(e => e.Message == "Já existe um cliente com este CPF"));
            Assert.IsTrue(result.Erros.Any(e => e.Message == "Já existe um cliente com este E-mail"));
        }
    }
}