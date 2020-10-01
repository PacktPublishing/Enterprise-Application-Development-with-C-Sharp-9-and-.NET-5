// "//-----------------------------------------------------------------------".
// <copyright file="IOrderRepository.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DataStore.Contracts
{
    using Packt.Ecommerce.Data.Models;

    /// <summary>
    /// The order repository.
    /// </summary>
    public interface IOrderRepository : IBaseRepository<Order>
    {
    }
}
