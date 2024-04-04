using Xunit;
using Moq;
using Cafeteria.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Cafeteria.Data;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Cafeteria.Models;
using Cafeteria.DTO;

namespace Cafeteria.Tests
{
    public class CafeteriaCControllerTests
    {
        [Fact]
        public async Task GetCafeteriaCById_ReturnsCafeteria_WhenIdExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Crie um contexto de banco de dados em memória
            using (var dbContext = new DataContext(options))
            {
                // Adicione algumas cafeterias ao contexto em memória
                var existingCafeteria = new CafeteriaC { Id = Guid.NewGuid(), Nome = "Cafeteria Test", Endereco = "Endereco Test" };
                dbContext.CafeteriaC.Add(existingCafeteria);
                await dbContext.SaveChangesAsync();

                // Crie um controlador com o contexto em memória
                var controller = new CafeteriaCController(dbContext);

                // Act
                var result = await controller.GetCafeteriaC(existingCafeteria.Id);

                // Assert
                var okResult = Assert.IsType<ActionResult<CafeteriaC>>(result);
                var returnValue = Assert.IsType<CafeteriaC>(okResult.Value);
                Assert.Equal(existingCafeteria, returnValue);
            }
        }
        [Fact]
        public async Task GetCafeteriaCById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Configurando o contexto do banco de dados em memória
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Adicionando algumas cafeterias ao contexto em memória
            using (var dbContext = new DataContext(options))
            {
                dbContext.CafeteriaC.Add(new CafeteriaC { Id = Guid.NewGuid(), Nome = "Cafeteria 1", Endereco = "Endereco 1" });
                dbContext.CafeteriaC.Add(new CafeteriaC { Id = Guid.NewGuid(), Nome = "Cafeteria 2", Endereco = "Endereco 2" });
                await dbContext.SaveChangesAsync();
            }

            // Criando um novo contexto do banco de dados em memória para cada teste
            using (var dbContext = new DataContext(options))
            {
                var controller = new CafeteriaCController(dbContext);

                // Act
                var result = await controller.GetCafeteriaC(nonExistingId);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }


        [Fact]
        public async Task PostCafeteriaC_ReturnsCreatedResponse_WhenValidCafeteriaCDTO()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Inicializa o contexto do banco de dados em memória
            using (var dbContext = new DataContext(options))
            {
                var controller = new CafeteriaCController(dbContext);
                var cafeteriaCDTO = new CafeteriaCDTO { Nome = "Nova Cafeteria", Endereco = "Novo Endereço", UsuarioId = Guid.NewGuid() };

                // Act
                var result = await controller.PostCafeteriaC(cafeteriaCDTO);

                // Assert
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var returnValue = Assert.IsType<CafeteriaC>(createdAtActionResult.Value);
                Assert.Equal(cafeteriaCDTO.Nome, returnValue.Nome);
                Assert.Equal(cafeteriaCDTO.Endereco, returnValue.Endereco);
            }
        }



        [Fact]
        public async Task DeleteCafeteriaC_ReturnsNotFound_WhenCafeteriaDoesNotExist()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Crie uma instância real do DataContext usando um contexto de banco de dados em memória
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var context = new DataContext(options);

            // Crie uma instância do controlador usando o contexto do banco de dados em memória
            var controller = new CafeteriaCController(context);

            // Act
            var result = await controller.DeleteCafeteriaC(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }



    }
}