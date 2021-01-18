using System;
using System.Collections;
using General.Authentication;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using IBM.Watson.TextToSpeech.V1;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Clients.TTSClient
{
    public class TTSClient : MonoBehaviour
    {
        
        public TextAsset secretsConfigurationFile;
        private SecretsConfiguration secretsConfiguration;
        
        private TextToSpeechService service;
        private string allisionVoice = "en-US_AllisonV3Voice";
        private string synthesizeText = "Hello, welcome to the Watson Unity SDK!";
        private string placeholderText = "Please type text here and press enter.";
        private string waitingText = "Watson Text to Speech service is synthesizing the audio!";
        private string synthesizeMimeType = "audio/wav";
        private bool _textEntered = false;
        private AudioClip _recording = null;
        private byte[] audioStream = null;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LogSystem.InstallDefaultReactors();
            secretsConfiguration = new AuthenticationConfigurationService(secretsConfigurationFile).configuration;
            InitializeService(secretsConfiguration.ibm);
        }

        public void InitializeService(IbmConfiguration ibmConfiguration)
        {
            Runnable.Run(CreateService(ibmConfiguration));
        }

        public void Synthesize(string text)
        {
            Runnable.Run(AsyncSynthesize(text));
        }

        private IEnumerator CreateService(IbmConfiguration ibmConfiguration)
        {
            if (string.IsNullOrEmpty(ibmConfiguration.IamApiKey))
            {
                throw new IBMException("Please add IAM ApiKey to the Iam Apikey field in the inspector.");
            }

            var authenticator = new IamAuthenticator(apikey: ibmConfiguration.IamApiKey);

            while (!authenticator.CanAuthenticate())
            {
                yield return null;
            }

            service = new TextToSpeechService(authenticator);
            if (!string.IsNullOrEmpty(ibmConfiguration.ServiceUrl))
            {
                service.SetServiceUrl(ibmConfiguration.ServiceUrl);
            }
        }

        #region Synthesize
        private IEnumerator AsyncSynthesize(string text)
        {
            yield return new WaitUntil(() => service != null);
            byte[] synthesizeResponse = null;
            AudioClip clip = null;
            service.Synthesize(
                callback: (DetailedResponse<byte[]> response, IBMError error) =>
                {
                    synthesizeResponse = response.Result;
                    Log.Debug("ExampleTextToSpeechV1", "Synthesize done!");
                    clip = WaveFile.ParseWAV("myClip", synthesizeResponse);
                    PlayClip(clip);
                },
                text: text,
                voice: allisionVoice,
                accept: synthesizeMimeType
            );

            while (synthesizeResponse == null)
                yield return null;

            yield return new WaitForSeconds(clip.length);
        }
        #endregion

        #region PlayClip
        private static void PlayClip(AudioClip clip)
        {
            if (!Application.isPlaying || clip == null) return;
            
            var audioObject = new GameObject("AudioObject");
            var source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();

            Object.Destroy(audioObject, clip.length);
        }
        #endregion
    }
}