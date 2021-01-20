using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Clients.WebController.uHTTP
{
    public static partial class uHTTP
    {
        public class Request
        {
            public string method { get; private set; }
            public string url { get; private set; }
            public Dictionary<string, string> headers {
                get; private set;
            }
            public string body { get; private set; }

            private Request(){
                headers = new Dictionary<string, string>();
                body = string.Empty;
            }

            public static Request TryParse(string str){
                var lines = str.Split(new string[]{ eol }, StringSplitOptions.None);

                if(!Regex.IsMatch(lines[0], "[GET|HEAD|POST|PUT|DELETE] /.* HTTP/1.1")){
                    return null;
                }

                var request = new Request
                {
                    method = lines[0].Split(' ')[0], 
                    url = lines[0].Split(' ')[1]
                };


                var isData = false;
                for(var i = 1; i < lines.Length; i++){
                    if(lines[i].Equals(string.Empty)){
                        isData = true;
                        continue;
                    }

                    if(isData){
                        request.body += (request.body.Equals(string.Empty) ? string.Empty : eol) + lines[i];
                    }
                    else{
                        string[] keyValuePair = lines[i].Split(new string[]{ ": " }, StringSplitOptions.None);                    
                        request.headers.Add(keyValuePair[0], keyValuePair[1]);
                    }
                }

                return request;
            }
        }
    }
}