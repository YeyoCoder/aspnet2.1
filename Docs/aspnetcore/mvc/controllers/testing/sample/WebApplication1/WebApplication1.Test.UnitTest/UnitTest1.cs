using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Controllers;
using Xunit;

namespace WebApplication1.Test.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arange
            var controller = new HomeController();

            //Act 
            var result = controller.Index();

            //Assert
            var ViewModel = Assert.IsType<ViewResult>(result);
        }
    }
}
