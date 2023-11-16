using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetDeviceId : MonoBehaviour
{
    TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = SystemInfo.deviceUniqueIdentifier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
