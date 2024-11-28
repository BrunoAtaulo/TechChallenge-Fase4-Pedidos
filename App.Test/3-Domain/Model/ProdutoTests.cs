using App.Domain.Models;
using Xunit;

namespace App.Test._3_Domain.Model
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_ShouldNotThrowException_WhenAllFieldsAreValid()
        {
            // Arrange
            var produto = new Produto(1, "Produto Teste", 100.00m, true);

            // Act & Assert
            var exception = Record.Exception(() => produto.ValidateEntity());
            Assert.Null(exception);  // Verifica que não há exceção
        }


    }
}
