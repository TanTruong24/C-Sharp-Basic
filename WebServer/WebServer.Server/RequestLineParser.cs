﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Server
{
    internal class RequestLineParser
    {
        public static bool TryParse(string line, out RequestLine? requestLine)
        {
            ArgumentNullException.ThrowIfNull(line);

            requestLine = default;

            if (line.StartsWith("GET"))
            {
                int idx = line.IndexOf(' ', 4);
                if (idx != -1)
                {
                    requestLine = new RequestLine();
                    requestLine.Method = SDK.WMethod.Get;
                    requestLine.Uri = line.Substring(4, idx - 4);
                    requestLine.Version = line.Substring(idx + 1);

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true; 
        }
    }
}
