// "//-----------------------------------------------------------------------".
// <copyright file="CartController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Packt.Ecommerce.DTO.Models;
    using Packt.Ecommerce.Web.Contracts;

    /// <summary>
    /// Shopping Cart controller.
    /// </summary>
    public class CartController : Controller
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private readonly ILogger<CartController> logger;

        /// <summary>
        /// Ecommerce instance.
        /// </summary>
        private readonly IECommerceService eCommerceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="eCommerceService">Service.</param>
        public CartController(ILogger<CartController> logger, IECommerceService eCommerceService)
        {
            this.logger = logger;
            this.eCommerceService = eCommerceService;
        }

        /// <summary>
        /// Add products to cart.
        /// </summary>
        /// <param name="product">Product.</param>
        /// <returns>Created OrderId.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ProductListViewModel product)
        {
            if (product == null)
            {
                return this.BadRequest();
            }

            OrderDetailsViewModel newOrder = new OrderDetailsViewModel();
            if (this.ModelState.IsValid)
            {
                newOrder.UserId = "test";
                newOrder.Products = new List<ProductListViewModel>();
                product.Quantity = 1;
                newOrder.Products.Add(product);
                newOrder = await this.eCommerceService.CreateOrUpdateOrder(newOrder).ConfigureAwait(false);
            }

            return this.RedirectToAction("Index", new { orderId = newOrder.Id });
        }

        /// <summary>
        /// Get the details of cart.
        /// </summary>
        /// <param name="orderId">Order Id.</param>
        /// <returns>Cart (Order).</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                return this.NotFound();
            }

            var newOrder = await this.eCommerceService.GetOrderByIdAsync(orderId).ConfigureAwait(false);
            if (newOrder != null && newOrder.OrderStatus == OrderStatus.Cart.ToString())
            {
                newOrder.PaymentMode = PaymentMode.Visa.ToString();
                return this.View(newOrder);
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}
