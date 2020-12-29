﻿using System;
using System.Collections;
using System.Collections.Generic;
using General;
using General.Skeleton;
using IBM.Cloud.SDK.Plugins.WebSocketSharp;
using JetBrains.Annotations;
using UnityEngine;

namespace Clients.WebSocketClient
{
    public class WebSocketClient : MonoBehaviour
    {
        private readonly bool enableLogging = false;
        public Person[] detectedPersons = { };
        public string serverUrl = "ws://localhost:8080/";

        private bool isWsConnected;

        private string message;
        private bool reconnecting;
        private WebSocket webSocket;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            Application.runInBackground = true;
        }

        public void Start()
        {
            StartCheckAndReconnectLifeCycle();
        }

        public void Update()
        {
            detectedPersons = DecodeMessageData(message);
        }

        public void OnApplicationQuit()
        {
            StopCoroutine(CheckAndReconnect());

            if (webSocket != null && webSocket.ReadyState == WebSocketState.Open)
                webSocket.Close();
        }

        public IEnumerable<Person> GetDecodedMessage()
        {
            return DecodeMessageData(message);
        }

        /**
 * Method to limit number of coroutines for reconnecting cycle to just one.
 */
        private void StartCheckAndReconnectLifeCycle()
        {
            if (!reconnecting)
            {
                StartCoroutine(CheckAndReconnect());
                reconnecting = true;
            }
        }

        private IEnumerator CheckAndReconnect()
        {
            while (!isWsConnected)
            {
                Debug.Log("Not connected to server. Will try to connect now.");
                yield return new WaitForSeconds(1);
                if (webSocket == null)
                    ConnectToWebSocketServer();
                else
                    webSocket.Connect();
            }

            reconnecting = false;
        }

        private void ConnectToWebSocketServer()
        {
            if (isWsConnected)
                return;

            using (webSocket = new WebSocket(serverUrl))
            {
                if (enableLogging)
                {
                    webSocket.Log.Level = LogLevel.Trace;
                    webSocket.Log.File = "./ws_log.txt";
                }

                webSocket.OnOpen += (sender, e) =>
                {
                    webSocket.Send("Hello server.");
                    Debug.Log("Connection opened.");
                    isWsConnected = true;
                };
                webSocket.OnMessage += (sender, e) => { message = e.Data; };
                webSocket.OnClose += (sender, e) =>
                {
                    isWsConnected = false;
                    StartCheckAndReconnectLifeCycle();
                };
                webSocket.OnError += (sender, e) =>
                {
                    isWsConnected = false;
                    StartCheckAndReconnectLifeCycle();
                };

                webSocket.Connect();
            }
        }

        public void SendRescaleSkeletons()
        {
            webSocket.Send("rescaleSkeletons");
            Debug.Log("Sent rescaleSkeleton message");
        }

        [CanBeNull]
        private Person[] DecodeMessageData(string message)
        {
            if (string.IsNullOrEmpty(message))
                return null;

            // Parse message
            const int parseOffset = 0; // No offset for real-time system
            var tokens = message.Split(',');

            var maxNumberOfJoints = 22;

            if ((tokens.Length - parseOffset) / 3 % maxNumberOfJoints != 0)
            {
                Debug.Log(
                    "Number of tokens cannot be parsed. Inconsistency between number of tokens and 3D vectors detected.");
                return null;
            }

            var currentNumPeople = (tokens.Length - parseOffset) / 3 / maxNumberOfJoints;
            Debug.Log("Detected " + (tokens.Length - parseOffset) / 3 + " joints. Number of detected people " +
                      currentNumPeople);

            var newDetection = new Person[currentNumPeople];

            for (var p = 0; p < currentNumPeople; ++p)
            {
                newDetection[p].joints = new Vector3[maxNumberOfJoints];
                newDetection[p].id = p;
                var lowestY = 999.0f;
                for (var i = 0; i < maxNumberOfJoints - 1; ++i)
                {
                    var tokenIndex = 3 * p * maxNumberOfJoints + 3 * i + 3;
                    newDetection[p].joints[i].x =
                        float.Parse(tokens[tokenIndex + 0]) * 0.001f; // Can be flipped here for mirroring
                    newDetection[p].joints[i].y = float.Parse(tokens[tokenIndex + 1]) * 0.001f;
                    newDetection[p].joints[i].z = -float.Parse(tokens[tokenIndex + 2]) * 0.001f;

                    if (newDetection[p].joints[i].y < lowestY)
                        lowestY = newDetection[p].joints[i].y;
                }

                newDetection[p].lowestY = lowestY;
            }

            return newDetection;
        }
    }
}