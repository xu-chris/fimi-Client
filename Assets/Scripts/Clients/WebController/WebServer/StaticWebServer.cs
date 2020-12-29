using System.IO;
using UnityEngine;

namespace Clients.WebController.WebServer
{
    public class StaticWebServer : MonoBehaviour {
        private uHTTP.uHTTP.Server server { get; set; }

        [Tooltip("Relative to the StreamingAssets")] public string rootFolder;
        [Tooltip("Relative to the Root Folder")] public string page404;
        public int port = 8080;
        public bool startStopAutomatically = true;

        private uHTTP.uHTTP.Response CreateResponse(uHTTP.uHTTP.StatusCode statusCode, string file){
            uHTTP.uHTTP.Response response = new uHTTP.uHTTP.Response(statusCode);
            response.headers.Add("Connection", "Closed");
            if(!string.IsNullOrEmpty(file)){
                response.body = File.ReadAllBytes(file);
            }
            return response;
        }

        void OnEnable(){
            if(server == null){
                server = new uHTTP.uHTTP.Server(port);
            }
            server.requestHandler = OnRequest;
            if(startStopAutomatically && !server.isRunning){
                server.Start();
            }
        }

        void OnDisable(){
            if(startStopAutomatically && server.isRunning){
                server.Stop();
            }
        }

        void OnDestroy(){
            if(server != null && server.isRunning){
                server.Stop();
            }
        }

        void Awake(){
            streamingAssetsPath = Application.streamingAssetsPath;
        }

        private string streamingAssetsPath;

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

        public string GetIndexFile(string folder){
            foreach(FileInfo file in new DirectoryInfo(folder).GetFiles()){
                if(file.Name.ToLower().Split('.')[0].Equals("index")){
                    return file.Name;
                }
            }
            return null;
        }

        public uHTTP.uHTTP.Response OnRequest(uHTTP.uHTTP.Request request){
            if(!request.method.ToUpper().Equals("GET")){
                return new uHTTP.uHTTP.Response(uHTTP.uHTTP.StatusCode.ERROR);
            }
            string file = root + request.url.Split('?')[0];
            if(file.EndsWith("/")){
                file += GetIndexFile(file);
            }
            if(file == null || !File.Exists(file)){
                return CreateResponse(uHTTP.uHTTP.StatusCode.NOT_FOUND, root + '/' + page404);
            }
            return CreateResponse(uHTTP.uHTTP.StatusCode.OK, file);
        }
    }
}
