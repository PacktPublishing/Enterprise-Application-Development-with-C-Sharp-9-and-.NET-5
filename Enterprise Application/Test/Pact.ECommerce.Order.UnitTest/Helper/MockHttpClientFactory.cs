namespace Pact.ECommerce.Order.UnitTest.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class MockHttpClientFactory : IHttpClientFactory
    {
        public HttpResponseMessage ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        public IList<MockHttpMessageHandler> MessageHandlers = new List<MockHttpMessageHandler>();

        public HttpClient CreateClient(string name = default)
        {
            var messageHandler = new MockHttpMessageHandler(this.ResponseMessage);
            this.MessageHandlers.Add(messageHandler);
            return new HttpClient(messageHandler);
        }
    }

    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage responseMessage;
        public HttpRequestMessage Request;

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            this.responseMessage = response;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.Request = request;
            return await Task.FromResult(responseMessage);
        }
    }
}
