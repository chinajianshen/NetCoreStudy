using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Transfer8Pro.CommunalWebAPI.Infrastructure.Results
{
    public class PlainTextResult : IHttpActionResult
    {
        private string text;
        private HttpRequestMessage request;

        public PlainTextResult(string text, HttpRequestMessage request)
        {
            this.text = text;
            this.request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage
            {
                Content = new StringContent(text, Encoding.Unicode),
                RequestMessage = request
            };
            return Task.FromResult(response);
        }
    }
}