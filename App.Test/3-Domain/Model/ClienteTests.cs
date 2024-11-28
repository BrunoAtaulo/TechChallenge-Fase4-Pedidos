using App.Domain.Models;
using Xunit;

namespace App.Test._3_Domain.Model
{
    public class ClienteTests
    {



        [Fact]
        public void Cliente_ShouldNotThrowException_WhenAllFieldsAreValid()
        {
            // Arrange
            var cliente = new Cliente("12345678901", "Nome", "Sobrenome", "email@exemplo.com", "Nome Social");

            // Act & Assert
            var exception = Record.Exception(() => cliente.ValidateEntity());
            Assert.Null(exception);
        }
    }

}
