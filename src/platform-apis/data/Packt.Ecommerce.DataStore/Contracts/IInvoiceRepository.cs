// "//-----------------------------------------------------------------------".
// <copyright file="IInvoiceRepository.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.DataStore.Contracts
{
    using Packt.Ecommerce.Data.Models;

    /// <summary>
    /// The invoice repostiry.
    /// </summary>
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
    }
}
