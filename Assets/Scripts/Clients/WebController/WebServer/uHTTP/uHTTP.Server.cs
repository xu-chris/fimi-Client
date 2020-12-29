using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Clients.WebController.WebServer.uHTTP
{
    public static partial class uHTTP 
    {
        public class Server
        {
            private TcpListener listener;
            public RequestHandler requestHandler;

            public delegate Response RequestHandler(Request request);

            private object monitor = new object();
            private bool isServerRunning;
            public bool isRunning {
                get{
                    lock(monitor){
                        return isServerRunning;
                    }
                }
                private set{
                    lock(monitor){
                        isServerRunning = value;
                    }
                }
            }
        
            public Server(int port = 80){
                listener = new TcpListener(IPAddress.Any, port);
            }

            public void Start(){
                lock(monitor){
                    if(isRunning){
                        throw new Exception("Server already running!");
                    }
                    isRunning = true;
                }

                listener.Start();
                new Thread(() => {
                    while(true){
                        try {
                            TcpClient client = listener.AcceptTcpClient();                    
                            new Thread(() => {
                                HandleConnection(client);
                                client.Close();
                            }).Start();
                        }
                        catch(SocketException){
                            break;
                        }
                    }
                }).Start();
            }

            public void Stop(){
                lock(monitor){
                    if(!isRunning){
                        throw new Exception("Server not running!");
                    }
                    listener.Stop();
                    isRunning = false;
                }
            }

            private void HandleConnection(TcpClient client){
                var stream = client.GetStream();

                while(!stream.DataAvailable){
                    Thread.Sleep(20);
                }

                var received = new byte[client.Available];
                stream.Read(received, 0, received.Length);

                var request = Request.TryParse(Encoding.UTF8.GetString(received));
                if(request == null){
                    return; // Not HTTP
                }
                else if(requestHandler != null){
                    var response = requestHandler(request);
                    var bytes = response.ToBinary();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
            }
        }
    }
}