// "//-----------------------------------------------------------------------".
// <copyright file="RepositoryExtensions.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.DataAccess.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Packt.Ecommerce.DataStore;
    using Packt.Ecommerce.DataStore.Contracts;

    /// <summary>
    /// Injects the data store repositories.
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Injects the data store repositories.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <returns>Scoped Services.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
