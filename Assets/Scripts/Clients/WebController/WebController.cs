using System.Text;
using Clients.WebController.WebServer;
using UnityEngine;
using UnityEngine.UI;
using uHTTP = Clients.WebController.WebServer.uHTTP.uHTTP;

namespace Clients.WebController
{
    public class WebController : MonoBehaviour
    {
        public RawWebImageLoader qrCode;
        public NetworkUtility networkUtility;
        public StaticWebServer webServer;

        public Text instructionText;
        public delegate string IncomingTcpMessageEventHandler(string message);
        
        [Header("POST call endpoint")]
        
        public int port = 1234;
        public bool startStopAutomatically = true;

        public event IncomingTcpMessageEventHandler onMessage;
        
        private uHTTP.Server server { get; set; }
        
        private void OnEnable(){
            if(server == null){
                server = new uHTTP.Server(port);
            }
            server.requestHandler = request => {
                if (!request.method.ToUpper().Equals("POST"))
                    return new uHTTP.Response(uHTTP.StatusCode.ERROR);

                var result = OnMessage(request.body);
                var response = new uHTTP.Response(uHTTP.StatusCode.OK)
                {
                    body = Encoding.UTF8.GetBytes(result)
                };
                response.headers.Add("Access-Control-Allow-Origin", "*");
                return response;
            };
            if(startStopAutomatically && !server.isRunning){
                server.Start();
            }
        }

        private void OnDisable(){
            if(startStopAutomatically && server.isRunning){
                server.Stop();
            }
        }

        private void OnDestroy(){
            if(server != null && server.isRunning){
                server.Stop();
            }
        }

        private void Start()
        {
            var url = "http://" + NetworkUtility.ip + (webServer.port.Equals(80) ? "" : (":" + webServer.port + "/"));
            qrCode.url = "https://api.qrserver.com/v1/create-qr-code/?format=png&size=500x500&margin=10&data=" + url;

            instructionText.text = "Scan the QR code or visit " + url + " to select a training";
            if(Application.isEditor) {
                // For testing in Editor
                Debug.Log("Please build the game to remote control it from your smartphone.\n");
                Debug.Log("Join a second player by visiting: " + url);
            }
        }

        protected virtual string OnMessage(string message)
        {
            return onMessage?.Invoke(message);
        }
    }
}