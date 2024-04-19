using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.Identity.Client;
using Moq;
using MyStore01.WebUI.Controllers;
using MyStore01.WebUI.Models;
using MyStore01.WebUI.Models.Users_Info;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace MyStore01.Tests
{
    
    public class UnitTests
    {
        [Fact]
        public void CanUseRepository()
        {
            //arrange
            Mock<IStoreRepository>mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] { new Product { Name = "P1", Id = 1 },
                new Product { Name = "P2", Id = 2 } }).AsQueryable<Product>());
            HomeController controller = new HomeController(mock.Object);
            //act
            IEnumerable<Product> result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;
            //assert
            Product[]? prodar = result?.ToArray() ?? Array.Empty<Product>();
            Assert.NotNull(prodar);
            Assert.True(prodar.Length == 2);
            Assert.Equal("P1", prodar[0].Name);
            Assert.Equal("P2", prodar[1].Name);

        }
    }
}
