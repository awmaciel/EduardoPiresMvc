using System;
using System.Linq;
using EP.CursoMvc.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EP.CursoMvc.Domain.Tests.Models
{
    [TestClass]
    public class ClienteTests
    {
        // AAA => Arrange, Act, Assert

        [TestMethod]
        public void Cliente_EhValido_DeveRetornarValido()
        {
            // Arrange
            var cliente = new Cliente("Eduardo", "contato@eduardopires.net.br", "30390600822",
                new DateTime(1982, 04, 24), true);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Cliente_EhValido_DeveRetornarComErros()
        {
            // Arrange
            var cliente = new Cliente("Eduardo", "contato2eduardopires.net.br", "30390600821", DateTime.Now, true);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(3, cliente.ValidationResult.Erros.Count());
            Assert.IsTrue(cliente.ValidationResult.Erros.Any(e => e.Message == "Cliente informou um CPF inválido."));
            Assert.IsTrue(cliente.ValidationResult.Erros.Any(e => e.Message == "Cliente informou um e-mail inválido."));
            Assert.IsTrue(cliente.ValidationResult.Erros.Any(e => e.Message == "Cliente não tem maioridade para cadastro."));
        }
    }
}