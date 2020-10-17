// "//-----------------------------------------------------------------------".
// <copyright file="InvoiceController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DataAccess.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Packt.Ecommerce.Data.Models;
    using Packt.Ecommerce.DataStore.Contracts;

    /// <summary>
    /// Invoice controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        /// <summary>
        /// The invoice repository.
        /// </summary>
        private readonly IInvoiceRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public InvoiceController(IInvoiceRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets all invoice.
        /// </summary>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns>Invoice.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllInvoiceAsync(string filterCriteria = null)
        {
            IEnumerable<Invoice> invoice;
            if (string.IsNullOrEmpty(filterCriteria))
            {
                invoice = await this.repository.GetAsync(string.Empty).ConfigureAwait(false);
            }
            else
            {
                invoice = await this.repository.GetAsync(filterCriteria).ConfigureAwait(false);
            }

            if (invoice.Any())
            {
                return this.Ok(invoice);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Gets the invoice by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Invoice.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetInvoiceById(string id)
        {
            Invoice result = await this.repository.GetByIdAsync(id, id).ConfigureAwait(false);
            if (result != null)
            {
                return this.Ok(result);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Create the invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateInvoiceAsync([FromBody] Invoice invoice)
        {
            if (invoice == null || invoice.Etag != null)
            {
                return this.BadRequest();
            }

            var result = await this.repository.AddAsync(invoice, invoice.Id).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetInvoiceById), new { id = result.Resource.Id }, result.Resource);
        }

        /// <summary>
        /// Updates the Invoice.
        /// </summary>
        /// <param name="invoice">The Invoice.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Invoice invoice)
        {
            if (invoice == null || invoice.Etag != null)
            {
                return this.BadRequest();
            }

            bool result = await this.repository.ModifyAsync(invoice, invoice.Etag, invoice.Id).ConfigureAwait(false);
            if (result)
            {
                return this.Accepted();
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Deletes the invoice.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            bool result = await this.repository.RemoveAsync(id, id).ConfigureAwait(false);
            if (result)
            {
                return this.Accepted();
            }
            else
            {
                return this.NoContent();
            }
        }
    }
}
