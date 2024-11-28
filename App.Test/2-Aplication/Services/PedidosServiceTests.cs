using App.Application.ViewModels.Enuns;
using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using App.Domain.Interfaces;
using App.Domain.Models;
using Application.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace App.Tests.Services
{
    public class PedidosServiceTests
    {
        private readonly Mock<IPedidosRepository> _mockRepository;
        private readonly PedidosService _service;

        public PedidosServiceTests()
        {
            _mockRepository = new Mock<IPedidosRepository>();
            _service = new PedidosService(_mockRepository.Object);
        }


        [Fact]
        public async Task GetPedido_ShouldReturnNull_WhenNoProductsExist()
        {
            // Arrange
            var filtro = new FiltroPedidos
            {
                IdPedido = 1,
                PedidoPagamento = EnumPedidoPagamento.Pago,
                PedidoStatus = EnumPedidoStatus.Recebido
            };


            _mockRepository.Setup(r => r.GetPedidosAsync(It.IsAny<int?>(),
                                                        (global::Domain.Base.EnumPedidoStatus?)It.IsAny<EnumPedidoStatus?>(),
                                                        (global::Domain.Base.EnumPedidoPagamento?)It.IsAny<EnumPedidoPagamento?>()))
                .ReturnsAsync(new List<PedidoBD>());

            // Act
            var result = await _service.GetPedidos(filtro);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdatePedido_ShouldReturnFalse_WhenPedidoDoesNotExist()
        {
            // Arrange
            var filtro = new FiltroPedidoById { idPedido = 1 };


            _mockRepository
                .Setup(r => r.GetPedidosByIdAsync(filtro.idPedido))
                .ReturnsAsync((PedidoBD)null);

            // Act
            var result = await _service.UpdatePedido(filtro);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdatePedido_ShouldUpdatePedido_WhenPedidoExists()
        {
            // Arrange
            var filtro = new FiltroPedidoById { idPedido = 1 };
            var pedidoExistente = new PedidoBD(1, DateTime.Now, 1, 1)
            {
                ClienteId = 123,
                Id = 1,
                PedidoStatusId = 1,
                PedidoPagamentoId = 2,
                DataPedido = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                StatusPedido = "Finalizado",
                Produtos = new List<Produto>(),



            };


            _mockRepository
                .Setup(r => r.GetPedidosByIdAsync(filtro.idPedido))
                .ReturnsAsync(pedidoExistente);

            _mockRepository
                .Setup(r => r.UpdatePedidoAsync(It.IsAny<PedidoBD>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdatePedido(filtro);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.UpdatePedidoAsync(It.Is<PedidoBD>(p =>
                p.PedidoPagamentoId == (int)EnumPedidoPagamento.Pago
            )), Times.Once);
        }

        [Fact]
        public async Task GetPedidos_ShouldReturnNull_WhenNoPedidosExist()
        {
            // Arrange
            var filtro = new FiltroPedidos
            {
                IdPedido = 1,
                PedidoStatus = EnumPedidoStatus.Recebido,
                PedidoPagamento = EnumPedidoPagamento.Pago
            };

            // Configuração do mock para retornar null quando GetPedidosAsync for chamado
            _mockRepository
                .Setup(r => r.GetPedidosAsync(filtro.IdPedido, (global::Domain.Base.EnumPedidoStatus?)filtro.PedidoStatus, (global::Domain.Base.EnumPedidoPagamento?)filtro.PedidoPagamento))
                .ReturnsAsync((IList<PedidoBD>)null);

            // Act
            var result = await _service.GetPedidos(filtro);

            // Assert
            Assert.Null(result);  // Verifica que o retorno é null quando não existem pedidos
        }

        [Fact]
        public async Task GetPedidos_ShouldReturnPedidos_WhenPedidosExist()
        {
            // Arrange
            var filtro = new FiltroPedidos
            {
                IdPedido = 1,
                PedidoStatus = EnumPedidoStatus.Recebido,
                PedidoPagamento = EnumPedidoPagamento.Pago
            };

            var pedidos = new List<PedidoBD>
            {
                new PedidoBD(1, DateTime.Now, 1, 1)
                {
                    ClienteId = 123,
                    Id = 1,
                    PedidoStatusId = (int)EnumPedidoStatus.Recebido,
                    PedidoPagamentoId = (int)EnumPedidoPagamento.Pago,
                    DataPedido = DateTime.Now
                },
                new PedidoBD(2, DateTime.Now, 2, 2)
                {
                    ClienteId = 124,
                    Id = 2,
                    PedidoStatusId = (int)EnumPedidoStatus.Recebido,
                    PedidoPagamentoId = (int)EnumPedidoPagamento.Pago,
                    DataPedido = DateTime.Now
                }
            };


            _mockRepository
                .Setup(r => r.GetPedidosAsync(filtro.IdPedido, (global::Domain.Base.EnumPedidoStatus?)filtro.PedidoStatus, (global::Domain.Base.EnumPedidoPagamento?)filtro.PedidoPagamento))
                .ReturnsAsync(pedidos);

            // Act
            var result = await _service.GetPedidos(filtro);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, item => Assert.IsType<Pedido>(item));
            Assert.Equal(pedidos[0].Id, result[0].IdPedido);
        }
    }
}
