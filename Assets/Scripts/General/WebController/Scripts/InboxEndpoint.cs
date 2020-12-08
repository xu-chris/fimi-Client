using UnityEngine;
using UnityEngine.Events;

namespace General.WebController.Scripts
{
    public class InboxEndpoint : MonoBehaviour
    {
        public uHTTP.uHTTP.Server server { get; private set; }

        [System.Serializable]
        public class PostRequestHandler : UnityEvent<string, string> {}
        [Tooltip("Callback (with url and request body) for any incoming post request")]
        public PostRequestHandler postRequestHandler;

        public int port = 1234;
        public bool StartStopAutomatically = true;

        void OnEnable(){
            if(server == null){
                server = new uHTTP.uHTTP.Server(port);
            }
            server.requestHandler = (uHTTP.uHTTP.Request request) => {
                if(request.method.ToUpper().Equals("POST")) {
                    Dispatcher.Invoke(() => {
                        if(postRequestHandler != null) {
                            postRequestHandler.Invoke(request.url, request.body);
                        }
                    });
                    uHTTP.uHTTP.Response response = new uHTTP.uHTTP.Response(uHTTP.uHTTP.StatusCode.OK);
                    response.headers.Add("Access-Control-Allow-Origin", "*");
                    return response;
                }
                return new uHTTP.uHTTP.Response(uHTTP.uHTTP.StatusCode.ERROR);
            };
            if(StartStopAutomatically && !server.isRunning){
                server.Start();
            }
        }

        void OnDisable(){
            if(StartStopAutomatically && server.isRunning){
                server.Stop();
            }
        }

        void OnDestroy(){
            if(server != null && server.isRunning){
                server.Stop();
            }
        }
    }
}
