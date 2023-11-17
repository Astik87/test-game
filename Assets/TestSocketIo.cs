using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System;
using UnityEngine;

public static class TestSocketIo
{
    private static Uri uri;
    public static SocketIOUnity socketIo;

    public static bool isConnected = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Connect()
    {
        Screen.fullScreen = false;

        uri = new Uri("ws://192.168.0.104:3000");
        socketIo = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        socketIo.JsonSerializer = new NewtonsoftJsonSerializer();

        socketIo.OnConnected += (e, h) => {
            Debug.Log("Socket IO Connected");
            isConnected = true;
        };

        socketIo.OnDisconnected += (e, h) =>
        {
            isConnected = false;
        };

        socketIo.ConnectAsync();
    }

    static void OnMessage(SocketIOResponse data)
    {
        Debug.Log(data.GetValue<string>());
    }
}
