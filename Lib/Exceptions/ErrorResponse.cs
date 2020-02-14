using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace Lib.Exceptions
{
    public class ErrorResponse
    {
        public ErrorResponse(string content, HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = HttpStatusCode;

            if (!String.IsNullOrWhiteSpace(content))
            {
                JArray response = JArray.Parse(content);

                Id = (string)response[0]["Code"];
                Message = (string)response[0]["Message"];
            }
        }

        public string Id { get;  set; }
        public string Message { get;  set; }
        public HttpStatusCode HttpStatusCode { get;  set; }
    }
}
