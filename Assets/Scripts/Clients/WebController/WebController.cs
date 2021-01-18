using System;
using System.Text;
using Clients.WebController.WebServer;
using UnityEngine;
using UnityEngine.UI;
using uHTTP = Clients.WebController.WebServer.uHTTP.uHTTP;

namespace Clients.WebController
{
    public class WebController : MonoBehaviour
    {
        private NetworkUtility networkUtility;
        private StaticWebServer webServer;
        
        public delegate string IncomingTcpMessageEventHandler(string message);
        
        [Header("POST call endpoint")]
        
        public int port = 1234;
        public bool startStopAutomatically = true;

        public event IncomingTcpMessageEventHandler onMessage;
        
        private uHTTP.Server server { get; set; }

        private string url;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            networkUtility = GetComponent<NetworkUtility>();
            webServer = GetComponent<StaticWebServer>();
        }
        

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
            
            url = "http://" + NetworkUtility.ip + (webServer.port.Equals(80) ? "" : (":" + webServer.port + "/"));
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

            // For testing in Editor
            if(Application.isEditor) {
                Debug.Log("Join by visiting: " + url);
            }
        }

        protected virtual string OnMessage(string message)
        {
            return onMessage?.Invoke(message);
        }

        public string GetUrl()
        {
            return url;
        }
    }
}