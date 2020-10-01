// "//-----------------------------------------------------------------------".
// <copyright file="InvoiceService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Invoice.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Extensions.Options;
    using Packt.Ecommerce.Caching.Interfaces;
    using Packt.Ecommerce.Common.Models;
    using Packt.Ecommerce.Common.Options;
    using Packt.Ecommerce.Common.Validator;
    using Packt.Ecommerce.DTO.Models;
    using Packt.Ecommerce.Invoice.Contracts;

    /// <summary>
    /// The invoice service.
    /// </summary>
    public class InvoiceService : IInvoiceService
    {
        /// <summary>
        /// Content Type.
        /// </summary>
        private const string ContentType = "application/json";

        /// <summary>
        /// The application settings.
        /// </summary>
        private readonly IOptions<ApplicationSettings> applicationSettings;

        /// <summary>
        /// The HTTP client.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper autoMapper;

        /// <summary>
        /// The cache service.
        /// </summary>
        private readonly IDistributedCacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceService"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP Client factory.</param>
        /// <param name="applicationSettings">The application settings.</param>
        /// <param name="autoMapper">The mapper.</param>
        /// <param name="cacheService">cache service.</param>
        public InvoiceService(IHttpClientFactory httpClientFactory, IOptions<ApplicationSettings> applicationSettings, IMapper autoMapper, IDistributedCacheService cacheService)
        {
            NotNullValidator.ThrowIfNull(applicationSettings, nameof(applicationSettings));
            IHttpClientFactory httpclientFactory = httpClientFactory;
            this.applicationSettings = applicationSettings;
            this.httpClient = httpclientFactory.CreateClient();
            this.autoMapper = autoMapper;
            this.cacheService = cacheService;
        }

        /// <inheritdoc/>
        public async Task<InvoiceDetailsViewModel> AddInvoiceAsync(InvoiceDetailsViewModel invoice)
        {
            NotNullValidator.ThrowIfNull(invoice, nameof(invoice));
            invoice.Id = Guid.NewGuid().ToString();
            invoice.SoldBy = new SoldByViewModel()
            {
                Email = "Packt@Packt.com",
                Phone = "9876543210",
                SellerName = "Packt",
            };
            using var invoiceRequest = new StringContent(JsonSerializer.Serialize(invoice), Encoding.UTF8, ContentType);
            var invoiceResponse = await this.httpClient.PostAsync(new Uri($"{this.applicationSettings.Value.DataStoreEndpoint}api/invoice"), invoiceRequest).ConfigureAwait(false);

            if (!invoiceResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(invoiceResponse).ConfigureAwait(false);
            }

            var createdInvoiceDAO = await invoiceResponse.Content.ReadFromJsonAsync<Packt.Ecommerce.Data.Models.Invoice>().ConfigureAwait(false);

            // Mapping
            var createdInvoice = this.autoMapper.Map<InvoiceDetailsViewModel>(createdInvoiceDAO);
            return createdInvoice;
        }

        /// <inheritdoc/>
        public async Task<InvoiceDetailsViewModel> GetInvoiceByIdAsync(string invoiceId)
        {
            using var invoiceRequest = new HttpRequestMessage(HttpMethod.Get, $"{this.applicationSettings.Value.DataStoreEndpoint}api/invoice/{invoiceId}");
            var invoiceResponse = await this.httpClient.SendAsync(invoiceRequest).ConfigureAwait(false);
            if (!invoiceResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(invoiceResponse).ConfigureAwait(false);
            }

            if (invoiceResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                var invoiceDAO = await invoiceResponse.Content.ReadFromJsonAsync<Packt.Ecommerce.Data.Models.Invoice>().ConfigureAwait(false);

                // Mapping
                var invoice = this.autoMapper.Map<InvoiceDetailsViewModel>(invoiceDAO);
                return invoice;
            }
            else
            {
                return null;
            }
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
