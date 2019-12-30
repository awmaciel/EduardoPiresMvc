using System;
using EP.CursoMvc.Domain.Models;
using EP.CursoMvc.Domain.Specifications.Clientes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EP.CursoMvc.Domain.Tests.Specification
{
    [TestClass]
    public class CpfSpecificationTests
    {
        [TestMethod]
        public void CpfSpecification_Valido_True()
        {
            // Arrange
            var cliente = new Cliente("Eduardo", "contato@eduardopires.net.br", "30390600822", new DateTime(1982, 04, 24), true);

            var cpfSpec = new CPFValidoSpecification();

            // Act
            var result = cpfSpec.IsSatisfiedBy(cliente);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CpfSpecification_Valido_False()
        {
            // Arrange
            var cliente = new Cliente("Eduardo", "contato@eduardopires.net.br", "30390600821", new DateTime(1982, 04, 24), true);

            var cpfSpec = new CPFValidoSpecification();

            // Act
            var result = cpfSpec.IsSatisfiedBy(cliente);

            // Assert
            Assert.IsFalse(result);
        }
    }
}