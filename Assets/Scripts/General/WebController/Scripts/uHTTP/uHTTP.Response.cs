using System;
using System.Collections.Generic;
using System.Text;

namespace General.WebController.Scripts.uHTTP
{
    public static partial class uHTTP {

        public class Response
        {
            public Response Default {
                get {
                    Response response = new Response(StatusCode.OK);
                    response.headers.Add("Connection", "Closed");
                    response.headers.Add("Content-Type", "text/html");
                    response.body = Encoding.UTF8.GetBytes("Hello, &#181;HTTP!");
                    return response;
                }
            }

            public StatusCode statusCode
            {
                get; private set;
            }
            public Dictionary<string, string> headers {
                get; private set;
            }

            public byte[] body;

            public Response(StatusCode statusCode)
            {
                this.statusCode = statusCode;
                headers = new Dictionary<string, string>();
                body = new byte[]{};
            }

            public byte[] ToBinary(){
                var head = new StringBuilder(
                    $"HTTP/1.1 {statusCode.statusCode} {statusCode.description}"
                );
                foreach(var header in headers){
                    head.Append(eol);
                    head.Append(
                        $"{header.Key}: {header.Value}"
                    );
                }
                head.Append(eol);
                head.Append(eol);

                var headBytes = Encoding.UTF8.GetBytes(head.ToString());
                var bytes = new byte[headBytes.Length + body.Length];
                Array.Copy(headBytes, bytes, headBytes.Length);
                Array.Copy(body, 0, bytes, headBytes.Length, body.Length);
                return bytes;
            }
        }
    }
}