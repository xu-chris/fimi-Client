using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Clients.WebController
{
    [RequireComponent(typeof(RawImage))]
    public class RawWebImageLoader : MonoBehaviour
    {
        public FilterMode filterMode;
        public TextureFormat textureFormat = TextureFormat.BGRA32;
        public bool mipChain;


        public string url {
            set {
                StopAllCoroutines();
                StartCoroutine(LoadAndApplyTexture(value));
            }
        }


        private IEnumerator LoadAndApplyTexture(string url)
        {
            var www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) yield break;
            var texture = new Texture2D(0, 0, textureFormat, mipChain) {filterMode = filterMode};

            texture.LoadImage(www.downloadHandler.data);
            GetComponent<RawImage>().texture = texture;
        }
    }
}
