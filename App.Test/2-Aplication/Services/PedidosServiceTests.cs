using App.Application.ViewModels.Request;
using App.Domain.Interfaces;
using App.Domain.Models;
using App.Infra.Data.Context;
using App.Test.MockObjects;
using Application.Services;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace App.Tests.Services
{
    public class PedidosServiceTests
    {
        private readonly Mock<IPedidosRepository> _mockRepository;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly PedidosService _service;

        public PedidosServiceTests()
        {
            // Mock do repositório
            _mockRepository = new Mock<IPedidosRepository>();

            // Mock do HttpClient
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

            // Instância do serviço
            _service = new PedidosService(_mockRepository.Object, _httpClient);
        }

        [Fact]
        public async Task GetPedidoById_ShouldReturnPedido_WhenPedidoExists()
        {
            // Arrange
            var pedidoBD = new PedidoBD(123, DateTime.Now, 1, 1)
            {
                Id = 1
            };

            _mockRepository
                .Setup(r => r.GetPedidosByIdAsync(1))
                .ReturnsAsync(pedidoBD);

            var filtro = new FiltroPedidoById { idPedido = 1 };

            // Act
            var result = await _service.GetPedidoById(filtro);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pedidoBD.Id, result.IdPedido);
        }

        [Fact]
        public async Task GetPedidoById_ShouldReturnNull_WhenPedidoDoesNotExist()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetPedidosByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((PedidoBD)null);

            var filtro = new FiltroPedidoById { idPedido = 1 };

            // Act
            var result = await _service.GetPedidoById(filtro);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PostPedido_ShouldReturnFiltroPedidoById_WithValidStatusPagamento()
        {
            // Arrange
            var input = new PostPedidoRequest
            {
                IdCliente = 123,
                DataPedido = DateTime.Now,
                PedidoStatusId = 1,
                PedidoPagamentoId = 2
            };

            var pedidoBD = new PedidoBD(input.IdCliente, input.DataPedido, input.PedidoStatusId, input.PedidoPagamentoId)
            {
                Id = 1
            };

            _mockRepository
                .Setup(r => r.PostPedido(It.IsAny<PedidoBD>()))
                .Callback<PedidoBD>(p => p.Id = pedidoBD.Id)
                .Returns(Task.CompletedTask);

            var mockHttpMessageHandler = new MockHttpMessageHandler(request =>
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,

                };
            });

            var httpClient = new HttpClient(mockHttpMessageHandler);

            var service = new PedidosService(_mockRepository.Object, httpClient);

            // Act
            var result = await service.PostPedido(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pedidoBD.Id, result.idPedido);

        }

        [Fact]
        public async Task PostPedido_ShouldReturnFiltroPedidoById_WithErrorMessage_WhenHttpClientFails()
        {
            // Arrange
            var input = new PostPedidoRequest
            {
                IdCliente = 123,
                DataPedido = DateTime.Now,
                PedidoStatusId = 1,
                PedidoPagamentoId = 2
            };

            var pedidoBD = new PedidoBD(input.IdCliente, input.DataPedido, input.PedidoStatusId, input.PedidoPagamentoId)
            {
                Id = 1
            };

            _mockRepository
                .Setup(r => r.PostPedido(It.IsAny<PedidoBD>()))
                .Callback<PedidoBD>(p => p.Id = pedidoBD.Id)
                .Returns(Task.CompletedTask);

            var mockHttpMessageHandler = new MockHttpMessageHandler(request =>
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            });

            var httpClient = new HttpClient(mockHttpMessageHandler);

            var service = new PedidosService(_mockRepository.Object, httpClient);

            // Act
            var result = await service.PostPedido(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pedidoBD.Id, result.idPedido);
            Assert.StartsWith("Erro ao obter status", result.statusPagamento);
        }


    }
}
