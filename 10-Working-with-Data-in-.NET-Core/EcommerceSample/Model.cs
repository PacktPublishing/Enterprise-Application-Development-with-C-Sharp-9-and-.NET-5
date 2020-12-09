using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EcommerceSample
{
    public class Rating
    {
        public int Stars { get; set; }
        public int Percentage { get; set; }
    }

    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<Rating> Rating { get; set; }
        public List<string> Format { get; set; }
        public List<string> Authors { get; set; }
        public List<int> Size { get; set; }
        public List<string> Color { get; set; }
    }
}