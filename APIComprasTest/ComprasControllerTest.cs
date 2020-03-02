using APICompras.Controllers;
using APICompras.Models;
using APICompras.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace APIComprasTest
{
    public class ComprasControllerTest
    {
        ComprasController _controller;
        ICompraService _service;
        public ComprasControllerTest()
        {
            _service = new CompraServiceFake();
            _controller = new ComprasController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<CompraItem>>(okResult.Value);
            Assert.Equal(5, items.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(99);
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testeId = 1;
            // Act
            var okResult = _controller.Get(testeId);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testeId = 1;
            // Act
            var okResult = _controller.Get(testeId).Result as OkObjectResult;
            // Assert
            Assert.IsType<CompraItem>(okResult.Value);
            Assert.Equal(testeId, (okResult.Value as CompraItem).Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new CompraItem()
            {
                Fabricante = "Logitech",
                Preco = 23.00M
            };
            _controller.ModelState.AddModelError("Nome", "Required");
            // Act
            var badResponse = _controller.Post(nameMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            CompraItem testeItem = new CompraItem()
            {
                Nome = "Mouse Ótico 200 dpi",
                Fabricante = "Logitech",
                Preco = 23.00M
            };
            // Act
            var createdResponse = _controller.Post(testeItem);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new CompraItem()
            {
                Nome = "Mouse Ótico 200 dpi",
                Fabricante = "Logitech",
                Preco = 23.00M
            };
            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as CompraItem;
            // Assert
            Assert.IsType<CompraItem>(item);
            Assert.Equal("Mouse Ótico 200 dpi", item.Nome);
        }

        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var IdInexistente = 99;
            // Act
            var badResponse = _controller.Remove(IdInexistente);
            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var Id_Existente = 2;
            // Act
            var okResponse = _controller.Remove(Id_Existente);
            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Remove_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var Id_Existente = 2;
            // Act
            var okResponse = _controller.Remove(Id_Existente);
            // Assert
            Assert.Equal(4, _service.GetAllItems().Count());
        }
    }
}
