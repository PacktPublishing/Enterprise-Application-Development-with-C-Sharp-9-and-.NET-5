using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace EcommerceSample
{
    class Program
    {
        private static readonly string Uri = "YOUR URI HERE";
        private static readonly string PrimaryKey = "YOUR PRIMARY KEY HERE";
        static async Task Main(string[] args)
        {
            using (CosmosClient cosmosClient = new CosmosClient(Uri, PrimaryKey))
            {
                DatabaseResponse createDatabaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync("ECommerce");
                Database database = createDatabaseResponse.Database;

                var containerProperties = new ContainerProperties("Products", "/Name");
                var createContainerResponse = await database.CreateContainerIfNotExistsAsync(containerProperties, 10000);
                var productContainer = createContainerResponse.Container;

                Product book = new Product()
                {
                    ProductId = "Book.1",
                    Category = "Books",
                    Price = 100,
                    Name = "Mastering enterprise application development Book",
                    Rating = new List<Rating>() { new Rating { Stars = 5, Percentage = 95 }, new Rating { Stars = 4, Percentage = 5 } },
                    Format = new List<string>() { "PDF", "Hard Cover" },
                    Authors = new List<string>() { "Rishabh Verma", "Neha Shrivastava", "Ravindra Akela", "Bhupesh Guptha" }
                };

                try
                {
                    // Check if item it exists.  
                    ItemResponse<Product> productBookResponse = await productContainer.ReadItemAsync<Product>(book.ProductId, new PartitionKey(book.Name));
                }
                catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ItemResponse<Product> productBookResponse = await productContainer.CreateItemAsync<Product>(book, new PartitionKey(book.Name));
                    Console.WriteLine($"Created item {productBookResponse.Resource.ProductId}");
                }
                string getAllProductsByBooksCAtegory = "SELECT * FROM p WHERE p.Category = 'Books'";
                QueryDefinition query = new QueryDefinition(getAllProductsByBooksCAtegory);
                FeedIterator<Product> iterator = productContainer.GetItemQueryIterator<Product>(query);

                while (iterator.HasMoreResults)
                {
                    FeedResponse<Product> result = await iterator.ReadNextAsync();
                    foreach (Product product in result)
                    {
                        Console.WriteLine($"Book retrived - {product.Name}");
                    }
                }
            }
        }
    }
}
