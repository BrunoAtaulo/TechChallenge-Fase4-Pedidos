using App.Domain.Models;
using System;
using Xunit;

namespace App.Tests.Models
{
    public class PedidoBDTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenValidArgumentsAreProvided()
        {
            // Arrange
            int clienteId = 1;
            DateTime dataPedido = DateTime.Now;
            int pedidoStatusId = 1;
            int pedidoPagamentoId = 1;

            // Act
            var pedido = new PedidoBD(clienteId, dataPedido, pedidoStatusId, pedidoPagamentoId);

            // Assert
            Assert.Equal(clienteId, pedido.ClienteId);
            Assert.Equal(dataPedido, pedido.DataPedido);
            Assert.Equal(pedidoStatusId, pedido.PedidoStatusId);
            Assert.Equal(pedidoPagamentoId, pedido.PedidoPagamentoId);
        }



        [Fact]
        public void ValidateEntity_ShouldNotThrowException_WhenAllArgumentsAreValid()
        {
            // Arrange
            int clienteId = 1;
            DateTime dataPedido = DateTime.Now;
            int pedidoStatusId = 1;
            int pedidoPagamentoId = 1;

            // Act & Assert
            var exception = Record.Exception(() => new PedidoBD(clienteId, dataPedido, pedidoStatusId, pedidoPagamentoId));
            Assert.Null(exception); // No exception should be thrown
        }


    }
}
