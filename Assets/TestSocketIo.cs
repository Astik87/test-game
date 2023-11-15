using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SocketIOClient.Messages;
using UnityEngine.UIElements;

public class TestSocketIo : MonoBehaviour
{
    private Uri uri;
    private SocketIOUnity socketIo;

    public TMP_InputField eventInput;
    public TMP_InputField messageInput;

    public GameObject textTemp;

    private bool isConnected = false;

    public UnityEngine.UI.Button sendButton;

    public struct Test
    {
        public int id;
        public string name;
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;

        uri = new Uri("ws://192.168.0.104:3000");
        socketIo = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        socketIo.JsonSerializer = new NewtonsoftJsonSerializer();

        socketIo.OnUnityThread("hello", this.OnMessage);

        socketIo.OnConnected += (e, h) => {
            this.isConnected = true;
        };

        socketIo.OnDisconnected += (e, h) =>
        {
            this.isConnected = false;
        };

        socketIo.ConnectAsync();

        string filePath = Application.persistentDataPath;

        

    }

    void Update()
    {

        if(!isConnected)
        {
            if(sendButton.interactable)
            {
                sendButton.interactable = false;
            }

            return;
        }

        string eventName = eventInput.text;
        string eventMessage = messageInput.text;

        if((eventName.Length == 0 || eventMessage.Length == 0) && sendButton.interactable)
        {
            sendButton.interactable = false;
        }

        if (eventName.Length != 0 && eventMessage.Length != 0 && !sendButton.interactable)
        {
            sendButton.interactable = true;
        }
    }

    public void OnMessage(SocketIOResponse data)
    {
        Debug.Log(data.GetValue<string>());
        TMP_Text newMessage = AddNewMessage($"hello: {data.GetValue<string>()}");
        newMessage.color = Color.green;
    }

    public TMP_Text AddNewMessage(string text)
    {
        GameObject newMessageObject = new GameObject();

        ContentSizeFitter newMessageObjectSizeFitter = newMessageObject.AddComponent<ContentSizeFitter>();
        TMP_Text newMessageObjectText = newMessageObject.AddComponent<TextMeshProUGUI>();

        newMessageObjectSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        newMessageObjectSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        newMessageObjectText.text = text;
        newMessageObjectText.fontSize = 8;

        newMessageObject.transform.SetParent(this.gameObject.transform, false);

        return newMessageObjectText;
    }

    public void SendMessage()
    {
        string eventName = eventInput.text;
        string eventMessage = messageInput.text;

        TMP_Text newMessageObject = this.AddNewMessage($"{eventName}: {eventMessage}");

        newMessageObject.color = Color.blue;

        this.socketIo.EmitAsync(eventName, eventMessage);
    }
}
