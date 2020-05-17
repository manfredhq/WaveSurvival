using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalizerUI : MonoBehaviour
{

    TextMeshProUGUI textField;

    public string key;
    public bool isQuest = false;
    // Start is called before the first frame update
    void Start()
    {
        LocalizationSystem.Init();
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalizationSystem.GetLocalizedValue(key, isQuest);
        textField.text = value;
    }

    public void Refresh(bool isQuest = false)
    {
        LocalizationSystem.Init();
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalizationSystem.GetLocalizedValue(key, isQuest);
        textField.text = value;
    }
}
