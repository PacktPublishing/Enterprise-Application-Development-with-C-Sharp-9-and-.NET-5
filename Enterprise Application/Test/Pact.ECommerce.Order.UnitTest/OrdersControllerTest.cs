namespace Pact.ECommerce.Order.UnitTest
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Packt.Ecommerce.DTO.Models;
    using Packt.Ecommerce.Order.Contracts;
    using Packt.Ecommerce.Order.Controllers;

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
            var stub = new Mock<IOrderService>();
            stub.Setup(x => x.GetOrderByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<OrderDetailsViewModel>(new OrderDetailsViewModel { Id = "1" }));
            OrdersController testObject = new OrdersController(stub.Object);

            var order = await testObject.GetOrderById("1").ConfigureAwait(false);
            Assert.IsInstanceOfType(order, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task When_GetOrdersAsync_with_No_ExistingOrder_receive_NotFoundResult()
        {
            var stub = new Mock<IOrderService>();
            stub.Setup(x => x.GetOrderByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<OrderDetailsViewModel>(null));
            OrdersController testObject = new OrdersController(stub.Object);

            var order = await testObject.GetOrderById("1").ConfigureAwait(false);
            Assert.IsInstanceOfType(order, typeof(NotFoundResult));
        }
    }
}
