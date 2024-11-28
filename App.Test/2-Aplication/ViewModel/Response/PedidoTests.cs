using App.Application.ViewModels.Enuns;
using App.Application.ViewModels.Response;
using App.Domain.Models;
using System;
using Xunit;

namespace App.Test._2_Aplication.ViewModel.Response
{
    public class PedidoTests
    {
        [Fact]
        public void Pedido_Constructor_ShouldMapPropertiesFromPedidoBD()
        {
            // Arrange
            var pedidoBD = new PedidoBD(1, DateTime.Now, 1, 2)
            {
                ClienteId = 123,
                Id = 456,
                PedidoStatusId = (int)EnumPedidoStatus.Recebido,
                PedidoPagamentoId = (int)EnumPedidoPagamento.Pago,
                DataPedido = DateTime.Now
            };

            // Act
            var pedido = new Pedido(pedidoBD);

            // Assert
            Assert.Equal(pedidoBD.ClienteId, pedido.IdCliente);
            Assert.Equal(pedidoBD.Id, pedido.IdPedido);
            Assert.Equal((EnumPedidoStatus)pedidoBD.PedidoStatusId, pedido.PedidoStatus);
            Assert.Equal((EnumPedidoPagamento)pedidoBD.PedidoPagamentoId, pedido.PedidoPagamento);
            Assert.Equal(pedidoBD.DataPedido, pedido.DataPedido);
        }



    }
}
