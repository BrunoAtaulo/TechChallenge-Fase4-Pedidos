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
        public async Task PostPedido_ShouldAddPedidoToDatabase()
        {
            // Arrange
            using var context = new MySQLContext(_dbContextOptions);
            var repository = new PedidoRepository(context);

            var pedido = new PedidoBD(1, System.DateTime.Now, 1, 1);

            // Act
            await repository.PostPedido(pedido);

            // Assert
            var savedPedido = await context.Pedidos.FindAsync(pedido.Id);
            Assert.NotNull(savedPedido);
            Assert.Equal(pedido.Id, savedPedido.Id);
            Assert.Equal(pedido.ClienteId, savedPedido.ClienteId);
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
