using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;
    private TextMeshProUGUI m_Text;

    // Use this for initialization
    void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        string temp = LocalizationManager.instance.GetLocalizedValue(key);
        m_Text.SetText(temp);   //Replace text form the localization file
    }

}