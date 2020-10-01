// "//-----------------------------------------------------------------------".
// <copyright file="IProductRepository.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.DataStore.Contracts
{
    using Packt.Ecommerce.Data.Models;

    /// <summary>
    /// The product repository.
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product>
    {
    }
}
