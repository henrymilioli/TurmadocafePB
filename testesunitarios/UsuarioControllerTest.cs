using Cafeteria.Controllers;
using Cafeteria.Data;
using Cafeteria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Tests
{
    public class UsuarioControllerTest
    {
        [Fact]
        public async Task GetUsuarioById_ReturnsUsuario_WhenIdExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Crie um contexto de banco de dados em memória
            using (var dbContext = new DataContext(options))
            {
                // Adicione alguns usuários ao contexto em memória
                var existingUsuario = new Usuario { Id = Guid.NewGuid(), Nome = "Usuario Test", Email = "Email Test"};
                dbContext.Usuario.Add(existingUsuario);
                await dbContext.SaveChangesAsync();

                // Crie um controlador com o contexto em memória
                var controller = new UsuarioController(dbContext);

                // Act
                var result = await controller.GetUsuario(existingUsuario.Id);

                // Assert
                var okResult = Assert.IsType<ActionResult<Usuario>>(result);
                var returnValue = Assert.IsType<Usuario>(okResult.Value);
                Assert.Equal(existingUsuario, returnValue);
            }
        }
        [Fact]
        public async Task GetUsuarioById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Configurando o contexto do banco de dados em memória
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Crie um contexto de banco de dados
            using (var dbContext = new DataContext(options))
            {
                // Adicione alguns usuários ao contexto em memória
                dbContext.Usuario.Add(new Usuario { Id = Guid.NewGuid(), Nome = "Usuario 1", Email = "Email 1" });
                dbContext.Usuario.Add(new Usuario { Id = Guid.NewGuid(), Nome = "Usuario 2", Email = "Email 2" });
                await dbContext.SaveChangesAsync();

                // Crie um controlador com o contexto em memória
                var controller = new UsuarioController(dbContext);

                // Act
                var result = await controller.GetUsuario(nonExistingId);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }

    }
}
