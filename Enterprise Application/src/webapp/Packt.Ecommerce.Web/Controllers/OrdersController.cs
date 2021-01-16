// "//-----------------------------------------------------------------------".
// <copyright file="OrdersController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.ApplicationInsights;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Packt.Ecommerce.DTO.Models;
    using Packt.Ecommerce.Web.Contracts;

    /// <summary>
    /// Orders controller.
    /// </summary>
    public class OrdersController : Controller
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private readonly ILogger<OrdersController> logger;

        /// <summary>
        /// Ecommerce instance.
        /// </summary>
        private readonly IECommerceService eCommerceService;

        /// <summary>
        /// Telemetry client.
        /// </summary>
        private readonly TelemetryClient telemetry;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="eCommerceService">Service.</param>
        /// <param name="telemetry">Telemetry Client.</param>
        public OrdersController(ILogger<OrdersController> logger, IECommerceService eCommerceService, TelemetryClient telemetry)
        {
            this.logger = logger;
            this.eCommerceService = eCommerceService;
            this.telemetry = telemetry;
        }

        /// <summary>
        /// Submit and order and generate invoice.
        /// </summary>
        /// <param name="order">Order.</param>
        /// <returns>Invoice id.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDetailsViewModel order)
        {
            this.telemetry.TrackEvent("Create Order");
            if (this.ModelState.IsValid)
            {
                InvoiceDetailsViewModel invoice;
                invoice = await this.eCommerceService.SubmitOrder(order).ConfigureAwait(false);
                return this.RedirectToAction("Index", new { invoiceId = invoice.Id });
            }

            return this.RedirectToAction("Index", "Cart", new { orderId = order.Id });
        }

        /// <summary>
        /// Load the invoice.
        /// </summary>
        /// <param name="invoiceId">Invoice id.</param>
        /// <returns>Invoice.</returns>
        public async Task<IActionResult> Index(string invoiceId)
        {
            var invoice = await this.eCommerceService.GetInvoiceByIdAsync(invoiceId).ConfigureAwait(false);
            if (invoice != null)
            {
                return this.View(invoice);
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}
