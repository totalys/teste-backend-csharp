using System;
using System.Collections.Generic;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;

namespace Tests.TorreHanoi.Domain
{
    [TestClass]
    public class TorreHanoiUnit
    {
        private const string CategoriaTeste = "Domain/TorreHanoi";

        private Mock<ILogger> _mockLogger;

        [TestInitialize]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Construtor_Deve_Retornar_Sucesso()
        {
            //Arrange
            int createdDiscs = 3;

            //Act
            var torreHanoi = new global::Domain.TorreHanoi.TorreHanoi(createdDiscs, _mockLogger.Object);

            //Assert
            Assert.IsNull(torreHanoi.DataFinalizacao);
            torreHanoi.Id.Should().NotBe(Guid.Empty);
            torreHanoi.Discos.Count.Should().Be(createdDiscs);
            torreHanoi.Origem.Discos.Count.Should().Be(createdDiscs);
            torreHanoi.Destino.Discos.Count.Should().Be(0);
            torreHanoi.Intermediario.Discos.Count.Should().Be(0);
            torreHanoi.PassoAPasso.ShouldBeEquivalentTo(new List<string>());
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Processar_Deverar_Retornar_Sucesso()
        {
            //Arrange
            var createdDiscs = 4;
            var expectedSteps = (int) Math.Pow(2, createdDiscs) - 1;
            var expectedState = global::Domain.TorreHanoi.TipoStatus.FinalizadoSucesso;
            var torreHanoi = new global::Domain.TorreHanoi.TorreHanoi(createdDiscs, _mockLogger.Object);

            //Act
            torreHanoi.Processar();

            //Assert
            torreHanoi.Status.Should().Be(expectedState);
            torreHanoi.Discos.Count.Should().Be(createdDiscs);
            torreHanoi.Origem.Discos.Count.Should().Be(0);
            torreHanoi.Intermediario.Discos.Count.Should().Be(0);
            torreHanoi.PassoAPasso.Count.Should().Be(expectedSteps);
            torreHanoi.DataFinalizacao.Should().BeAfter(torreHanoi.DataCriacao);
            
        }
    }
}
