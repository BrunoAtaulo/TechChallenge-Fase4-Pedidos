using Api.Controllers;
using App.Application.Interfaces;
using App.Application.ViewModels.Enuns;
using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using App.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace App.Test._1_WebAPI.Controllers
{
    public class PedidosControllerTests
    {
        private readonly PedidosController _controller;
        private readonly Mock<IPedidosService> _appService;
        private readonly List<PedidoBD> _fakeProdutos;

        public PedidosControllerTests()
        {
            _appService = new Mock<IPedidosService>();
            _controller = new PedidosController(_appService.Object);
            _fakeProdutos = new List<PedidoBD>();

            for (int i = 1; i <= 10; i++)
            {
                _fakeProdutos.Add(new PedidoBD(i, DateTime.Now, i, i));
            }


        }



        #region [GET]
        [Fact(DisplayName = "BuscarListaPedidos Ok")]
        public async Task GetPedidos_Returns_Ok()
        {
            var filtro = new FiltroPedidos { IdPedido = 1, PedidoStatus = EnumPedidoStatus.Finalizado, PedidoPagamento = EnumPedidoPagamento.Pago };

            _appService.Setup(service => service.GetPedidos(filtro))
       .ReturnsAsync(new List<Pedido>());

            // Act
            var result = await _controller.GetPedidos(filtro);

            // Assert
            Assert.IsType<OkObjectResult>(result);



            // Assert
            Assert.True(result is OkObjectResult);


        }
        [Fact(DisplayName = "BuscarListaPedidos Empty")]
        public async Task GetPedidos_Returns_Empty()
        {
            // Arrange
            var filtro = new FiltroPedidos
            {
                IdPedido = 1,
                PedidoStatus = EnumPedidoStatus.Finalizado,
                PedidoPagamento = EnumPedidoPagamento.Pago
            };

            // Configura o mock para retornar uma lista vazia
            _appService.Setup(service => service.GetPedidos(filtro))
         .ReturnsAsync((List<Pedido>)null);

            // Act
            var result = await _controller.GetPedidos(filtro);

            // Assert
            Assert.True(result is NoContentResult); // Verifica se o retorno é NoContent (204)
        }




        #endregion



        #region [PATCH]

        [Fact(DisplayName = "PatchProdutos OK")]
        public async Task PatchProdutos_Returns_OK()
        {
            // Arrange
            var item = new FiltroPedidoById
            {
                idPedido = 1,
            };
            _appService.Setup(service => service.UpdatePedido(It.IsAny<FiltroPedidoById>()))
           .ReturnsAsync(true);


            // Act
            var result = await _controller.PatchPedido(item);
            // Assert
            Assert.True(result is OkObjectResult);
        }


        #endregion



    }
}
