using System.IO;
using UnityEngine;

namespace Clients.WebController
{
    public class WebController : MonoBehaviour
    {
        private uHTTP.uHTTP.Server server { get; set; }
        
        [Tooltip("Relative to the StreamingAssets")] 
        public string rootFolder;
        [Tooltip("Relative to the Root Folder")] 
        public string page404;
        public int port = 8080;

        private NetworkUtility networkUtility;

        private string streamingAssetsPath;
        
        public delegate uHTTP.uHTTP.Response IncomingTcpMessageEventHandler(string message);
        
        [Header("POST call endpoint")]
        
        public bool startStopAutomatically = true;

        public event IncomingTcpMessageEventHandler onMessage;

        private string url;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            networkUtility = GetComponent<NetworkUtility>();
            streamingAssetsPath = Application.streamingAssetsPath;
        }
        

        private void OnEnable(){
            if(server == null){
                server = new uHTTP.uHTTP.Server(port);
            }
            server.requestHandler = OnRequest;
            if(startStopAutomatically && !server.isRunning){
                server.Start();
            }
            
            url = "http://" + NetworkUtility.ip + (port.Equals(80) ? "" : (":" + port + "/"));
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
        
        public string GetIndexFile(string folder){
            foreach(FileInfo file in new DirectoryInfo(folder).GetFiles()){
                if(file.Name.ToLower().Split('.')[0].Equals("index")){
                    return file.Name;
                }
            }
            return null;
        }
        
        private string root {
            get {
                string root = streamingAssetsPath;
                root += (string.IsNullOrEmpty(rootFolder) ? string.Empty : "/") + rootFolder;
                if(root.EndsWith("/")){
                    root = root.Substring(0, root.Length -1);
                }
                return root;
            }
        }
        
        public uHTTP.uHTTP.Response OnRequest(uHTTP.uHTTP.Request request)
        {
            if(request.method.ToUpper().Equals("GET")){
                var file = root + request.url.Split('?')[0];
                if(file.EndsWith("/")){
                    file += GetIndexFile(file);
                }
                if(file == null || !File.Exists(file)){
                    return CreateResponse(uHTTP.uHTTP.StatusCode.NOT_FOUND, root + '/' + page404);
                }
                return CreateResponse(uHTTP.uHTTP.StatusCode.OK, file);  
            }

            if (request.method.ToUpper().Equals("POST"))
            {
                var response = OnMessage(request.body);
                response.headers.Add("Access-Control-Allow-Origin", "*");
                return response;
            }

            return new uHTTP.uHTTP.Response(uHTTP.uHTTP.StatusCode.ERROR);
        }

        protected virtual uHTTP.uHTTP.Response OnMessage(string message)
        {
            return onMessage?.Invoke(message);
        }
        
        private uHTTP.uHTTP.Response CreateResponse(uHTTP.uHTTP.StatusCode statusCode, string file){
            var response = new uHTTP.uHTTP.Response(statusCode);
            response.headers.Add("Connection", "Closed");
            if(!string.IsNullOrEmpty(file)){
                response.body = File.ReadAllBytes(file);
            }
            return response;
        }

        public string GetUrl()
        {
            return url;
        }
    }
}