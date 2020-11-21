// "//-----------------------------------------------------------------------".
// <copyright file="ECommerceService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Packt.Ecommerce.Common.Models;
    using Packt.Ecommerce.Common.Options;
    using Packt.Ecommerce.Common.Validator;
    using Packt.Ecommerce.DTO.Models;
    using Packt.Ecommerce.Web.Contracts;

    /// <summary>
    /// Common service interacting with backend.
    /// </summary>
    public class ECommerceService : IECommerceService
    {
        /// <summary>
        /// Content Type.
        /// </summary>
        private const string ContentType = "application/json";

        /// <summary>
        /// The HTTP client.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Application settings.
        /// </summary>
        private readonly ApplicationSettings applicationSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECommerceService"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="applicationSettings">The application settings.</param>
        public ECommerceService(IHttpClientFactory httpClientFactory, IOptions<ApplicationSettings> applicationSettings)
        {
            NotNullValidator.ThrowIfNull(applicationSettings, nameof(applicationSettings));
            IHttpClientFactory httpclientFactory = httpClientFactory;
            this.httpClient = httpclientFactory.CreateClient();
            this.applicationSettings = applicationSettings.Value;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductListViewModel>> GetProductsAsync(string filterCriteria = null)
        {
            IEnumerable<ProductListViewModel> products = new List<ProductListViewModel>();
            using var productRequest = new HttpRequestMessage(HttpMethod.Get, $"{this.applicationSettings.ProductsApiEndpoint}?filterCriteria={filterCriteria}");
            var productResponse = await this.httpClient.SendAsync(productRequest).ConfigureAwait(false);

            if (!productResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(productResponse).ConfigureAwait(false);
            }

            if (productResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                products = await productResponse.Content.ReadFromJsonAsync<IEnumerable<ProductListViewModel>>().ConfigureAwait(false);
            }

#pragma warning disable S125

            // To Simulate an error uncomment below line
            // if (products.Any())
            // Sections of code should not be commented out
            // {
            //    throw new InvalidOperationException();
            // }
#pragma warning restore S125 // Sections of code should not be commented out
            return products;
        }

        /// <inheritdoc/>
        public async Task<ProductDetailsViewModel> GetProductByIdAsync(string productId, string productName)
        {
            ProductDetailsViewModel product = new ProductDetailsViewModel();
            using var productRequest = new HttpRequestMessage(HttpMethod.Get, $"{this.applicationSettings.ProductsApiEndpoint}{productId}?name={productName}");
            var productResponse = await this.httpClient.SendAsync(productRequest).ConfigureAwait(false);
            if (!productResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(productResponse).ConfigureAwait(false);
            }

            if (productResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                product = await productResponse.Content.ReadFromJsonAsync<ProductDetailsViewModel>().ConfigureAwait(false);
            }

            return product;
        }

        /// <inheritdoc/>
        public async Task<OrderDetailsViewModel> CreateOrUpdateOrder(OrderDetailsViewModel order)
        {
            NotNullValidator.ThrowIfNull(order, nameof(order));
            using var orderRequest = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, ContentType);
            var orderResponse = await this.httpClient.PostAsync(new Uri($"{this.applicationSettings.OrdersApiEndpoint}"), orderRequest).ConfigureAwait(false);

            if (!orderResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(orderResponse).ConfigureAwait(false);
            }

            var createdOrder = await orderResponse.Content.ReadFromJsonAsync<OrderDetailsViewModel>().ConfigureAwait(false);

            return createdOrder;
        }

        /// <inheritdoc/>
        public async Task<OrderDetailsViewModel> GetOrderByIdAsync(string orderId)
        {
            OrderDetailsViewModel order = new OrderDetailsViewModel();
            using var orderRequest = new HttpRequestMessage(HttpMethod.Get, $"{this.applicationSettings.OrdersApiEndpoint}{orderId}");
            var productResponse = await this.httpClient.SendAsync(orderRequest).ConfigureAwait(false);
            if (!productResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(productResponse).ConfigureAwait(false);
            }

            if (productResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                order = await productResponse.Content.ReadFromJsonAsync<OrderDetailsViewModel>().ConfigureAwait(false);
            }

            return order;
        }

        /// <inheritdoc/>
        public async Task<InvoiceDetailsViewModel> GetInvoiceByIdAsync(string invoiceId)
        {
            InvoiceDetailsViewModel invoice = new InvoiceDetailsViewModel();
            using var invoiceRequest = new HttpRequestMessage(HttpMethod.Get, $"{this.applicationSettings.InvoiceApiEndpoint}{invoiceId}");
            var invoiceResponse = await this.httpClient.SendAsync(invoiceRequest).ConfigureAwait(false);
            if (!invoiceResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(invoiceResponse).ConfigureAwait(false);
            }

            if (invoiceResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                invoice = await invoiceResponse.Content.ReadFromJsonAsync<InvoiceDetailsViewModel>().ConfigureAwait(false);
            }

            return invoice;
        }

        /// <inheritdoc/>
        public async Task<InvoiceDetailsViewModel> SubmitOrder(OrderDetailsViewModel order)
        {
            // Update Order Status
            order.OrderStatus = OrderStatus.Submitted.ToString();
            await this.CreateOrUpdateOrder(order).ConfigureAwait(false);

            // Create invoice
            InvoiceDetailsViewModel invoice = new InvoiceDetailsViewModel()
            {
                OrderId = order.Id,
                PaymentMode = order.PaymentMode,
                Products = order.Products,
                ShippingAddress = order.ShippingAddress,
            };
            using var invoiceRequest = new StringContent(JsonSerializer.Serialize(invoice), Encoding.UTF8, ContentType);
            var invoiceResponse = await this.httpClient.PostAsync(new Uri($"{this.applicationSettings.InvoiceApiEndpoint}"), invoiceRequest).ConfigureAwait(false);
            if (!invoiceResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(invoiceResponse).ConfigureAwait(false);
            }

            invoice = await invoiceResponse.Content.ReadFromJsonAsync<InvoiceDetailsViewModel>().ConfigureAwait(false);

            return invoice;
        }

        /// <summary>
        /// Throws the service to service errors.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>The task.</returns>
        private async Task ThrowServiceToServiceErrors(HttpResponseMessage response)
        {
            var exceptionReponse = await response.Content.ReadFromJsonAsync<ExceptionResponse>().ConfigureAwait(false);
            throw new Exception(exceptionReponse.InnerException);
        }
    }
}