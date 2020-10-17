// "//-----------------------------------------------------------------------".
// <copyright file="EntitySerializer.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Caching
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Packt.Ecommerce.Caching.Interfaces;
    using Packt.Ecommerce.Common.Validator;

    /// <summary>
    /// JSON serializer implementation.
    /// </summary>
    /// <seealso cref="IEntitySerializer" />
    [ExcludeFromCodeCoverage]
    public class EntitySerializer : IEntitySerializer
    {
        /// <inheritdoc/>
        public async Task<T> DeserializeEntityAsync<T>(byte[] entity, CancellationToken cancellationToken = default)
        {
            NotNullValidator.ThrowIfNull(entity, nameof(entity));

            using MemoryStream memoryStream = new MemoryStream(entity);
            var value = await JsonSerializer.DeserializeAsync<T>(memoryStream, cancellationToken: cancellationToken);
            return value;
        }

        /// <inheritdoc/>
        public async Task<byte[]> SerializeEntityAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            using MemoryStream memoryStream = new MemoryStream();
            await JsonSerializer.SerializeAsync<T>(memoryStream, entity, cancellationToken: cancellationToken).ConfigureAwait(false);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream.ToArray();
        }
    }
}
