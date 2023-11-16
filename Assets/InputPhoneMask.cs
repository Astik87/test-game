using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class InputPhoneMask : MonoBehaviour
{
    TMP_InputField inputField;

    public string phoneNumber;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField.text = "+7 (";
        inputField.onValueChanged.AddListener((value) =>
        {
            FormatPhoneValue(value);
        });
    }

    void FormatPhoneValue(string value)
    {

        if(value.Length < 4) {
            inputField.text = "+7 (";
            return;
        }

        phoneNumber = new Regex(@"\D").Replace(value, string.Empty);

        if(phoneNumber.Length < 1)
        {
            phoneNumber = "7";
        }

        string newValue = $"+7 ({phoneNumber[1..]}";

        if (phoneNumber.Length > 4 && phoneNumber.Length <= 7)
        {
            newValue = $"+7 ({phoneNumber[1..4]}) {phoneNumber[4..]}";
        }

        if (phoneNumber.Length > 7 && phoneNumber.Length <= 9)
        {
            newValue = $"+7 ({phoneNumber[1..4]}) {phoneNumber[4..7]}-{phoneNumber[7..]}";
        }

        if (phoneNumber.Length > 9)
        {
            newValue = $"+7 ({phoneNumber[1..4]}) {phoneNumber[4..7]}-{phoneNumber[7..9]}-{phoneNumber[9..]}";
        }

        StartCoroutine(SetPosition());

        IEnumerator SetPosition()
        {
            int width = inputField.caretWidth;

            inputField.caretWidth = 0;

            yield return new WaitForEndOfFrame();

            inputField.text = newValue;

            inputField.caretWidth = width;

            inputField.caretPosition = newValue.Length;

        }
        Debug.Log(inputField.caretPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
