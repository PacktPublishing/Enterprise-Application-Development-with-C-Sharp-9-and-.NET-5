// "//-----------------------------------------------------------------------".
// <copyright file="ProductsService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Product.Services
{
    using System;
    using System.Collections.Generic;
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
    using Packt.Ecommerce.Product.Contracts;

    /// <summary>
    /// Products service contract for CRUD operations.
    /// </summary>
    public class ProductsService : IProductService
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
        /// Initializes a new instance of the <see cref="ProductsService"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP Client factory.</param>
        /// <param name="applicationSettings">The application settings.</param>
        /// <param name="autoMapper">The mapper.</param>
        /// <param name="cacheService">cache service.</param>
        public ProductsService(IHttpClientFactory httpClientFactory, IOptions<ApplicationSettings> applicationSettings, IMapper autoMapper, IDistributedCacheService cacheService)
        {
            NotNullValidator.ThrowIfNull(applicationSettings, nameof(applicationSettings));
            IHttpClientFactory httpclientFactory = httpClientFactory;
            this.applicationSettings = applicationSettings;
            this.httpClient = httpclientFactory.CreateClient();
            this.autoMapper = autoMapper;
            this.cacheService = cacheService;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductListViewModel>> GetProductsAsync(string filterCriteria = null)
        {
            // cache aside pattern
            var products = await this.cacheService.GetCacheAsync<IEnumerable<Packt.Ecommerce.Data.Models.Product>>($"products{filterCriteria}").ConfigureAwait(false);

            if (products == null)
            {
                using var productRequest = new HttpRequestMessage(HttpMethod.Get, $"{this.applicationSettings.Value.DataStoreEndpoint}api/products?filterCriteria={filterCriteria}");
                var productResponse = await this.httpClient.SendAsync(productRequest).ConfigureAwait(false);

                if (!productResponse.IsSuccessStatusCode)
                {
                    await this.ThrowServiceToServiceErrors(productResponse).ConfigureAwait(false);
                }

                if (productResponse.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new List<ProductListViewModel>();
                }

                products = await productResponse.Content.ReadFromJsonAsync<IEnumerable<Packt.Ecommerce.Data.Models.Product>>().ConfigureAwait(false);
                await this.cacheService.AddOrUpdateCacheAsync<IEnumerable<Packt.Ecommerce.Data.Models.Product>>($"products{filterCriteria}", products).ConfigureAwait(false);
            }

            // Mapping
            var productList = this.autoMapper.Map<List<ProductListViewModel>>(products);
            return productList;
        }

        /// <inheritdoc/>
        public async Task<ProductDetailsViewModel> AddProductAsync(ProductDetailsViewModel product)
        {
            NotNullValidator.ThrowIfNull(product, nameof(product));
            product.CreatedDate = DateTime.UtcNow;
            using var productRequest = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, ContentType);
            var productResponse = await this.httpClient.PostAsync(new Uri($"{this.applicationSettings.Value.DataStoreEndpoint}api/products"), productRequest).ConfigureAwait(false);

            if (!productResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(productResponse).ConfigureAwait(false);
            }

            var createdProductDAO = await productResponse.Content.ReadFromJsonAsync<Packt.Ecommerce.Data.Models.Product>().ConfigureAwait(false);

            // clearning the cache
            await this.cacheService.RemoveCacheAsync("products").ConfigureAwait(false);

            // Mapping
            var createdProduct = this.autoMapper.Map<ProductDetailsViewModel>(createdProductDAO);
            return createdProduct;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> UpdateProductAsync(ProductDetailsViewModel product)
        {
            using var productRequest = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, ContentType);
            var productResponse = await this.httpClient.PutAsync(new Uri($"{this.applicationSettings.Value.DataStoreEndpoint}api/products"), productRequest).ConfigureAwait(false);
            if (!productResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(productResponse).ConfigureAwait(false);
            }

            // clearning the cache
            await this.cacheService.RemoveCacheAsync("products").ConfigureAwait(false);

            return productResponse;
        }

        /// <inheritdoc/>
        public async Task<ProductDetailsViewModel> GetProductByIdAsync(string productId, string productName)
        {
            using var productRequest = new HttpRequestMessage(HttpMethod.Get, $"{this.applicationSettings.Value.DataStoreEndpoint}api/products/{productId}?name={productName}");
            var productResponse = await this.httpClient.SendAsync(productRequest).ConfigureAwait(false);
            if (!productResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(productResponse).ConfigureAwait(false);
            }

            if (productResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                var productDAO = await productResponse.Content.ReadFromJsonAsync<Packt.Ecommerce.Data.Models.Product>().ConfigureAwait(false);

                // Mapping
                var product = this.autoMapper.Map<ProductDetailsViewModel>(productDAO);
                return product;
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DeleteProductAsync(string productId, string productName)
        {
            var productResponse = await this.httpClient.DeleteAsync(new Uri($"{this.applicationSettings.Value.DataStoreEndpoint}api/products/{productId}?name={productName}")).ConfigureAwait(false);

            if (!productResponse.IsSuccessStatusCode)
            {
                await this.ThrowServiceToServiceErrors(productResponse).ConfigureAwait(false);
            }

            // clearning the cache
            await this.cacheService.RemoveCacheAsync("products").ConfigureAwait(false);

            return productResponse;
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
