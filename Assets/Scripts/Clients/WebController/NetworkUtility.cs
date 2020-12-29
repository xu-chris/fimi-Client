using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Clients.WebController
{
    public class NetworkUtility : MonoBehaviour {

        public bool autoUpdateFirewallRule = true;

        public string host {
            get {
                return Dns.GetHostEntry("").HostName;
            }
        }

        public static string ip {
            get {
                string ip = "unknown";
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach(IPAddress iPAddress in host.AddressList){
                    if(iPAddress.AddressFamily == AddressFamily	.InterNetwork){
                        ip = iPAddress.ToString();
                        break;
                    }
                }
                return ip;
            }
        }

        public bool isHttpSupported {
            get {
                return HttpListener.IsSupported;
            }
        }

        public bool isLan {
            get {
                return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
            }
        }

#if UNITY_STANDALONE_WIN
	void Awake() {
		if(Firewall.DoesAppRestrictionExist() && autoUpdateFirewallRule){
			Firewall.UpdateAppRule();
		}
	}
#endif
    }
}
