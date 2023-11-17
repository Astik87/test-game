using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SendMessage()
    {
        TestSocketIo.socketIo.OnUnityThread("hello", (data) => {
            Debug.Log(data.GetValue<string>());
        });
        TestSocketIo.socketIo.Emit("hello", "Astik");
    }
}
