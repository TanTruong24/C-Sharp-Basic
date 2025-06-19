using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.SDK;

namespace WebServer.Server
{
    internal class WRequestBuilder
    {
        WMethod Method { get; set; } = WMethod.Get;

        public WRequest Build()
        {
            var request = new WRequest() { Method = Method };

            return request;
        }
    }
}
