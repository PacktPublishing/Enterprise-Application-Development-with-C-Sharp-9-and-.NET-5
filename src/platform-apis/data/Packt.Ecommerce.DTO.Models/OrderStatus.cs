// "//-----------------------------------------------------------------------".
// <copyright file="OrderStatus.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.DTO.Models
{
    /// <summary>
    /// Order status.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Order status while in cart.
        /// </summary>
        Cart,

        /// <summary>
        /// Order status after purchase.s
        /// </summary>
        Submitted,
    }
}
