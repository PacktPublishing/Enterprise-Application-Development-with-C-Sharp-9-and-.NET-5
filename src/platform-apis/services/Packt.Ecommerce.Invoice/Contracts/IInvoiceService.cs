// "//-----------------------------------------------------------------------".
// <copyright file="IInvoiceService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Invoice.Contracts
{
    using System.Threading.Tasks;
    using Packt.Ecommerce.DTO.Models;

    /// <summary>
    /// Invoice service contract for CRUD operations.
    /// </summary>
    public interface IInvoiceService
    {
        /// <summary>
        /// Gets the invoice by identifier asynchronously.
        /// </summary>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns>The invoice.</returns>
        Task<InvoiceDetailsViewModel> GetInvoiceByIdAsync(string invoiceId);

        /// <summary>
        /// Adds the invoice asynchronously.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>Invoice.</returns>
        Task<InvoiceDetailsViewModel> AddInvoiceAsync(InvoiceDetailsViewModel invoice);
    }
}
