using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;

public class TestRequests : MonoBehaviour
{
    public TMP_InputField userNameInput;
    public TMP_Text textObject;
    public Button button;

    struct User
    {
        public string userName;
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(userNameInput.text) && button.interactable)
        {
            button.interactable = false;
        }

        if(!string.IsNullOrEmpty(textObject.text) && !button.interactable)
        {
            button.interactable = true;
        }
    }

    public void SendRequest()
    {
        StartCoroutine(SayHello(new User(){userName = userNameInput.text}));
    }

    private IEnumerator SayHello(User user)
    {
        button.interactable = false;

        WWWForm form = new WWWForm();

        UnityWebRequest request = UnityWebRequest.Post("http://192.168.0.104:3000", form);

        byte[] postBytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(user));

        UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

        request.uploadHandler = uploadHandler;

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        textObject.text = request.downloadHandler.text;

        button.interactable = false;
    }
}
