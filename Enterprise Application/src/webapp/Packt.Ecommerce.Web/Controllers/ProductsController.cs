// "//-----------------------------------------------------------------------".
// <copyright file="ProductsController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Packt.Ecommerce.Web.Contracts;
    using Packt.Ecommerce.Web.Models;

    /// <summary>
    /// The products Controller.
    /// </summary>
    public class ProductsController : Controller
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private readonly ILogger<ProductsController> logger;

        /// <summary>
        /// Ecommerce instance.
        /// </summary>
        private readonly IECommerceService eCommerceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="eCommerceService">Service.</param>
        public ProductsController(ILogger<ProductsController> logger, IECommerceService eCommerceService)
        {
            this.logger = logger;
            this.eCommerceService = eCommerceService;
        }

        /// <summary>
        /// Load all products.
        /// </summary>
        /// <param name="searchString">search string.</param>
        /// <param name="category">Category.</param>
        /// <returns>Products.</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string category)
        {
            this.ViewBag.SearchString = searchString;
            this.ViewBag.Category = category;
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = $"CONTAINS(e.Name,  '{searchString}')";
                if (!string.IsNullOrEmpty(category))
                {
                    searchString = $"{searchString} and CONTAINS(e.Category,  '{category}')";
                }
            }
            else if (!string.IsNullOrEmpty(category))
            {
                searchString = $"CONTAINS(e.Category,  '{category}')";
            }

            var products = await this.eCommerceService.GetProductsAsync(searchString).ConfigureAwait(false);
            return this.View(products);
        }

        /// <summary>
        /// Get product details.
        /// </summary>
        /// <param name="productId">Id.</param>
        /// <param name="productName">Name.</param>
        /// <returns>Product.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(string productId, string productName)
        {
            var product = await this.eCommerceService.GetProductByIdAsync(productId, productName).ConfigureAwait(false);
            return this.View(product);
        }

        /// <summary>
        /// Error handler.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <returns>Action result.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/Products/Error/{code:int}")]
        public IActionResult Error(int code)
        {
            if (code == 404)
            {
                return this.View("~/Views/Shared/NotFound.cshtml");
            }
            else
            {
                return this.View("~/Views/Shared/Error.cshtml", new ErrorViewModel { CorrelationId = Activity.Current?.RootId ?? this.HttpContext.TraceIdentifier });
            }
        }
    }
}
