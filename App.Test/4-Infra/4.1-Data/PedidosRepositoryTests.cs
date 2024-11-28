using App.Application.ViewModels.Enuns;
using App.Domain.Interfaces;
using App.Domain.Models;
using App.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace App.Tests.Repositories
{
    public class PedidosRepositoryTests
    {
        private readonly DbContextOptions<MySQLContext> _dbContextOptions;

        public PedidosRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<MySQLContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }



        [Fact]
        public async Task GetProdutosByIdCategoria_ShouldReturnFilteredProducts_WhenCategoriaExists()
        {
            // Arrange
            using var context = new MySQLContext(_dbContextOptions);
            var repository = new PedidoRepository(context);

            context.Pedidos.AddRange(
                new PedidoBD(1, System.DateTime.MaxValue, 1, 1),
                new PedidoBD(2, System.DateTime.MaxValue, 2, 2),
                new PedidoBD(3, System.DateTime.MaxValue, 3, 3)
            );
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetPedidosAsync(1, (global::Domain.Base.EnumPedidoStatus?)EnumPedidoStatus.Finalizado, (global::Domain.Base.EnumPedidoPagamento?)EnumPedidoPagamento.Cancelado);

            // Assert
            Assert.NotNull(result);

            Assert.All(result, p => Assert.Equal(1, p.ClienteId));
        }

        [Fact]
        public async Task GetPedidosByIdAsync_ShouldReturnPedido_WhenPedidoExists()
        {
            // Arrange
            using var context = new MySQLContext(_dbContextOptions);
            var repository = new PedidoRepository(context);

            var pedidoId = 1;
            var pedido = new PedidoBD(1, System.DateTime.MaxValue, 1, 1);
            context.Pedidos.Add(pedido);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetPedidosByIdAsync(pedidoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pedidoId, result.Id);
            Assert.Equal(1, result.ClienteId);
        }

        [Fact]
        public async Task UpdatePedidoAsync_ShouldReturnTrue_WhenPedidoIsUpdatedSuccessfully()
        {
            // Arrange
            using var context = new MySQLContext(_dbContextOptions);
            var repository = new PedidoRepository(context);

            var pedido = new PedidoBD(1, System.DateTime.MaxValue, 1, 1);
            context.Pedidos.Add(pedido);
            await context.SaveChangesAsync();

            pedido.ClienteId = 2;

            // Act
            var result = await repository.UpdatePedidoAsync(pedido);

            // Assert
            Assert.True(result);

            // Verificar se a atualização foi persistida no banco de dados
            var updatedPedido = await context.Pedidos.FindAsync(pedido.Id);
            Assert.NotNull(updatedPedido);
            Assert.Equal(2, updatedPedido.ClienteId);
        }



    }
}
