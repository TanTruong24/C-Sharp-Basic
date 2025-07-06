using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebServer.SDK;

namespace WebServer.Server.RequestReader
{
    internal class DefaultRequestReader: IRequestReader
    {
        private readonly ILogger<DefaultRequestReader> _logger;

        private readonly Socket _socket;

        public DefaultRequestReader(Socket socket, ILogger<DefaultRequestReader> logger) 
        {
            _logger = logger;
            _socket = socket;
        }
        public async Task<WRequest> ReadRequestAsync(CancellationToken cancellationToken)
        {
            // Đọc dòng đầu tiên (ví dụ: GET /index.html HTTP/1.1)
            // Parse dòng đầu bằng RequestLineParser.
            var stream = new NetworkStream(_socket);
            var reader = new StreamReader(stream, Encoding.ASCII);

            var requestBuilder = new WRequestBuilder();

            var requestLineString = await reader.ReadLineAsync(cancellationToken);
            _logger.LogInformation(requestLineString);

            if (requestLineString != null)
            {
                if (RequestLineParser.TryParse(requestLineString, out var requestLine) && requestLine != null)
                {
                    requestBuilder.HttpVersion = requestLine.Version;
                    requestBuilder.Url = requestLine.Uri;

                    var headerLine = await reader.ReadLineAsync(cancellationToken);
                    while (!string.IsNullOrEmpty(headerLine))
                    {
                        _logger.LogInformation(headerLine);

                        if (HeaderLine.TryParse(headerLine, out var header))
                        {
                            requestBuilder.AddHeader(header);
                        }

                        headerLine = await reader.ReadLineAsync(cancellationToken);


                    }
                }
            }

            return requestBuilder.Build();
        }
    }
}
