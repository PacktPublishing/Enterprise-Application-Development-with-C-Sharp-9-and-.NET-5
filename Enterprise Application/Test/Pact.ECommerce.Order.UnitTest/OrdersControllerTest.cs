using Microsoft.AspNetCore.Mvc;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Packt.Ecommerce.DTO.Models;
using Packt.Ecommerce.Order.Contracts;
using Packt.Ecommerce.Order.Contracts.Fakes;
using Packt.Ecommerce.Order.Controllers;
using Packt.Ecommerce.Order.Services.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pact.ECommerce.Order.UnitTest
{
    [TestClass]
    public class OrdersControllerTest
    {

        [TestMethod]
        public async Task OrderController_Constructor()
        {
            OrdersController testObject = new OrdersController(null);
            Assert.IsNotNull(testObject);
        }


        [TestMethod]
        public async Task When_GetOrdersAsync_with_ExistingOrder_receive_OkObjectResult()
        {
            var stub = new StubIOrderService()
            {
                GetOrderByIdAsyncString = (x) => Task.FromResult<OrderDetailsViewModel>(new OrderDetailsViewModel() { Id = "1" }),
            };

            OrdersController testObject = new OrdersController(stub);
            var order = (await testObject.GetOrderById("1"));
            Assert.IsInstanceOfType(order, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task When_GetOrdersAsync_with_No_ExistingOrder_receive_NotFoundResult()
        {
            var stub = new StubIOrderService()
            {
                GetOrderByIdAsyncString = (x) => Task.FromResult<OrderDetailsViewModel>(null),
            };

            OrdersController testObject = new OrdersController(stub);
            var order = (await testObject.GetOrderById("1"));
            Assert.IsInstanceOfType(order, typeof(NotFoundResult));
        }
    }
}
