// "//-----------------------------------------------------------------------".
// <copyright file="InvoiceController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Invoice.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Packt.Ecommerce.DTO.Models;
    using Packt.Ecommerce.Invoice.Contracts;

    /// <summary>
    /// The invoice controller.
    /// </summary>
    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        /// <summary>
        /// The Invoice service.
        /// </summary>
        private readonly IInvoiceService invoiceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceController"/> class.
        /// </summary>
        /// <param name="invoiceService">The Invoice service.</param>
        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        /// <summary>
        /// Gets the Invoice by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Invoice.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetInvoiceById(string id)
        {
            var invoice = await this.invoiceService.GetInvoiceByIdAsync(id).ConfigureAwait(false);
            if (invoice != null)
            {
                return this.Ok(invoice);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Adds the invoice asynchronously.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>Invoice.</returns>
        [HttpPost]
        public async Task<IActionResult> AddInvoiceAsync(InvoiceDetailsViewModel invoice)
        {
            // Invoice null check is to avoid null attribute validation error.
            if (invoice == null || invoice.Etag != null)
            {
                return this.BadRequest();
            }

            var result = await this.invoiceService.AddInvoiceAsync(invoice).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetInvoiceById), new { id = result.Id }, result); // HATEOS principle
        }
    }
}
